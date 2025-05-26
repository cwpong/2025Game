using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace GameTools
{

    [Serializable]
    public class Property<T>
    {
        public Property()
        {
        }

        private bool setted = false;

        public Property(T initValue)
        {
            mValue = initValue;
        }

        public T Value
        {
            get { return mValue; }
            set
            {
                if (value == null || !value.Equals(mValue) || !setted)
                {
                    mValue = value;

                    if (mSetter != null)
                    {
                        mSetter.Invoke(mValue);
                        OnValueChanged.Invoke(mValue);
                    }

                    setted = true;
                }
            }
        }

        private T mValue;

        public void Bind(Action<T> setter)
        {
            mSetter += setter;
        }

        public void UnBindAll()
        {
            mSetter = null;
        }

        private event Action<T> mSetter = t => { };
        public UnityEvent<T> OnValueChanged = new OnPropertyChangedEvent<T>();

    }

    public class OnPropertyChangedEvent<T> : UnityEvent<T>
    {

    }

    public interface IView : IDisposable
    {
        void Show();

        void Hide();

        void DrawGUI();

        ILayout Parent { get; set; }

        GUIStyle Style { get; }

        Color BackgroundColor { get; set; }

        void RefreshNextFrame();

        void AddLayoutOption(GUILayoutOption option);

        void RemoveFromParent();

        void Refresh();
    }

    public interface ILayout : IView
    {
        ILayout AddChild(IView view);

        void RemoveChild(IView view);

        void Clear();
    }

    public abstract class View : IView
    {
        private bool mVisible = true;

        public bool Visible
        {
            get { return mVisible; }
            set { mVisible = value; }
        }

        private List<GUILayoutOption> mprivateLayoutOptions = new List<GUILayoutOption>();

        private List<GUILayoutOption> mLayoutOptions
        {
            get { return mprivateLayoutOptions; }
        }

        protected GUILayoutOption[] LayoutStyles { get; private set; }


        private GUIStyle mStyle = new GUIStyle();

        public GUIStyle Style
        {
            get { return mStyle; }
            protected set { mStyle = value; }
        }

        private Color mBackgroundColor = GUI.backgroundColor;

        public Color BackgroundColor
        {
            get { return mBackgroundColor; }
            set { mBackgroundColor = value; }
        }

        public void RefreshNextFrame()
        {
            this.PushCommand(Refresh);
        }

        public void AddLayoutOption(GUILayoutOption option)
        {
            mLayoutOptions.Add(option);
        }

        public void Show()
        {
            Visible = true;
            OnShow();
        }

        protected virtual void OnShow()
        {
        }

        public void Hide()
        {
            Visible = false;
            OnHide();
        }

        protected virtual void OnHide()
        {
        }


        private Color mPreviousBackgroundColor;

        public void DrawGUI()
        {
            BeforeDraw();

            if (Visible)
            {
                mPreviousBackgroundColor = GUI.backgroundColor;
                GUI.backgroundColor = BackgroundColor;
                OnGUI();
                GUI.backgroundColor = mPreviousBackgroundColor;
            }
        }

        private bool mBeforeDrawCalled = false;

        void BeforeDraw()
        {
            if (!mBeforeDrawCalled)
            {
                OnBeforeDraw();

                LayoutStyles = mLayoutOptions.ToArray();

                mBeforeDrawCalled = true;
            }
        }

        protected virtual void OnBeforeDraw()
        {
        }

        public ILayout Parent { get; set; }

        public void RemoveFromParent()
        {
            Parent.RemoveChild(this);
        }

        public virtual void Refresh()
        {
            OnRefresh();
        }

        protected virtual void OnRefresh()
        {
        }

        protected abstract void OnGUI();

        public void Dispose()
        {
            OnDisposed();
        }

        protected virtual void OnDisposed()
        {
        }
    }

    public static class WindowExtension
    {
        public static T PushCommand<T>(this T view, Action command) where T : IView
        {
            RenderEndCommandExecuter.PushCommand(command);
            return view;
        }
    }

    public class RenderEndCommandExecuter
    {
        private static Queue<Action> mPrivateCommands = new Queue<Action>();

        private static Queue<Action> mCommands
        {
            get { return mPrivateCommands; }
        }

        public static void PushCommand(Action command)
        {
            mCommands.Enqueue(command);
        }

        public static void ExecuteCommand()
        {
            while (mCommands.Count > 0)
            {
                mCommands.Dequeue().Invoke();
            }
        }
    }

    public abstract class Layout : View, ILayout
    {
        protected List<IView> Children = new List<IView>();

        public ILayout AddChild(IView view)
        {
            Children.Add(view);
            view.Parent = this;
            return this;
        }

        public void RemoveChild(IView view)
        {
            this.PushCommand(() =>
            {
                Children.Remove(view);
                view.Parent = null;
            });

            view.Dispose();
        }

        public void Clear()
        {
            this.Children.ForEach(view =>
            {
                view.Parent = null;
                view.Dispose();
            });

            this.Children.Clear();
        }

        public override void Refresh()
        {
            Children.ForEach(view => view.Refresh());
            base.Refresh();
        }

        protected override void OnGUI()
        {
            OnGUIBegin();
            foreach (var child in Children)
            {
                child.DrawGUI();
            }

            OnGUIEnd();
        }

        protected abstract void OnGUIBegin();
        protected abstract void OnGUIEnd();
    }

    public class VerticalLayout : Layout
    {
        public string VerticalStyle { get; set; }

        public VerticalLayout(string verticalStyle = null)
        {
            VerticalStyle = verticalStyle;
        }

        protected override void OnGUIBegin()
        {
            if (string.IsNullOrEmpty(VerticalStyle))
            {
                GUILayout.BeginVertical(LayoutStyles);
            }
            else
            {
                GUILayout.BeginVertical(VerticalStyle, LayoutStyles);
            }
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndVertical();
        }
    }

    public class HorizontalLayout : Layout
    {
        public string HorizontalStyle { get; set; }

        public HorizontalLayout(string horizontalStyle = null)
        {
            HorizontalStyle = horizontalStyle;
        }

        protected override void OnGUIBegin()
        {
            if (string.IsNullOrEmpty(HorizontalStyle))
            {
                GUILayout.BeginHorizontal();
            }
            else
            {
                GUILayout.BeginHorizontal(HorizontalStyle);
            }
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndHorizontal();
        }
    }

    public class ScrollLayout : Layout
    {
        Vector2 mScrollPos = Vector2.zero;

        protected override void OnGUIBegin()
        {
            mScrollPos = GUILayout.BeginScrollView(mScrollPos, LayoutStyles);
        }

        protected override void OnGUIEnd()
        {
            GUILayout.EndScrollView();
        }
    }


    public class SpaceView : View
    {
        public int Pixel { get; set; }

        public SpaceView(int pixel = 10)
        {
            Pixel = pixel;
        }

        protected override void OnGUI()
        {
            GUILayout.Space(Pixel);
        }
    }
    public class LabelView : View
    {
        public string Content { get; set; }

        public LabelView(string content)
        {
            Content = content;
            try
            {
                Style = new GUIStyle(GUI.skin.label);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        protected override void OnGUI()
        {
            GUILayout.Label(Content, Style, LayoutStyles);
        }
    }

    public class EnumPopupView : View
    {
        public Property<Enum> ValueProperty { get; set; }

        public EnumPopupView(Enum initValue)
        {
#if UNITY_EDITOR
            ValueProperty = new Property<Enum>(initValue);
            ValueProperty.Value = initValue;

            try
            {
                Style = new GUIStyle(EditorStyles.popup);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
#endif
        }

        protected override void OnGUI()
        {
#if UNITY_EDITOR
            Enum enumType = ValueProperty.Value;
            ValueProperty.Value = EditorGUILayout.EnumPopup(enumType, Style, LayoutStyles);
#endif
        }
    }

    public class CustomView : View
    {
        public CustomView(Action onGuiAction)
        {
            OnGUIAction = onGuiAction;
        }

        public Action OnGUIAction { get; set; }

        protected override void OnGUI()
        {
            OnGUIAction.Invoke();
        }
    }

    public class PopupView : View
    {
        public Property<int> IndexProperty { get; private set; }

        public string[] MenuArray { get; private set; }

        public PopupView(int initValue, string[] menuArray)
        {
            MenuArray = menuArray;
            IndexProperty = new Property<int>(initValue);
            IndexProperty.Value = initValue;

            // Style = new GUIStyle(EditorStyles.popup);
        }

        protected override void OnGUI()
        {
#if UNITY_EDITOR
            IndexProperty.Value = EditorGUILayout.Popup(IndexProperty.Value, MenuArray, LayoutStyles);
#endif
        }
    }

    public class ButtonView : View
    {
        public ButtonView(string text = null, Action onClickEvent = null)
        {
            Text = text;
            OnClickEvent = onClickEvent;
        }

        public string Text { get; set; }

        public Action OnClickEvent { get; set; }
        public UnityEvent OnClick = new UnityEvent();

        protected override void OnGUI()
        {
            if (GUILayout.Button(Text, GUI.skin.button, LayoutStyles))
            {
                if (OnClickEvent != null)
                {
                    OnClickEvent.Invoke();
                }

                OnClick.Invoke();
            }
        }
    }

    public class TextView : View
    {
        public TextView(string content = "", Action<string> onValueChanged = null)
        {
            Content = new Property<string>(content);
            //Style = GUI.skin.textField;

            Content.Bind(_ => OnValueChanged.Invoke());

            if (onValueChanged != null)
            {
                Content.Bind(onValueChanged);
            }
        }

        public Property<string> Content;

        protected override void OnGUI()
        {
#if UNITY_EDITOR
            if (mPasswordMode)
            {
                Content.Value = EditorGUILayout.PasswordField(Content.Value, GUI.skin.textField, LayoutStyles);
            }
            else
            {
                Content.Value = EditorGUILayout.TextField(Content.Value, GUI.skin.textField, LayoutStyles);
            }
#endif
        }

        public UnityEvent OnValueChanged = new UnityEvent();


        private bool mPasswordMode = false;

        public TextView PasswordMode()
        {
            mPasswordMode = true;
            return this;
        }
    }

    public class TextAreaView : View
    {
        public TextAreaView(string content = "")
        {
            Content = new Property<string>(content);
            //Style = new GUIStyle(GUI.skin.textArea);
        }

        public Property<string> Content { get; set; }

        protected override void OnGUI()
        {
#if UNITY_EDITOR
            Content.Value = EditorGUILayout.TextArea(Content.Value, GUI.skin.textArea, LayoutStyles);
#endif
        }
    }

    public class ToggleView : View
    {
        public string Text { get; private set; }

        public ToggleView(string text, bool initValue = false)
        {
            Text = text;
            Toggle = new Property<bool>(initValue);

            try
            {
                Style = new GUIStyle(GUI.skin.toggle);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public Property<bool> Toggle { get; private set; }

        protected override void OnGUI()
        {
            // Toggle.Value = GUILayout.Toggle(Toggle.Value, Text, Style, LayoutStyles);
            Toggle.Value = GUILayout.Toggle(Toggle.Value, Text, LayoutStyles);
        }
    }

    public class ToolbarView : View
    {
        public ToolbarView(int defaultIndex = 0)
        {
            Index.Value = defaultIndex;
            Index.Bind(index => MenuSelected[index].Invoke(MenuNames[index]));
        }


        public ToolbarView Menus(List<string> menuNames)
        {
            this.MenuNames = menuNames;
            // empty
            this.MenuSelected = MenuNames.Select(menuName => new Action<string>(str => { })).ToList();
            return this;
        }

        public ToolbarView AddMenu(string name, Action<string> onMenuSelected)
        {
            MenuNames.Add(name);
            MenuSelected.Add(onMenuSelected);
            return this;
        }

        List<string> MenuNames = new List<string>();

        List<Action<string>> MenuSelected = new List<Action<string>>();

        public Property<int> Index = new Property<int>(0);

        protected override void OnGUI()
        {
            Index.Value = GUILayout.Toolbar(Index.Value, MenuNames.ToArray(), GUI.skin.button, LayoutStyles);
        }
    }

    public static class LayoutExtension
    {
        public static T AddTo<T>(this T view, ILayout parent) where T : IView
        {
            parent.AddChild(view);
            return view;
        }
    }

    public static class ViewExtension
    {
        public static T Width<T>(this T view, float width) where T : IView
        {
            view.AddLayoutOption(GUILayout.Width(width));
            return view;
        }

        public static T Height<T>(this T view, float height) where T : IView
        {
            view.AddLayoutOption(GUILayout.Height(height));
            return view;
        }

        public static T MaxHeight<T>(this T view, float height) where T : IView
        {
            view.AddLayoutOption(GUILayout.MaxHeight(height));
            return view;
        }

        public static T MinHeight<T>(this T view, float height) where T : IView
        {
            view.AddLayoutOption(GUILayout.MinHeight(height));
            return view;
        }

        public static T ExpandHeight<T>(this T view) where T : IView
        {
            view.AddLayoutOption(GUILayout.ExpandHeight(true));
            return view;
        }


        public static T TextMiddleLeft<T>(this T view) where T : IView
        {
            view.Style.alignment = TextAnchor.MiddleLeft;
            return view;
        }

        public static T TextMiddleRight<T>(this T view) where T : IView
        {
            view.Style.alignment = TextAnchor.MiddleRight;
            return view;
        }

        public static T TextLowerRight<T>(this T view) where T : IView
        {
            view.Style.alignment = TextAnchor.LowerRight;
            return view;
        }

        public static T TextMiddleCenter<T>(this T view) where T : IView
        {
            view.Style.alignment = TextAnchor.MiddleCenter;
            return view;
        }

        public static T TextLowerCenter<T>(this T view) where T : IView
        {
            view.Style.alignment = TextAnchor.LowerCenter;
            return view;
        }

        public static T Color<T>(this T view, Color color) where T : IView
        {
            view.BackgroundColor = color;
            return view;
        }

        public static T FontColor<T>(this T view, Color color) where T : IView
        {
            view.Style.normal.textColor = color;
            return view;
        }

        public static T FontBold<T>(this T view) where T : IView
        {
            view.Style.fontStyle = FontStyle.Bold;
            return view;
        }

        public static T FontNormal<T>(this T view) where T : IView
        {
            view.Style.fontStyle = FontStyle.Normal;
            return view;
        }

        public static T FontSize<T>(this T view, int fontSize) where T : IView
        {
            view.Style.fontSize = fontSize;
            return view;
        }
    }



    public class ViewKit
    {

    }
}
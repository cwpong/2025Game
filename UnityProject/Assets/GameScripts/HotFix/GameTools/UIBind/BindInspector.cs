using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameTools
{
#if UNITY_EDITOR
    [CustomEditor(typeof(Bind), true)]
    public class BindInspector : Editor
    {
        class LocaleText
        {
            public static string MarkType
            {
                get { return " 标记类型:"; }
            }

            public static string Type
            {
                get { return " 组件类型:"; }
            }

            public static string Comment
            {
                get { return " 注释"; }
            }

        }
        private Bind mBindScript
        {
            get { return target as Bind; }
        }


        private VerticalLayout mRootLayout;
        private HorizontalLayout mComponentLine;
        private HorizontalLayout mClassnameLine;

        private void OnEnable()
        {
            Bind bind = target as Bind;
            mRootLayout = new VerticalLayout("box");

            new SpaceView()
                .AddTo(mRootLayout);

            var markTypeLine = new HorizontalLayout()
                .AddTo(mRootLayout);

            new LabelView(LocaleText.MarkType)
                .FontSize(12)
                .Width(60)
                .AddTo(markTypeLine);

            var enumPopupView = new EnumPopupView(mBindScript.MarkType)
                .AddTo(markTypeLine);

            enumPopupView.ValueProperty.Bind(newValue =>
            {
                if (mBindScript.MarkType != (BindType)newValue)
                {
                    mBindScript.MarkType = (BindType)newValue;
                    EditorUtility.SetDirty(bind.gameObject);
                }
                OnRefresh();
            });


            new SpaceView()
                .AddTo(mRootLayout);

            new CustomView(() =>
            {
                if (mBindScript.CustomComponentName == null ||
                    string.IsNullOrEmpty(mBindScript.CustomComponentName.Trim()))
                {
                    mBindScript.CustomComponentName = mBindScript.name;
                }
            }).AddTo(mRootLayout);


            mComponentLine = new HorizontalLayout();

            new LabelView(LocaleText.Type)
                .Width(60)
                .FontSize(12)
                .AddTo(mComponentLine);

            if (mBindScript.MarkType == BindType.UnityElement)
            {

                var components = mBindScript.GetComponents<Component>();

                var componentNames = components.Where(c => c.GetType() != typeof(Bind))
                    .Select(c => c.GetType().FullName)
                    .ToArray();

                var componentNameIndex = 0;

                componentNameIndex = componentNames.ToList()
                    .FindIndex((componentName) => componentName.Contains(mBindScript.ComponentName));

                if (componentNameIndex == -1 || componentNameIndex >= componentNames.Length)
                {
                    componentNameIndex = 0;
                }

                mBindScript.ComponentName = componentNames[componentNameIndex];

                new PopupView(componentNameIndex, componentNames)
                    .AddTo(mComponentLine)
                    .IndexProperty.Bind((index) => {
                        if (mBindScript.ComponentName != componentNames[index])
                        {
                            mBindScript.ComponentName = componentNames[index];
                            EditorUtility.SetDirty(bind.gameObject);
                        }
                    });
            }

            mComponentLine.AddTo(mRootLayout);


            new SpaceView()
                .AddTo(mRootLayout);

            var belongsTo = new HorizontalLayout()
                .AddTo(mRootLayout);

            mClassnameLine = new HorizontalLayout();

            new TextView(mBindScript.CustomComponentName)
                .AddTo(mClassnameLine)
                .Content.Bind(newValue => {
                    if (mBindScript.CustomComponentName != newValue)
                    {
                        mBindScript.CustomComponentName = newValue;
                        EditorUtility.SetDirty(bind.gameObject);
                    }
                });

            mClassnameLine.AddTo(mRootLayout);

            new SpaceView()
                .AddTo(mRootLayout);

            new LabelView(LocaleText.Comment)
                .FontSize(12)
                .AddTo(mRootLayout);

            new SpaceView()
                .AddTo(mRootLayout);

            new TextAreaView(mBindScript.Comment)
                .Height(100)
                .AddTo(mRootLayout)
                .Content.Bind(newValue => {
                    if (mBindScript.CustomComment != newValue)
                    {
                        mBindScript.CustomComment = newValue;
                        EditorUtility.SetDirty(bind.gameObject);
                    }
                });

            OnRefresh();
        }

        private void OnRefresh()
        {
            if (mBindScript.MarkType == BindType.UnityElement)
            {
                mComponentLine.Show();
                mClassnameLine.Hide();
            }
            else
            {
                mClassnameLine.Show();
                mComponentLine.Hide();
            }
        }

        private void OnDisable()
        {
            mRootLayout.Clear();
            mRootLayout = null;
        }

        public override void OnInspectorGUI()
        {
            mRootLayout.DrawGUI();
            base.OnInspectorGUI();
        }

    }
#endif

}

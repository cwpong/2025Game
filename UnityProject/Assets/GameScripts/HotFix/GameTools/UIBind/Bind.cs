using System.Collections.Generic;
using System.Linq;
//using TMPro;
//using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GameTools
{
    public class Bind : MonoBehaviour, IBind
    {
        public enum BindComponetType
        {
            /// <summary>
            /// 组件名
            /// </summary>
            DefaultName,
            /// <summary>
            /// 代码变量名
            /// </summary>
            CodeName
        }

        [HideInInspector] public BindType MarkType = BindType.UnityElement;

        public string Comment
        {
            get { return CustomComment; }
        }

        public Transform Transform
        {
            get { return transform; }
        }

        [HideInInspector] public string CustomComponentName;

        [HideInInspector] public string ComponentGeneratePath;

        [HideInInspector] public string CustomComment;

        public BindType GetBindType()
        {
            return MarkType;
        }

        [HideInInspector] [SerializeField] private string mComponentName;

        public virtual string ComponentName
        {
            get
            {
                if (MarkType == BindType.UnityElement)
                {

                    if (string.IsNullOrEmpty(mComponentName))
                    {
                        mComponentName = GetDefaultOrCodeComponentName(Bind.BindComponetType.DefaultName);
                        //GetDefaultComponentName();
                    }

                    return mComponentName;
                }

                return CustomComponentName;
            }
            set { mComponentName = value; }
        }

        public string CheckBindTypeUseName()
        {
            var components = GetComponents<Component>();
            var componentNames = components.Where(c => c.GetType() != typeof(Bind))
                .Select(c => c.GetType())
                .ToArray();

            for (int i = 0; i < componentNames.Length; i++)
            {
                if (mComponentName.Contains(componentNames[i].FullName))
                {
                    return GetComponentTypeName(componentNames[i]);
                }
            }
            return "Tran";
        }

        /// <summary>
        /// 返回类型的自定义命名
        /// </summary>
        /// <param name="comType"></param>
        /// <returns></returns>
        private string GetComponentTypeName(System.Type comType)
        {
            // text mesh pro supported
            //if (comType == typeof(TextMeshProUGUI)) return "TextMeshProUGUI";
            //if (comType == typeof(TextMeshPro)) return "TextMeshPro";
            //if (comType == typeof(TMP_InputField)) return "TMP_InputField";

            // ugui bind
            if (comType == typeof(Canvas)) return "Canvsa";
            if (comType == typeof(ScrollRect)) return "ScrolRect";
            if (comType == typeof(InputField)) return "InputField";
            if (comType == typeof(Dropdown)) return "Dropdown";
            if (comType == typeof(Button)) return "Btn";
            if (comType == typeof(Text)) return "Text";
            if (comType == typeof(RawImage)) return "RawImg";
            if (comType == typeof(Toggle)) return "Toggle";
            if (comType == typeof(Slider)) return "Slider";
            if (comType == typeof(Scrollbar)) return "Scrollbar";
            if (comType == typeof(Image)) return "Img";
            if (comType == typeof(ToggleGroup)) return "ToggleGroup";

            //Unity
            if (comType == typeof(Rigidbody)) return "Rigidbody";
            if (comType == typeof(Rigidbody2D)) return "Rigidbody2D";
            if (comType == typeof(BoxCollider2D)) return "BoxCollider2D";
            if (comType == typeof(BoxCollider)) return "BoxCollider";
            if (comType == typeof(CircleCollider2D)) return "CircleCollider2D";
            if (comType == typeof(SphereCollider)) return "SphereCollider";
            if (comType == typeof(MeshCollider)) return "MeshCollider";
            if (comType == typeof(Collider)) return "Collider";
            if (comType == typeof(Collider2D)) return "Collider2D";
            if (comType == typeof(Animator)) return "Animator";
            if (comType == typeof(Animation)) return "Animation";
            if (comType == typeof(MeshRenderer)) return "MeshRenderer";
            if (comType == typeof(SpriteRenderer)) return "SpriteRenderer";

            if (comType == typeof(Camera)) return "Camera";
            if (comType == typeof(RectTransform)) return "Rtrn";
            //if (comType == typeof(ImgButton)) return "ImgBtn";
            if (comType == typeof(VideoPlayer)) return "VideoPlayer";
            if (comType == typeof(AudioSource)) return "AudioSource";
            //if (comType == typeof(SkeletonGraphic)) return "SpineUI";
            //if (comType == typeof(SkeletonAnimation)) return "SpineAnim";
            //if (comType == typeof(SkeletonRenderer)) return "SpinerRender";
            return "Trn";
        }


        public string GetDefaultOrCodeComponentName(BindComponetType bindComponetType)
        {

            if (GetComponent("SkeletonAnimation")) return bindComponetType == BindComponetType.DefaultName ? "SkeletonAnimation" : "SkeletonAnimation";
            if (GetComponent<ScrollRect>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.ScrollRect" : "Scroll";
            if (GetComponent<InputField>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.InputField" : "InputField";

            // text mesh pro supported
            if (GetComponent("TMP.TextMeshProUGUI")) return bindComponetType == BindComponetType.DefaultName ? "TMP.TextMeshProUGUI" : "TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshProUGUI")) return bindComponetType == BindComponetType.DefaultName ? "TMPro.TextMeshProUGUI" : "TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshPro")) return bindComponetType == BindComponetType.DefaultName ? "TMPro.TextMeshPro" : "TextMeshPro";
            if (GetComponent("TMPro.TMP_InputField")) return bindComponetType == BindComponetType.DefaultName ? "TMPro.TMP_InputField" : "TMP_InputField";

            // ugui bind
            if (GetComponent<Dropdown>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Dropdown" : "Dropdown";
            if (GetComponent<Button>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Button" : "Btn";
            if (GetComponent<Text>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Text" : "Text";
            if (GetComponent<RawImage>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.RawImage" : "RawImg";
            if (GetComponent<Toggle>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Toggle" : "Toggle";
            if (GetComponent<Slider>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Slider" : "Slider";
            if (GetComponent<Scrollbar>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Scrollbar" : "Scrollbar";
            if (GetComponent<Image>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.Image" : "Img";
            if (GetComponent<ToggleGroup>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.UI.ToggleGroup" : "ToggleGroup";

            // other
            if (GetComponent<Rigidbody>()) return bindComponetType == BindComponetType.DefaultName ? "Rigidbody" : "Rigidbody";
            if (GetComponent<Rigidbody2D>()) return bindComponetType == BindComponetType.DefaultName ? "Rigidbody2D" : "Rigidbody2D";

            if (GetComponent<BoxCollider2D>()) return bindComponetType == BindComponetType.DefaultName ? "BoxCollider2D" : "BoxCollider2D";
            if (GetComponent<BoxCollider>()) return bindComponetType == BindComponetType.DefaultName ? "BoxCollider" : "BoxCollider";
            if (GetComponent<CircleCollider2D>()) return bindComponetType == BindComponetType.DefaultName ? "CircleCollider2D" : "CircleCollider2D";
            if (GetComponent<SphereCollider>()) return bindComponetType == BindComponetType.DefaultName ? "SphereCollider" : "SphereCollider";
            if (GetComponent<MeshCollider>()) return bindComponetType == BindComponetType.DefaultName ? "MeshCollider" : "MeshCollider";

            if (GetComponent<Collider>()) return bindComponetType == BindComponetType.DefaultName ? "Collider" : "Collider";
            if (GetComponent<Collider2D>()) return bindComponetType == BindComponetType.DefaultName ? "Collider2D" : "Collider2D";

            if (GetComponent<Animator>()) return bindComponetType == BindComponetType.DefaultName ? "Animator" : "Animator";
            if (GetComponent<Animation>()) return bindComponetType == BindComponetType.DefaultName ? "Animation" : "Animation";
            if (GetComponent<Canvas>()) return bindComponetType == BindComponetType.DefaultName ? "Canvas" : "Canvsa";
            if (GetComponent<Camera>()) return bindComponetType == BindComponetType.DefaultName ? "Camera" : "Camera";

            if (GetComponent<SpriteRenderer>()) return bindComponetType == BindComponetType.DefaultName ? "SpriteRenderer" : "SpriteRenderer";
            //if (GetComponent<ImgButton>()) return bindComponetType == BindComponetType.DefaultName ? "ImgButton" : "Btn";
            if (GetComponent<AudioSource>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.AudioSource" : "AudioSource";
            if (GetComponent<VideoPlayer>()) return bindComponetType == BindComponetType.DefaultName ? "UnityEngine.Video.VideoPlayer" : "VideoPlayer";
            //if (GetComponent<SkeletonRenderer>()) return bindComponetType == BindComponetType.DefaultName ? "Spine.Unity.SkeletonRenderer" : "SkeletonRenderer";
            //if (GetComponent<SkeletonGraphic>()) return bindComponetType == BindComponetType.DefaultName ? "Spine.Unity.SkeletonGraphic" : "SkeletonGraphic";
            //if (GetComponent<SkeletonAnimation>()) return bindComponetType == BindComponetType.DefaultName ? "Spine.Unity.SkeletonAnimation" : "SkeletonAnimation";
            if (GetComponent<MeshRenderer>()) return bindComponetType == BindComponetType.DefaultName ? "MeshRenderer" : "MeshRenderer";

            if (GetComponent<RectTransform>()) return bindComponetType == BindComponetType.DefaultName ? "RectTransform" : "Rtrn";
            return bindComponetType == BindComponetType.DefaultName ? "Transform" : "Trn";
        }

        string GetDefaultComponentName()
        {

            if (GetComponent("SkeletonAnimation")) return "SkeletonAnimation";
            if (GetComponent<ScrollRect>()) return "UnityEngine.UI.ScrollRect";
            if (GetComponent<InputField>()) return "UnityEngine.UI.InputField";

            // text mesh pro supported
            if (GetComponent("TMP.TextMeshProUGUI")) return "TMP.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshProUGUI")) return "TMPro.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshPro")) return "TMPro.TextMeshPro";
            if (GetComponent("TMPro.TMP_InputField")) return "TMPro.TMP_InputField";

            // ugui bind
            if (GetComponent<Dropdown>()) return "UnityEngine.UI.Dropdown";
            if (GetComponent<Button>()) return "UnityEngine.UI.Button";
            if (GetComponent<Text>()) return "UnityEngine.UI.Text";
            if (GetComponent<RawImage>()) return "UnityEngine.UI.RawImage";
            if (GetComponent<Toggle>()) return "UnityEngine.UI.Toggle";
            if (GetComponent<Slider>()) return "UnityEngine.UI.Slider";
            if (GetComponent<Scrollbar>()) return "UnityEngine.UI.Scrollbar";
            if (GetComponent<Image>()) return "UnityEngine.UI.Image";
            if (GetComponent<ToggleGroup>()) return "UnityEngine.UI.ToggleGroup";

            // other
            if (GetComponent<Rigidbody>()) return "Rigidbody";
            if (GetComponent<Rigidbody2D>()) return "Rigidbody2D";

            if (GetComponent<BoxCollider2D>()) return "BoxCollider2D";
            if (GetComponent<BoxCollider>()) return "BoxCollider";
            if (GetComponent<CircleCollider2D>()) return "CircleCollider2D";
            if (GetComponent<SphereCollider>()) return "SphereCollider";
            if (GetComponent<MeshCollider>()) return "MeshCollider";

            if (GetComponent<Collider>()) return "Collider";
            if (GetComponent<Collider2D>()) return "Collider2D";

            if (GetComponent<Animator>()) return "Animator";
            if (GetComponent<Animation>()) return "Animation";
            if (GetComponent<Canvas>()) return "Canvas";
            if (GetComponent<Camera>()) return "Camera";
            if (GetComponent("Empty4Raycast")) return "QFramework.Empty4Raycast";
            if (GetComponent<RectTransform>()) return "RectTransform";
            if (GetComponent<MeshRenderer>()) return "MeshRenderer";

            if (GetComponent<SpriteRenderer>()) return "SpriteRenderer";

            return "Transform";
        }
    }
}


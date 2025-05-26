///*
// *版权(C) 2021 by 厦门千奇百游科技有限公司
// *脚本名: Extension_Spine.cs
// *作者: Bob
// *修改者: 
// *版本: 1.0
// *Unity版本：2019.4.20f1c1
// *创建时间: 2021-07-01
// *描述:   
// *历史记录:
//*/
//using Spine.Unity;
//using UnityEngine;

//namespace GameTools
//{
//    public static partial class Extension_Spine
//    {

//        private static Material SpineShareGrayMaterial = null;          //共享置灰材质
//        private static Material SpineShareOutLineMaterial = null;       //共享描边材质
//        private static Material SpineShareDefaultMaterial = null;       //共享默认材质

//        public static SkeletonAnimation SetOutLine(this SkeletonAnimation selfSpine,Texture mainT)
//        {
//            if (SpineShareOutLineMaterial == null)
//            {
//                SpineShareOutLineMaterial = new Material(Shader.Find("Spine/Outline/Skeleton"));
//                SpineShareOutLineMaterial.SetColor("_OutlineColor", Color.red);
//            }
//            SpineShareOutLineMaterial.mainTexture = mainT;
//            selfSpine.GetComponent<MeshRenderer>().material = SpineShareOutLineMaterial;
//            return selfSpine;
//        }

//        /// <summary>
//        /// 设置灰色材质
//        /// </summary>
//        /// <param name="selfSpine"></param>
//        /// <param name="r"></param>
//        /// <returns></returns>
//        public static SkeletonGraphic SetGrayMat(this SkeletonGraphic selfSpine)
//        {
//            if (SpineShareGrayMaterial == null)
//            {
//                SpineShareGrayMaterial = new Material(Shader.Find("BFeffect/UI/PartArea"));
//            }
//            selfSpine.material = SpineShareGrayMaterial;
//            return selfSpine;
//        }

//        /// <summary>
//        /// 设置默认材质
//        /// </summary>
//        /// <param name="selfSpine"></param>
//        /// <param name="r"></param>
//        /// <returns></returns>
//        public static SkeletonGraphic SetDefaultMat(this SkeletonGraphic selfSpine)
//        {
//            if (SpineShareDefaultMaterial == null)
//            {
//                SpineShareDefaultMaterial = new Material(Shader.Find("Spine/SkeletonGraphic"));
//            }
//            selfSpine.material = SpineShareDefaultMaterial;
//            return selfSpine;
//        }

//        /// <summary>
//        /// 播放Spine动画
//        /// </summary>
//        /// <param name="selfSpine"></param>
//        /// <param name="animName"></param>
//        /// <param name="isLoop"></param>
//        /// <returns></returns>
//        public static void PlayAnim(this SkeletonGraphic selfSpine, string animName, bool isLoop = false, int trackIndex = 0)
//        {
//            if (selfSpine.gameObject.activeInHierarchy)
//            {
//                try
//                {
//                    selfSpine.AnimationState.SetAnimation(trackIndex, animName, isLoop);
//                }
//                catch (System.Exception e)
//                {
//                    Debug.LogError($"{selfSpine} playanim {animName} error : {e.Message} \n {e.StackTrace}");
//                }
//            }
//            else
//            {
//                //Debug.LogWarning($"{selfSpine} activeInHierarchy is false can not play {animName}");
//            }
//        }
//        /// <summary>
//        /// 播放Spine动画
//        /// </summary>
//        /// <param name="selfSpine"></param>
//        /// <param name="animName"></param>
//        /// <param name="isLoop"></param>
//        /// <param name="trackIndex">轨道</param>
//        /// <returns></returns>
//        public static void PlayAnim(this SkeletonAnimation selfSpine, string animName, bool isLoop = false, int trackIndex = 0)
//        {
//            if (selfSpine.gameObject.activeInHierarchy)
//            {
//                try
//                {
//                    selfSpine.AnimationState.SetAnimation(trackIndex, animName, isLoop);
//                }
//                catch (System.Exception e)
//                {
//                    Debug.LogError($"{selfSpine} playanim {animName} error : {e.Message} \n {e.StackTrace}");
//                }
//            }
//            else
//            {
//                string name = selfSpine.name;
//                if (selfSpine.transform.parent != null)
//                {
//                    name = selfSpine.transform.parent.name;
//                }
//                Debug.LogWarning($"{name} activeInHierarchy is false can not play {animName}");
//            }
//        }


//    }
//}

/*
 *版权(C) 2021 by BFramework
 *脚本名: Extension_GameObject.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2018.4.3f1
 *创建时间: 2021-02-04
 *描述:   
 *历史记录:
*/

using UnityEngine.UI;
using UnityEngine;
using System;

namespace GameTools
{
    /// <summary>
    /// 实例化扩展
    /// </summary>
    public static partial class Extension_GameObject
    {

        public static GameObject InstantiateGo(this UnityEngine.Object selfObj, Vector3 v3birth,Transform parent = null)
        {
            GameObject go;
            go = UnityEngine.Object.Instantiate(selfObj, parent) as GameObject;
            go.transform.localPosition = v3birth;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            return go;
        }

        /// <summary>
        /// 实例化扩展，允许设置父类
        /// 实例化后设置父类会把自身属性做一次重置
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject InstantiateObj(this UnityEngine.Object selfObj, Transform parent = null)
        {
            GameObject go;
            go = UnityEngine.Object.Instantiate(selfObj, parent) as GameObject;
            go.transform.ResetAll();
            return go;
        }

        /// <summary>
        /// 隐藏对象
        /// </summary>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static GameObject Hide(this GameObject selfObj)
        {
            if (selfObj != null)
            {
                selfObj.SetActive(false);
            }
            return selfObj;
        }

        /// <summary>
        /// 显示对象
        /// </summary>
        /// <param name="selfObj"></param>
        /// <returns></returns>
        public static GameObject Show(this GameObject selfObj)
        {
            if (selfObj != null)
            {
                selfObj.SetActive(true);
            }
            return selfObj;
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static GameObject SetLayer(this GameObject selfObj, int layer)
        {
            if (selfObj != null)
            {
                selfObj.layer = layer;
            }
            return selfObj;
        }

        /// <summary>
        /// 根据层级名字设置层级
        /// </summary>
        /// <param name="selfObj"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static GameObject SetLayer(this GameObject selfObj, string layerName)
        {
            if (selfObj != null)
            {
                selfObj.layer = LayerMask.NameToLayer(layerName);
            }
            return selfObj;
        }

        /// <summary>
        /// 销毁所有子对象
        /// </summary>
        /// <param name="selfGameObj"></param>
        /// <returns></returns>
        public static GameObject DestroyAllChild(this GameObject selfGameObj)
        {
            var childCount = selfGameObj.transform.childCount;

            for (var i = 0; i < childCount; i++)
            {
                selfGameObj.transform.GetChild(i).DestroyGameObjSafely();
            }

            return selfGameObj;
        }

        /// <summary>
        /// 安全并马上销毁自己
        /// </summary>
        /// <param name="selfGo"></param>
        public static void DestroySelfSafelyImmediately(this GameObject selfGo)
        {
            if (selfGo != null)
            {
                UnityEngine.Object.DestroyImmediate(selfGo);
            }
        }

        /// <summary>
        /// 销毁所有子对象
        /// </summary>
        /// <param name="selfGameObj"></param>
        /// <returns></returns>
        public static void DestroySelf(this GameObject selfGameObj)
        {
            if (selfGameObj != null)
            {
                GameObject.Destroy(selfGameObj);
            }
        }

        /// <summary>
        /// 销毁对象（安全）,并且回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroySelfCallBack(this GameObject selfBehaviour, Action action = null)
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour.gameObject);
                action?.Invoke();
            }
        }

        /// <summary>
        /// 销毁对象（安全）,并且回调，无论是否是销毁空对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroySelfAlwaysCallBack(this GameObject selfBehaviour, Action action = null)
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour.gameObject);
            }
            action?.Invoke();
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        /// <param name="selfGo"></param>
        /// <param name="layer"></param>
        /// <param name="isRecursion">是否递归设置</param>
        public static void SetLayer(this GameObject selfGo, int layer, bool isRecursion)
        {
            if (isRecursion)
            {
                SetLayerRecursionTransform(selfGo.transform, layer);
            }
            else
            {
                selfGo.layer = layer;
            }
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="selfTrans"></param>
        private static void SetLayerRecursionTransform(Transform selfTrans, int layer)
        {
            selfTrans.gameObject.layer = layer;
            for (int i = 0; i < selfTrans.childCount; i++)
            {
                var trans = selfTrans.GetChild(i);
                if (trans.childCount == 0)
                {
                    trans.gameObject.layer = layer;
                    return;
                }
                else
                {
                    SetLayerRecursionTransform(trans, layer);
                }
            }
        }

        /// <summary>
        /// 模仿Transform.Find 查找对象
        /// 为了防止与Transform.Find 冲突，后面添加了一个前缀 To
        /// </summary>
        /// <param name="selfGo"></param>
        public static T ToFind<T>(this GameObject selfGo, string path) where T : Component
        {
            if (selfGo != null)
            {
                return selfGo.transform.Find(path).GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找对象
        /// </summary>
        /// <param name="selfGo"></param>
        public static GameObject FindGo(this GameObject selfGo, string path)
        {
            if (selfGo != null)
            {
                return selfGo.transform.Find(path).gameObject;
            }
            return null;
        }

        /// <summary>
        /// Transform.Find 查找对象
        /// </summary>
        /// <param name="selfGo"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Transform Find(this GameObject selfGo, string path)
        {
            if (selfGo != null)
            {
                return selfGo.transform.Find(path);
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找Text对象
        /// </summary>
        public static Text FindText(this GameObject selfGo, string path, string content)
        {
            if (selfGo != null)
            {
                Text text = selfGo.transform.Find(path).GetComponent<Text>();
                if (text != null)
                {
                    text.text = content;
                }
                return text;
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找Text对象
        /// </summary>
        public static Text FindText(this GameObject selfGo, string path)
        {
            if (selfGo != null)
            {
                return selfGo.transform.Find(path).GetComponent<Text>();
            }
            return null;
        }


        /// <summary>
        /// 模仿Transform.Find 查找Image对象, 添加可以设置的对象
        /// </summary>
        public static Image FindImage(this GameObject selfGo, string path, Sprite sprite)
        {
            if (selfGo != null)
            {
                Image img = selfGo.transform.Find(path).GetComponent<Image>();
                if ((img != null) && (sprite != null))
                {
                    img.sprite = sprite;
                }
                return img;
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找Image对象, 添加可以设置的对象
        /// </summary>
        public static Image FindImage(this GameObject selfGo, string path)
        {
            if (selfGo != null)
            {
                return selfGo.transform.Find(path).GetComponent<Image>();
            }
            return null;
        }

        /// <summary>
        /// 初始化GameObject
        /// </summary>
        /// <param name="go"></param>
        /// <param name="goName"></param>
        public static void InitializeGo(this GameObject go, string goName)
        {
            go.name = goName;
            go.transform.ResetAll();
        }
    }
}
/*
 *版权(C) 2021 by 厦门千奇百游科技有限公司
 *脚本名: Extension_Component.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2019.4.20f1c1
 *创建时间: 2021-03-01
 *描述:   
 *历史记录:
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameTools
{
    public static partial class Extension_Component
    {
        /// <summary>
        /// 安全的处理隐藏对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T Hide<T>(this T selfComponent) where T : Component
        {
            if (selfComponent != null)
            {
                selfComponent.gameObject.Hide();
            }
            return selfComponent;
        }

        /// <summary>
        /// 安全地显示对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T Show<T>(this T selfComponent) where T : Component
        {
            if (selfComponent != null)
            {
                selfComponent.gameObject.Show();
            }
            return selfComponent;
        }

        /// <summary>
        /// 判断组件是否是transform组件，如果是就直接返回，如果不是获取组件再返回(主要是为了提高性能)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Transform GetTransform<T>(this T selfComponent) where T : Component
        {
            return selfComponent as Transform ?? selfComponent.transform;
        }

        public static Transform[] GetTActiveChinlds(this Transform selfTrn)
        {
            int childCount = selfTrn.childCount;

            List<Transform> ListT = new List<Transform>();
            for (int i = 0; i < childCount; i++)
            {
                Transform tr = selfTrn.GetChild(i);
                if (tr.gameObject.activeInHierarchy)
                {
                    ListT.Add(tr);
                }
            }
            return ListT.ToArray();
        }

        public static int GetTActiveChinldsLength(this Transform selfTrn)
        {
            return selfTrn.GetTActiveChinlds().Length;
        }

        /// <summary>
        /// 获取所有的子物体
        /// </summary>
        /// <param name="selfTrn"></param>
        /// <returns></returns>
        public static Transform[] GetTransformChinlds(this Transform selfTrn)
        {
            int childCount = selfTrn.childCount;

            Transform[] allT = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                allT[i] = selfTrn.GetChild(i);
            }
            return allT;
        }

        /// <summary>
        /// 销毁对象（安全）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroyGameObjSafely<T>(this T selfBehaviour) where T : Component
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour.gameObject);
            }
        }

        /// <summary>
        /// 销毁对象（安全）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroyComponentSafely<T>(this T selfBehaviour) where T : Component
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour);
            }
        }

        /// <summary>
        /// 销毁对象（安全）,并且回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroyGameObjCallBack<T>(this T selfBehaviour, Action action = null) where T : Component
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour.gameObject);
                action?.Invoke();
            }
        }

        /// <summary>
        /// 销毁对象（安全）,并且回调,无聊是否执行的是空对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfBehaviour"></param>
        public static void DestroyGameObjAlwaysCallBack<T>(this T selfBehaviour, Action action = null) where T : Component
        {
            if (selfBehaviour != null)
            {
                UnityEngine.Object.Destroy(selfBehaviour.gameObject);
            }
            action?.Invoke();
        }

        /// <summary>
        /// 设置层级（空对象保护）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static T SetLayer<T>(this T selfComponent, int layer) where T : Component
        {
            if (selfComponent != null)
            {
                selfComponent.gameObject.layer = layer;
            }
            return selfComponent;
        }

        /// <summary>
        /// 根据字符串名字设置层级（空对象保护）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="layerName"></param>
        /// <returns></returns>
        public static T SetLayer<T>(this T selfComponent, string layerName) where T : Component
        {
            if (selfComponent != null)
            {
                selfComponent.gameObject.layer = LayerMask.NameToLayer(layerName);
            }
            return selfComponent;
        }

        /// <summary>
        /// 获取组件，如果组件不存在则添加一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject selfComponent) where T : Component
        {
            var comp = selfComponent.gameObject.GetComponent<T>();
            return comp ? comp : selfComponent.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 获取组件，如果组件不存在则添加一个组件
        /// </summary>
        /// <param name="selfComponent"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Component GetOrAddComponent(this GameObject selfComponent, Type type)
        {
            var comp = selfComponent.gameObject.GetComponent(type);
            return comp ? comp : selfComponent.gameObject.AddComponent(type);
        }

        /// <summary>
        /// 设置父对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="parentComponent"></param>
        /// <returns></returns>
        public static T Parent<T>(this T selfComponent, Component parentComponent) where T : Component
        {
            selfComponent.transform.SetParent(parentComponent == null ? null : parentComponent.GetTransform());
            return selfComponent;
        }

        /// <summary>
        /// 设置成为顶端 Transform
        /// </summary>
        /// <returns>The root transform.</returns>
        /// <param name="selfComponent">Self component.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T AsRootTransform<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().SetParent(null);
            return selfComponent;
        }

        /// <summary>
        /// 本地位置/旋转/缩放复位
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetLocalIdentity<T>(this T selfComponent) where T : Component
        {
            if (selfComponent == null)
            {
                return null;
            }
            Transform tmp = selfComponent.GetTransform();
            tmp.localPosition = Vector3.zero;
            tmp.localRotation = Quaternion.identity;
            tmp.localScale = Vector3.one;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部坐标位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="localPos"></param>
        /// <returns></returns>
        public static T SetLocalPosition<T>(this T selfComponent, Vector3 localPos) where T : Component
        {
            selfComponent.GetTransform().localPosition = localPos;
            return selfComponent;
        }

        /// <summary>
        /// 获取局部坐标位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Vector3 GetLocalPosition<T>(this T selfComponent) where T : Component
        {
            return selfComponent.GetTransform().localPosition;
        }

        /// <summary>
        /// 设置局部坐标位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T SetLocalPosition<T>(this T selfComponent, float x, float y, float z) where T : Component
        {
            selfComponent.GetTransform().localPosition = new Vector3(x, y, z);
            return selfComponent;
        }

        private static Vector3 mLocalPos;
        /// <summary>
        /// 设置局部坐标的xy位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetLocalPosition<T>(this T selfComponent, float x, float y) where T : Component
        {
            mLocalPos = selfComponent.GetTransform().localPosition;
            mLocalPos.x = x;
            mLocalPos.y = y;
            selfComponent.GetTransform().localPosition = mLocalPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部坐标的X位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static T SetLocalPositionX<T>(this T selfComponent, float x) where T : Component
        {
            mLocalPos = selfComponent.GetTransform().localPosition;
            mLocalPos.x = x;
            selfComponent.GetTransform().localPosition = mLocalPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部坐标的Y位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetLocalPositionY<T>(this T selfComponent, float y) where T : Component
        {
            mLocalPos = selfComponent.GetTransform().localPosition;
            mLocalPos.y = y;
            selfComponent.GetTransform().localPosition = mLocalPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部坐标的Z位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T SetLocalPositionZ<T>(this T selfComponent, float z) where T : Component
        {
            mLocalPos = selfComponent.GetTransform().localPosition;
            mLocalPos.z = z;
            selfComponent.GetTransform().localPosition = mLocalPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部坐标位置归零
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetLocalPositionIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().localPosition = Vector3.zero;
            return selfComponent;
        }

        /// <summary>
        /// 获取局部旋转四元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Quaternion GetLocalRotation<T>(this T selfComponent) where T : Component
        {
            return selfComponent.GetTransform().localRotation;
        }

        /// <summary>
        /// 设置局部旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="localRotation"></param>
        /// <returns></returns>
        public static T SetLocalEulerAngle<T>(this T selfComponent, Vector3 eulerAngle) where T : Component
        {
            selfComponent.GetTransform().localEulerAngles = eulerAngle;
            return selfComponent;
        }

        /// <summary>
        /// 设置局部旋转四元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="localRotation"></param>
        /// <returns></returns>
        public static T SetLocalRotation<T>(this T selfComponent, Quaternion localRotation) where T : Component
        {
            selfComponent.GetTransform().localRotation = localRotation;
            return selfComponent;
        }

        public static void SetLocalRotation(this Transform t, Vector2 v2)
        {
            t.localRotation = new Quaternion(Mathf.Deg2Rad * v2.x, Mathf.Deg2Rad * v2.y, Mathf.Deg2Rad * 0, t.rotation.w);
        }
        public static void SetLocalRotation(this Transform t, Vector3 v3)
        {
            t.localRotation = new Quaternion(Mathf.Deg2Rad * v3.x, Mathf.Deg2Rad * v3.y, Mathf.Deg2Rad * v3.z, t.rotation.w);
        }

        public static void SetLoaclAngle(this Transform t, Vector3 angle)
        {
            t.localEulerAngles = angle;
        }

        public static void SetLocalRotationX(this Transform t, float newX)
        {
            t.localEulerAngles = new Vector3(newX, t.localEulerAngles.y, t.localEulerAngles.z);
        }
        public static void SetLocalRotationY(this Transform t, float newY)
        {
            t.localEulerAngles = new Vector3(t.localEulerAngles.x, newY, t.localEulerAngles.z);
        }
        public static void SetLocalRotationZ(this Transform t, float newZ)
        {
            t.localEulerAngles = new Vector3(t.localEulerAngles.x, t.localEulerAngles.y, newZ);
        }

        /// <summary>
        /// 设置局部位置不旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetLocalRotationIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().localRotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static T SetLocalScale<T>(this T selfComponent, Vector3 scale) where T : Component
        {
            selfComponent.GetTransform().localScale = scale;
            return selfComponent;
        }

        /// <summary>
        /// 获取全局旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Vector3 GetLocalScale<T>(this T selfComponent) where T : Component
        {
            return selfComponent.GetTransform().localScale;
        }

        /// <summary>
        /// 设置局部缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="xyz"></param>
        /// <returns></returns>
        public static T SetLocalScale<T>(this T selfComponent, float xyz) where T : Component
        {
            selfComponent.GetTransform().localScale = Vector3.one * xyz;
            return selfComponent;
        }

        private static Vector3 mScale;
        /// <summary>
        /// 设置局部缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T SetLocalScale<T>(this T selfComponent, float x, float y, float z) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.x = x;
            mScale.y = y;
            mScale.z = z;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetLocalScale<T>(this T selfComponent, float x, float y) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.x = x;
            mScale.y = y;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局X缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static T SetLocalScaleX<T>(this T selfComponent, float x) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.x = x;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局Y缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetLocalScaleY<T>(this T selfComponent, float y) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.y = y;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局Z缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T SetLocalScaleZ<T>(this T selfComponent, float z) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.z = z;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局Y缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetLocalScaleXY<T>(this T selfComponent, float x, float y) where T : Component
        {
            mScale = selfComponent.GetTransform().localScale;
            mScale.y = y;
            mScale.x = x;
            selfComponent.GetTransform().localScale = mScale;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局不缩放
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetLocalScaleIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().localScale = Vector3.one;
            return selfComponent;
        }

        /// <summary>
        /// 全局transform reset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().position = Vector3.zero;
            selfComponent.GetTransform().rotation = Quaternion.identity;
            selfComponent.GetTransform().localScale = Vector3.one;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T SetPosition<T>(this T selfComponent, Vector3 position) where T : Component
        {
            selfComponent.GetTransform().position = position;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T SetPositionY<T>(this T selfComponent, float y) where T : Component
        {
            Vector3 pos = selfComponent.GetTransform().position;
            selfComponent.GetTransform().position = new Vector3(pos.x, y, pos.z);
            return selfComponent;
        }

        /// <summary>
        /// 获取全局坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Vector3 GetPosition<T>(this T selfComponent) where T : Component
        {
            return selfComponent.GetTransform().position;
        }

        private static Vector3 mPos;
        /// <summary>
        /// 设置全局坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T SetPosition<T>(this T selfComponent, float x, float y, float z) where T : Component
        {
            selfComponent.GetTransform().position = new Vector3(x, y, z);
            return selfComponent;
        }

        /// <summary>
        /// 设置全局xy坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T SetPosition<T>(this T selfComponent, float x, float y) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.x = x;
            mPos.y = y;
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 全局坐标归零
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetPositionIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().position = Vector3.zero;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局X坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static T SetPositionX<T>(this T selfComponent, float x) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.x = x;
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局x坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="xSetter"></param>
        /// <returns></returns>
        public static T SetPositionX<T>(this T selfComponent, Func<float, float> xSetter) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.x = xSetter(mPos.x);
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局Y坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static T PositionY<T>(this T selfComponent, float y) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.y = y;
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局Y坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="ySetter"></param>
        /// <returns></returns>
        public static T PositionY<T>(this T selfComponent, Func<float, float> ySetter) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.y = ySetter(mPos.y);
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局z坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static T PositionZ<T>(this T selfComponent, float z) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.z = z;
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局z坐标
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="zSetter"></param>
        /// <returns></returns>
        public static T PositionZ<T>(this T selfComponent, Func<float, float> zSetter) where T : Component
        {
            mPos = selfComponent.GetTransform().position;
            mPos.z = zSetter(mPos.z);
            selfComponent.GetTransform().position = mPos;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局不旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetRotationIdentity<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().rotation = Quaternion.identity;
            return selfComponent;
        }

        /// <summary>
        /// 设置全局旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static T SetRotation<T>(this T selfComponent, Quaternion rotation) where T : Component
        {
            selfComponent.GetTransform().rotation = rotation;
            return selfComponent;
        }

        /// <summary>
        /// 使用欧拉角设置全局旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static T SetRotation<T>(this T selfComponent, Vector3 angle) where T : Component
        {
            selfComponent.GetComponent<Transform>().eulerAngles = angle;
            return selfComponent;
        }

        /// <summary>
        /// 获得全局旋转
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static Quaternion GetRotation<T>(this T selfComponent) where T : Component
        {
            return selfComponent.GetTransform().rotation;
        }

        //public static Vector3 GetGlobalScale<T>(this T selfComponent) where T : Component
        //{
        //    return selfComponent.GetTransform().lossyScale;
        //}

        ///// <summary>
        ///// 销毁所有子对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="selfComponent"></param>
        ///// <returns></returns>
        //public static T DestroyAllChild<T>(this T selfComponent) where T : Component
        //{
        //    var childCount = selfComponent.GetTransform().childCount;

        //    for (var i = 0; i < childCount; i++)
        //    {
        //        selfComponent.GetTransform().GetChild(i).DestroyGameObjSafely();
        //    }

        //    return selfComponent;
        //}

        /// <summary>
        /// 设置最后位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T AsLastSibling<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().SetAsLastSibling();
            return selfComponent;
        }

        /// <summary>
        /// 设置最前位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T AsFirstSibling<T>(this T selfComponent) where T : Component
        {
            selfComponent.GetTransform().SetAsFirstSibling();
            return selfComponent;
        }

        /// <summary>
        /// 设置位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T SiblingIndex<T>(this T selfComponent, int index) where T : Component
        {
            selfComponent.GetTransform().SetSiblingIndex(index);
            return selfComponent;
        }

        private static Transform tmpTransform;
        /// <summary>
        /// 根据路径显示指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="tranformPath"></param>
        /// <returns></returns>
        public static T ShowChildTransByPath<T>(this T selfComponent, string tranformPath) where T : Component
        {
            if (selfComponent != null)
            {
                tmpTransform = selfComponent.GetTransform().Find(tranformPath);
                if (tmpTransform != null)
                {
                    tmpTransform.gameObject.Show();
                }
            }
            return selfComponent;
        }

        /// <summary>
        /// 根据路径隐藏指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="tranformPath"></param>
        /// <returns></returns>
        public static T HideChildTransByPath<T>(this T selfComponent, string tranformPath) where T : Component
        {
            if (selfComponent != null)
            {
                tmpTransform = selfComponent.GetTransform().Find(tranformPath);
                if (tmpTransform != null)
                {
                    tmpTransform.gameObject.Hide();
                }
            }
            return selfComponent;
        }

        /// <summary>
        /// 隐藏所有子对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T HideAllChildren<T>(this T selfComponent) where T : Component
        {
            tmpTransform = selfComponent.transform;
            for (int i = 0; i < tmpTransform.childCount; i++)
            {
                tmpTransform.GetChild(i).gameObject.SetActive(false);
            }
            return selfComponent;
        }

        /// <summary>
        /// 隐藏指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T SetActive<T>(this T selfComponent, bool active) where T : Component
        {
            if (selfComponent != null)
            {
                GameObject go = selfComponent.gameObject;
                if (go.activeSelf != active)
                {
                    go.SetActive(active);
                }
            }
            return selfComponent;
        }

        /// <summary>
        /// 根判断对象是否隐藏或者已经销毁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static bool IsNullOrInactive<T>(this T selfComponent) where T : Component
        {
            if (selfComponent != null)
            {
                GameObject go = selfComponent.gameObject;
                return !go.activeInHierarchy;
            }
            return true;
        }

        /// <summary>
        /// 根判断对象是否隐藏或者已经销毁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T InstantiateGameObj<T>(this T selfComponent) where T : Component
        {
            if (selfComponent != null)
            {
                return GameObject.Instantiate(selfComponent.gameObject).GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找对象
        /// 为了防止与Transform.Find 冲突，后面添加了一个前缀 To
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <returns></returns>
        public static T ToFind<T>(this Component selfComponent, string path) where T : Component
        {
            if (selfComponent != null)
            {
                //return ((selfComponent as Transform) ?? selfComponent.transform).Find(path).GetComponent<T>();    // 性能好，但是怪怪的
                return selfComponent.transform.Find(path).GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找Text对象
        /// </summary>
        public static Text FindText(this Component selfComponent, string path, string content)
        {
            if (selfComponent != null)
            {
                Text text = selfComponent.transform.Find(path).GetComponent<Text>();
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
        public static Text FindText(this Component selfComponent, string path)
        {
            if (selfComponent != null)
            {
                return selfComponent.transform.Find(path).GetComponent<Text>();
            }
            return null;
        }

        /// <summary>
        /// 模仿Transform.Find 查找Image对象, 添加可以设置的对象
        /// </summary>
        public static Image FindImage(this Component selfComponent, string path, Sprite sprite)
        {
            if (selfComponent != null)
            {
                Image img = selfComponent.transform.Find(path).GetComponent<Image>();
                if (img != null)
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
        public static Image FindImage(this Component selfComponent, string path)
        {
            if (selfComponent != null)
            {
                return selfComponent.transform.Find(path).GetComponent<Image>();
            }
            return null;
        }

        /// <summary>
        /// 根据子对象的名称获得子对象的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T GetComponentInChildByName<T>(this T selfComponent, string childName) where T : Component
        {
            return GetComponentInChildren<T>(selfComponent.transform, childName);
        }

        /// <summary>
        /// 根据子对象的名称获得子对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selfComponent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static GameObject GetGameObjChildByName<T>(this T selfComponent, string childName) where T : Component
        {
            T component = GetComponentInChildren<T>(selfComponent.transform, childName);
            if (component != null)
            {
                return component.gameObject;
            }
            return null;
        }

        /// <summary>
        /// 根据游戏对象名查找子对象
        /// </summary>
        /// <param name="rootTrans"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        private static T GetComponentInChildren<T>(Transform rootTrans, string childName) where T : Component
        {
            for (int i = 0; i < rootTrans.childCount; i++)
            {
                Transform tmp = rootTrans.GetChild(i);
                if (tmp.gameObject.name == childName)
                {
                    T com = tmp.GetComponent<T>();
                    if (com != null)
                    {
                        return com;
                    }
                }

                // 递归查找
                T subCom = GetComponentInChildren<T>(tmp, childName);
                if (subCom != null)
                {
                    return subCom;
                }
            }
            return null;
        }

    }
}

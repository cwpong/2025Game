/*
 *版权(C) 2021 by BFramework
 *脚本名: Extension_Transform.cs
 *作者: Chenwp
 *修改者: 
 *版本: 1.0
 *Unity版本：2018.4.3f1
 *创建时间: 2021-01-30
 *描述:   
 *历史记录:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools
{
    public static partial class Extension_Transform
    {
        public static void SetPositionX(this Transform t, float newX)
        {
            t.position = new Vector3(newX, t.position.y, t.position.z);
        }

        public static void SetPositionY(this Transform t, float newY)
        {
            t.position = new Vector3(t.position.x, newY, t.position.z);
        }
        public static void SetPositionZ(this Transform t, float newZ)
        {
            t.position = new Vector3(t.position.x, t.position.y, newZ);
        }
        public static void SetPosition(this Transform t, Vector3 v3)
        {
            t.position = v3;
        }
        public static void SetPosition(this Transform t, float newX, float newY, float newZ)
        {
            t.position = new Vector3(newX, newY, newZ);
        }
        public static void SetLocalPosition(this Transform t, Vector3 v3)
        {
            t.localPosition = v3;
        }
        public static void SetLocalPosition(this Transform t, float newX, float newY, float newZ)
        {
            t.localPosition = new Vector3(newX, newY, newZ);
        }

        public static void SetLocalPositionX(this Transform t, float newX)
        {
            t.localPosition = new Vector3(newX, t.localPosition.y, t.localPosition.z);
        }

        public static void SetLocalPositionY(this Transform t, float newY)
        {
            t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
        }

        public static void AddPositionX(this Transform t, float addX)
        {
            t.position = t.position + new Vector3(addX, 0, 0);
        }
        public static void AddPositionY(this Transform t, float addY)
        {
            t.position = t.position + new Vector3(0, addY, 0);
        }
        public static void AddPositionZ(this Transform t, float addZ)
        {
            t.position = t.position + new Vector3(0, 0, addZ);
        }
        public static void AddLocalPositionX(this Transform t, float addX)
        {
            t.localPosition = t.localPosition + new Vector3(addX, 0, 0);
        }
        public static void AddLocalPositionY(this Transform t, float addY)
        {
            t.localPosition = t.localPosition + new Vector3(0, addY, 0);
        }
        public static void AddLocalPositionZ(this Transform t, float addZ)
        {
            t.localPosition = t.localPosition + new Vector3(0, 0, addZ);
        }
        //--------------------------Scale-----------------------------//
        public static void SetLocalScale(this Transform t, float newX, float newY, float newZ)
        {
            t.localScale = new Vector3(newX, newY, newZ);
        }
        public static void SetLocalScale(this Transform t, Vector3 scale)
        {
            t.localScale = scale;
        }

        //----------------------------Rotation-------------------------------//
        public static void SetRotationX(this Transform t, float newX)
        {
            t.rotation = new Quaternion(newX, t.rotation.y, t.rotation.z, t.rotation.w);
        }
        public static void SetRotationY(this Transform t, float newY)
        {
            t.rotation = new Quaternion(t.rotation.z, newY, t.rotation.z, t.rotation.w);
        }
        public static void SetRotationZ(this Transform t, float newZ)
        {
            t.rotation = new Quaternion(t.rotation.z, t.rotation.y, newZ, t.rotation.w);
        }

        public static void AddRotationX(this Transform t, float addX)
        {
            t.rotation = new Quaternion(t.rotation.x + addX, t.rotation.y, t.rotation.z, t.rotation.w);
        }
        public static void AddRotationY(this Transform t, float addY)
        {
            t.rotation = new Quaternion(t.rotation.x, t.rotation.y + addY, t.rotation.z, t.rotation.w);
        }
        public static void AddRotationZ(this Transform t, float addZ)
        {
            t.rotation = new Quaternion(t.rotation.x, t.rotation.y, t.rotation.z + addZ, t.rotation.w);
        }
        public static void ResetAll(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }

        public static void SetParentAndRes(this Transform t, Transform _parent)
        {
            t.SetParent(_parent);
            t.ResetAll();
        }
    }
}

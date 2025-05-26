///*
// *版权(C) 2021 by 厦门千奇百游科技有限公司
// *脚本名: Extension_Function.cs
// *作者: Chenwp
// *修改者: 
// *版本: 1.0
// *Unity版本：2021.3.20f1c1
// *创建时间: 2023-03-29
// *描述:   
// *历史记录:
//*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace GameTools
//{
//    public static partial class Extension_Function
//    {
//        private static TouchEventTrigger.VoidDelegate voidDelegate = (GameObject obj) =>
//        {
//            //TODO:添加播放音效
//        };
//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <param name="isClickAudioOn">是否有点击音效</param>
//        /// <returns></returns>
//        public static T AddOnClick<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfComponent.gameObject);
//            touch.onClick = callback;
//            touch.onClick += voidDelegate;

//            return selfComponent;
//        }
//        public static T AddOnClick<T>(this T selfComponent, System.Action callback) where T : Component
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfComponent.gameObject);
//            void voidDelegate(GameObject obj)
//            {
//                //TODO：添加播放音效
//                callback();
//            }
//            touch.onClick = voidDelegate;

//            return selfComponent;
//        }

//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <returns></returns>
//        public static GameObject AddOnClick(this GameObject selfGo, TouchEventTrigger.VoidDelegate callback)
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfGo);
//            touch.onClick = callback;
//            touch.onClick += voidDelegate;

//            return selfGo;
//        }

//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <returns></returns>
//        public static T AddOnDown<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger.Get(selfComponent.gameObject).onDown = callback;
//            return selfComponent;
//        }

//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <returns></returns>
//        public static GameObject AddOnDown(this GameObject gameObject, TouchEventTrigger.VoidDelegate callback)
//        {
//            TouchEventTrigger.Get(gameObject).onDown = callback;
//            return gameObject;
//        }

//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <returns></returns>
//        public static T AddOnUp<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger.Get(selfComponent.gameObject).onUp = callback;
//            return selfComponent;
//        }
//        /// <summary>
//        /// 扩展 TouchEventTrigger 功能
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="callback"></param>
//        /// <returns></returns>
//        public static GameObject AddOnUp(this GameObject gameObject, TouchEventTrigger.VoidDelegate callback)
//        {
//            TouchEventTrigger.Get(gameObject).onUp = callback;
//            return gameObject;
//        }
//        public static GameObject AddOnExit(this GameObject gameObject, TouchEventTrigger.VoidDelegate callback)
//        {
//            TouchEventTrigger.Get(gameObject).onExit = callback;
//            return gameObject;
//        }
//        public static TouchEventTrigger GetTouchOnClick<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfComponent.gameObject);
//            touch.onClick = callback;
            
//            //TODO:播放音效

//            return touch;
//        }

//        public static TouchEventTrigger GetTouchOnDown<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfComponent.gameObject);
//            touch.onDown = callback;

//            return touch;
//        }

//        public static TouchEventTrigger GetTouchOnUp<T>(this T selfComponent, TouchEventTrigger.VoidDelegate callback) where T : Component
//        {
//            TouchEventTrigger touch = TouchEventTrigger.Get(selfComponent.gameObject);
//            touch.onUp = callback;
//            return touch;
//        }
//    }
//}

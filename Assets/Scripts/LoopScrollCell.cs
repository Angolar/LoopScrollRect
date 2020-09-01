// /***************************************************************
//  * 项目名称：第七史诗
//  * 类 名 称：NewMonoBehaviour
//  * 功能简介：
//  * 版 本 号：v1.0.0.0
//  * 作    者：Robert Xu
//  * CLR 版本：4.0.30319.42000  ( mono-4.0 )
//  * C# 版 本：4.7.1
//  * 创建时间：2020/8/29 15:46:34
// ****************************************************************
//  * Copyright  @  Xu Donghao 2020  .  All rights reserved.
// ***************************************************************/
using UnityEngine;
using System.Collections;
using System;


namespace UnityEngine.UI
{
    [RequireComponent(typeof(LayoutElement))]
    public abstract class LoopScrollCell : MonoBehaviour
    {
        public GameObject Root;
        public LayoutElement Element;
        public RectTransform RectTrans;

        /// <summary>
        /// Cell序号
        /// </summary>
        public int Index { get; set; }

        public virtual LoopScrollCellLoadType LoadType
        {
            get { return LoopScrollCellLoadType.Static; }
        }

        /// <summary>
        /// Cell宽度
        /// </summary>
        public float Width
        {
            get
            {
                return Element.preferredWidth;
            }
            set
            {
                Element.preferredWidth = value;
            }
        }

        /// <summary>
        /// Cell高度
        /// </summary>
        public float Height
        {
            get
            {
                return Element.preferredHeight;
            }
            set
            {
                Element.preferredHeight = value;
            }
        }

        protected void Init()
        {
            Root = gameObject;

            Element = gameObject.GetComponent<LayoutElement>();
            if (Element == null)
            {
                Element = gameObject.AddComponent<LayoutElement>();
            }

            RectTrans = gameObject.GetComponent<RectTransform>();
            if (RectTrans == null)
            {
                RectTrans = gameObject.AddComponent<RectTransform>();
            }
            RectTrans.anchorMin = Vector2.zero;
            RectTrans.anchorMax = Vector2.one;
            RectTrans.sizeDelta = Vector2.zero;
            RectTrans.anchoredPosition = Vector2.zero;
        }

        public virtual void Refresh()
        {

        }

        public virtual void Release()
        {
            //TODO:将Cell放入池子
        }

        public virtual void OnDestroy()
        {
            
        }
    }
}


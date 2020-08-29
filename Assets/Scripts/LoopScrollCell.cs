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
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class LoopScrollCell : MonoBehaviour
{
    public GameObject Root;
    public LayoutElement Element;
    public RectTransform RectTrans;

    /// <summary>
    /// Cell序号
    /// </summary>
    public int Index { get; set; }

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

    /// <summary>
    /// 逻辑控制器
    /// </summary>
    public ILoopScrollCellController Controller;

    public static LoopScrollCell Create(int idx)
    {
        var go = new GameObject("Cell" + idx);
        var obj = go.GetComponent<LoopScrollCell>();

        if (obj == null)
        {
            obj = go.AddComponent<LoopScrollCell>();
        }

        obj.Element = go.GetComponent<LayoutElement>();
        obj.RectTrans = go.GetComponent<RectTransform>();
        if (obj.Element == null)
        {
            obj.Element = go.AddComponent<LayoutElement>();
        }
        
        obj.Root = go;

        return obj;
    }

    public void Release()
    {
        //通知控制器回收
        Controller.CellClear();

        //TODO:将Cell放入池子
    }

    public void OnDestroy()
    {
        //通知控制器销毁
        Controller.CellDestroy();
    }
}

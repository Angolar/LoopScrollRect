// /***************************************************************
//  * 项目名称：第七史诗
//  * 类 名 称：NewClass
//  * 功能简介：
//  * 版 本 号：v1.0.0.0
//  * 作    者：Robert Xu
//  * CLR 版本：4.0.30319.42000  ( mono-4.0 )
//  * C# 版 本：4.7.1
//  * 创建时间：2020/8/31 17:9:38
// ****************************************************************
//  * Copyright  @  Xu Donghao 2020  .  All rights reserved.
// ***************************************************************/
using System;

using UnityEngine;
using UnityEngine.UI;

public class LoopScrollStaticCell : LoopScrollCell
{
    public override LoopScrollCellLoadType LoadType => LoopScrollCellLoadType.Static;

    public LoopScrollStaticCell()
    {
    }

    public static LoopScrollStaticCell Create(GameObject goSource, int idx)
    {
        var go = GameObject.Instantiate(goSource);
        go.name += idx.ToString();

        var obj = go.GetComponent<LoopScrollStaticCell>();
        if (obj == null)
        {
            obj = go.AddComponent<LoopScrollStaticCell>();
        }

        obj.Init();

        return obj;
    }
}

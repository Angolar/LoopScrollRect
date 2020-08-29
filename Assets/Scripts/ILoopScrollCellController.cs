// /***************************************************************
//  * 项目名称：第七史诗
//  * 类 名 称：ILoopScrollCellController
//  * 功能简介：LoopScrollCell控制器接口
//  * 版 本 号：v1.0.0.0
//  * 作    者：Robert Xu
//  * CLR 版本：4.0.30319.42000  ( mono-4.0 )
//  * C# 版 本：4.7.1
//  * 创建时间：2020/8/29 17:46:40
// ****************************************************************
//  * Copyright  @  Xu Donghao 2020  .  All rights reserved.
// ***************************************************************/
using System;

/// <summary>
/// LoopScrollCell控制器接口
/// </summary>
public interface ILoopScrollCellController
{
    /// <summary>
    /// 清理Cell
    /// </summary>
    void CellClear();

    /// <summary>
    /// 销毁Cell
    /// </summary>
    void CellDestroy();
}

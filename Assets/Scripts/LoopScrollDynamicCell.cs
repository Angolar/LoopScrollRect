// /***************************************************************
//  * 项目名称：第七史诗
//  * 类 名 称：NewClass1
//  * 功能简介：
//  * 版 本 号：v1.0.0.0
//  * 作    者：Robert Xu
//  * CLR 版本：4.0.30319.42000  ( mono-4.0 )
//  * C# 版 本：4.7.1
//  * 创建时间：2020/8/31 17:9:46
// ****************************************************************
//  * Copyright  @  Xu Donghao 2020  .  All rights reserved.
// ***************************************************************/
using System;


namespace UnityEngine.UI
{
    public class LoopScrollDynamicCell : LoopScrollCell
    {
        public override LoopScrollCellLoadType LoadType => LoopScrollCellLoadType.Dynamic;

        /// <summary>
        /// 逻辑控制器
        /// </summary>
        public ILoopScrollCellController Controller;

        public static LoopScrollDynamicCell Create(int idx)
        {
            var go = new GameObject("Cell" + idx);
            var obj = go.GetComponent<LoopScrollDynamicCell>();

            if (obj == null)
            {
                obj = go.AddComponent<LoopScrollDynamicCell>();
            }
            
            obj.Init();

            return obj;
        }

        public override void Release()
        {
            //通知控制器回收
            Controller.CellClear();

            //TODO:将Cell放入池子
        }

        public override void OnDestroy()
        {
            //通知控制器销毁
            Controller.CellDestroy();
        }
    }
}

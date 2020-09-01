using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEditor;
using Unity.UIWidgets.ui;
using UnityEngine.Assertions.Must;

namespace SG
{
    [RequireComponent(typeof(UnityEngine.UI.LoopScrollRect))]
    [DisallowMultipleComponent]
    public class InitOnStart : MonoBehaviour
    {
        public int totalCount = -1;
        void Start()
        {
            var ls = GetComponent<LoopScrollRect>();
            ls.Count = totalCount;
            ls.CellProvider = OnCellProvider;
            ls.CellTypeProvider = OnCellTypeProvider;
            ls.RefillCells();
        }

        private Type OnCellTypeProvider(int idx)
        {
            return typeof(ScrollIndexCallback1);
        }

        private LoopScrollCell OnCellProvider(LoopScrollCell cell, int idx)
        {
            LoopScrollDynamicCell dynamicCell = cell as LoopScrollDynamicCell;

            if (dynamicCell.Controller == null)
            {
                UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>("Assets/Demo/Resources/ScrollCell1.prefab");
                var go = (GameObject)GameObject.Instantiate(asset);
                var goRect = go.GetComponent<RectTransform>();
                goRect.SetParent(cell.RectTrans);
                goRect.localScale = Vector3.one;

                goRect.anchorMin = new Vector2(0, 1);
                goRect.anchorMax = new Vector2(0, 1);
                //goRect.anchoredPosition = Vector2.zero;
                goRect.pivot = new Vector2(0, 1);
                goRect.sizeDelta = new Vector2(150.0f, 50.0f);
                cell.Height = 50;

                dynamicCell.Controller = cell.GetComponentInChildren<ScrollIndexCallback1>();

                var aaa = go.GetComponent<LayoutElement>();
                aaa.enabled = false;
            }

            dynamicCell.GetComponentInChildren<ScrollIndexCallback1>().ScrollCellIndex(idx);

            return cell;
        }
    }
}
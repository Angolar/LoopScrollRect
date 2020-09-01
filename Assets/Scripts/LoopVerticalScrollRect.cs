using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Loop Vertical Scroll Rect", 51)]
    [DisallowMultipleComponent]
    public class LoopVerticalScrollRect : LoopScrollRect
    {
        private Vector2 m_DefaultCellSize = Vector2.zero;

        protected override float GetSize(LoopScrollCell cell)
        {
            float size = contentSpacing;
            if (m_GridLayout != null)
            {
                size += m_GridLayout.cellSize.y;
            }
            else
            {
                size += cell.Height;
            }
            return size;
        }

        protected override float GetDimension(Vector2 vector)
        {
            return vector.y;
        }

        protected override Vector2 GetVector(float value)
        {
            return new Vector2(0, value);
        }

        protected override void Awake()
        {
            base.Awake();
            directionSign = -1;

            GridLayoutGroup layout = content.GetComponent<GridLayoutGroup>();
            if (layout != null && layout.constraint != GridLayoutGroup.Constraint.FixedColumnCount)
            {
                Debug.LogError("[LoopHorizontalScrollRect] unsupported GridLayoutGroup constraint");
            }
        }

        protected override bool UpdateCells(Bounds viewBounds, Bounds contentBounds)
        {
            bool changed = false;

            if (viewBounds.min.y < contentBounds.min.y)
            {
                float size = NewCellAtEnd(), totalSize = size;
                while (size > 0 && viewBounds.min.y < contentBounds.min.y - totalSize)
                {
                    size = NewCellAtEnd();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            if (viewBounds.max.y > contentBounds.max.y)
            {
                float size = NewCellAtStart(), totalSize = size;
                while (size > 0 && viewBounds.max.y > contentBounds.max.y + totalSize)
                {
                    size = NewCellAtStart();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            if (viewBounds.min.y > contentBounds.min.y + threshold)
            {
                float size = DeleteCellAtEnd(), totalSize = size;
                while (size > 0 && viewBounds.min.y > contentBounds.min.y + threshold + totalSize)
                {
                    size = DeleteCellAtEnd();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            //if (viewBounds.min.y > contentBounds.min.y)
            //{
            //    if (!(((m_Dragging || velocity != Vector2.zero) && Count >= 0 && cellTypeStart < contentConstraintCount)
            //    || content.childCount == 0))
            //    {
            //        float totalSize = 0;
            //        int i = 1;
            //        bool isDirty = false;
            //        while (viewBounds.min.y > contentBounds.min.y + totalSize && i <= contentConstraintCount)
            //        {
            //            LoopScrollCell oldCell = content.GetChild(content.childCount - i).GetComponent<LoopScrollCell>();
            //            totalSize += GetSize(oldCell);
            //            ++i;

            //            if (viewBounds.min.y <= contentBounds.min.y + totalSize)
            //            {
            //                break;
            //            }

            //            isDirty = true;
            //            DeleteCellAtEnd();
            //        }

            //        if (isDirty)
            //            changed = true;
            //    }
            //}

            if (viewBounds.max.y < contentBounds.max.y - threshold)
            {
                float size = DeleteCellAtStart(), totalSize = size;
                while (size > 0 && viewBounds.max.y < contentBounds.max.y - threshold - totalSize)
                {
                    size = DeleteCellAtStart();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            //if (viewBounds.max.y < contentBounds.max.y)
            //{
            //    if (!(((m_Dragging || velocity != Vector2.zero) && Count >= 0 && cellTypeEnd >= Count - 1)
            //    || content.childCount == 0) && contentConstraintCount > 0)
            //    {
            //        float totalSize = 0;
            //        int i = 1;
            //        bool isDirty = false;
            //        while (viewBounds.max.y < contentBounds.max.y - totalSize && i <= contentConstraintCount)
            //        {
            //            LoopScrollCell oldCell = content.GetChild(0).GetComponent<LoopScrollCell>();
            //            totalSize += GetSize(oldCell);
            //            ++i;

            //            if (viewBounds.max.y >= contentBounds.max.y - totalSize)
            //            {
            //                break;
            //            }

            //            isDirty = true;
            //            DeleteCellAtStart();
            //        }

            //        if (isDirty)
            //            changed = true;
            //    }
            //}

            return changed;
        }

        protected override Vector2 GetDafaultCellSize()
        {
            if (m_DefaultCellSize == Vector2.zero)
            {
                HorizontalOrVerticalLayoutGroup layout1 = content.GetComponent<HorizontalOrVerticalLayoutGroup>();
                if (layout1 != null)
                {
                    m_DefaultCellSize.x = viewRect.rect.size.x - layout1.padding.left - layout1.padding.right;
                    m_DefaultCellSize.y = 100;
                }

                GridLayoutGroup layout2 = content.GetComponent<GridLayoutGroup>();
                if (layout2 != null)
                {
                    m_DefaultCellSize = layout2.cellSize;
                }
            }

            return m_DefaultCellSize;
        }
    }
}
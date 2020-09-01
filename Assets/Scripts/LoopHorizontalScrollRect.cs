using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [AddComponentMenu("UI/Loop Horizontal Scroll Rect", 50)]
    [DisallowMultipleComponent]
    public class LoopHorizontalScrollRect : LoopScrollRect
    {
        private Vector2 m_DefaultCellSize = Vector2.zero;

        protected override float GetSize(LoopScrollCell cell)
        {
            float size = contentSpacing;
            if (m_GridLayout != null)
            {
                size += m_GridLayout.cellSize.x;
            }
            else
            {
                size += cell.Width;
            }
            return size;
        }

        protected override float GetDimension(Vector2 vector)
        {
            return -vector.x;
        }

        protected override Vector2 GetVector(float value)
        {
            return new Vector2(-value, 0);
        }

        protected override void Awake()
        {
            base.Awake();
            directionSign = 1;

            GridLayoutGroup layout = content.GetComponent<GridLayoutGroup>();
            if (layout != null && layout.constraint != GridLayoutGroup.Constraint.FixedRowCount)
            {
                Debug.LogError("[LoopHorizontalScrollRect] unsupported GridLayoutGroup constraint");
            }
        }

        protected override bool UpdateCells(Bounds viewBounds, Bounds contentBounds)
        {
            bool changed = false;

            if (viewBounds.max.x > contentBounds.max.x)
            {
                float size = NewCellAtEnd(), totalSize = size;
                while (size > 0 && viewBounds.max.x > contentBounds.max.x + totalSize)
                {
                    size = NewCellAtEnd();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            if (viewBounds.min.x < contentBounds.min.x)
            {
                float size = NewCellAtStart(), totalSize = size;
                while (size > 0 && viewBounds.min.x < contentBounds.min.x - totalSize)
                {
                    size = NewCellAtStart();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            if (viewBounds.max.x < contentBounds.max.x - threshold)
            {
                float size = DeleteCellAtEnd(), totalSize = size;
                while (size > 0 && viewBounds.max.x < contentBounds.max.x - threshold - totalSize)
                {
                    size = DeleteCellAtEnd();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            if (viewBounds.min.x > contentBounds.min.x + threshold)
            {
                float size = DeleteCellAtStart(), totalSize = size;
                while (size > 0 && viewBounds.min.x > contentBounds.min.x + threshold + totalSize)
                {
                    size = DeleteCellAtStart();
                    totalSize += size;
                }
                if (totalSize > 0)
                    changed = true;
            }

            return changed;
        }

        protected override Vector2 GetDafaultCellSize()
        {
            if (m_DefaultCellSize == Vector2.zero)
            {
                HorizontalOrVerticalLayoutGroup layout1 = content.GetComponent<HorizontalOrVerticalLayoutGroup>();
                if (layout1 != null)
                {
                    m_DefaultCellSize.x = 100;
                    m_DefaultCellSize.y = viewRect.rect.size.y - layout1.padding.top - layout1.padding.bottom;
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
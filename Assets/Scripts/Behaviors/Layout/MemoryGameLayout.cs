using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Layout
{
    public class MemoryGameLayout : Layout
    {
        private Vector2 _padding = new Vector2 { x = 0.8f, y = 1.1f };
        private int _colCount = 6;
        private int _rowCount;

        public MemoryGameLayout(List<Transform> layoutElements) : base(layoutElements) { }

        public override void Refresh()
        {
            _rowCount = _layoutElements.Count % _colCount == 0 ? _layoutElements.Count / _colCount : (_layoutElements.Count / _colCount) + 1;
            int currentRow = 0, currentColumn = 0;

            foreach (var element in _layoutElements)
            {
                element.transform.position = GetPosition(new[] { currentColumn, currentRow });

                if (currentColumn == _colCount - 1)
                {
                    currentRow += 1;
                    currentColumn = 0;
                }
                else { currentColumn++; }
            }
        }

        private Vector3 GetPosition(int[] coordinates)
            => new Vector3
            {
                x = ((2 * coordinates[0]) - (_colCount - 1)) * _padding.x,
                y = ((_rowCount - 1) - (2 * coordinates[1])) * _padding.y
            };
    }
}

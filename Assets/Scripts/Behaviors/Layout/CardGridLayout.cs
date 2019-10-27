using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Behaviors.Layout
{
    public class CardGridLayout : CardLayout
    {
        private Vector2 _padding;
        private int _colCount;
        private int _rowCount;

        public CardGridLayout(List<Transform> layoutElements, float[] padding, int columnCount) : base(layoutElements)
        {
            _padding = new Vector2 { x = padding[0], y = padding[1] };
            _colCount = columnCount;
        }

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

using MatchingGame.Behaviors;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Managers
{
    public class LayoutManager : MonoBehaviour
    {
        public Vector2 _padding = new Vector2 { x = 1.0f, y = 1.25f };
        public int _colCount = 1;
        private int _rowCount;

        public List<Transform> _layoutElements;

        public void Initialize() { }

        public void SetLayoutElements(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                _layoutElements.Add(card.transform);
            }

            RefreshLayout();
        }

        private void RefreshLayout()
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

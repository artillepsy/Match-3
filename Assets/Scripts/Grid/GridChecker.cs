using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        public static readonly UnityEvent<List<Cell>> OnFoundMatches = new UnityEvent<List<Cell>>();
        public static readonly UnityEvent OnNoMatchesFound = new UnityEvent();
        public GridChecker Inst { get; private set; }

        public void CheckForMatches()
        {
            var cellsToClear = new List<Cell>();

            GridCheckHelper.CheckXMatches(ref cellsToClear);
            
            GridCheckHelper.CheckYMatches(ref cellsToClear);

            if (cellsToClear.Count > 0)
            {
                OnFoundMatches?.Invoke(cellsToClear);
            }
            else OnNoMatchesFound?.Invoke();
        }

        public void FindPossibleMatches()
        {
            var x = GridContainer.Inst.X;
            var y = GridContainer.Inst.Y;
            
            var diagonalCheckArray = new int[2, 5]
            {
                { -1,  1, 1, -1, -1},
                { -1, -1, 1,  1, -1}
            };
            var crossCheckArray = new int[2, 9]
            {
                {-1, 0, 1,-1, 1, 0,-1, 1,-1},
                { 1,-1, 1, 0,-1, 1,-1, 0, 1}
            };
            
            for (var j = 1; j < y - 1; j++)
            {
                for (var i = 1; i < x - 1; i++)
                {
                    if (!GridCheckHelper.CanMatch(i, j, diagonalCheckArray)) continue;
                    
                    if (!GridCheckHelper.CanMatch(i, j, crossCheckArray)) continue;
                }
            }
        }

        // после свапа проверить на наличие матчей. Если их нет - вернуть false
        // после успешной проверки вызвать апдейт grid'a. После апдейта снова проверить,
        // но сначала на матчи, а потом на то, могут ли они быть на сцене

        private void Awake() => Inst = this;

        private void Start()
        {
            ItemMover.OnAllMoved.AddListener(CheckForMatches);
        }

        
    }
}
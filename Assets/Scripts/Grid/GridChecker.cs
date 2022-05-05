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

            GridCheckHelper.CheckXMatches(cellsToClear);
            
            GridCheckHelper.CheckYMatches(cellsToClear);

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
            var grid = GridContainer.Inst.Grid;

            for (var j = 1; j < y - 1; j++)
            {
                for (var i = 1; i < x - 1; i++)
                {
                    if (GridCheckHelper.CanMatchDiagonals(i, j))
                    {
                        // return true
                    }
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
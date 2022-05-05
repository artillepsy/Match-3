using System;
using System.Collections.Generic;
using Cells;
using UnityEngine;

namespace Grid
{
    public class GridUpdater : MonoBehaviour
    {
        public void ReformGrid()
        {
            // вызывается, когда на поле не может быть матчей
        }

        public void UpdateGrid(List<Cell> cellsToRemove)
        {
            // заполнение идёт снизу вверх. Пустышки не учитываются
            // invoke OnGridUpdated? or OnAllMoved in item mover
        }

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener(UpdateGrid);
        }
    }
}
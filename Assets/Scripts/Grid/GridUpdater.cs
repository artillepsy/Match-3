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

        public void UpdateGrid(List<Cell> cellsToClear)
        {
            foreach (var cell in cellsToClear)
            {
                var item = cell.Item;
                
                Destroy(item.gameObject);
                
                item.transform.SetParent(null);

                cell.Item = null;
            }

            var grid = GridContainer.Inst.Grid;
            
            var variants = GridContainer.Inst.Variants;
            
            



            // заполнение идёт снизу вверх. Пустышки не учитываются
            // invoke OnGridUpdated? or OnAllMoved in item mover
        }

        private void Start()
        {
            GridChecker.OnFoundMatches.AddListener(UpdateGrid);
        }
    }
}
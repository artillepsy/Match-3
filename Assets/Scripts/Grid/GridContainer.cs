using System.Collections.Generic;
using Cells;
using UnityEngine;

namespace Grid
{
    public class GridContainer : MonoBehaviour
    {
        [Header("Grid dimension")]
        [Range(6, 10)]
        [SerializeField] private int x = 6;
        [Range(6, 10)]
        [SerializeField] private int y = 6;
        
        [Header("Empty space")] 
        [SerializeField] private int emptyCellCount = 3;

        [Header("Unique items")] 
        [SerializeField] private List<CellVariant> variants;
        
        [Header("Grid creation prefabs")] 
        [SerializeField] private Transform rowsParent;
        [SerializeField] private Transform rowPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private Cell cellPrefab;
        
        private Cell[,] _grid;

        private GridChecker _gridChecker;
        
        private void Start()
        {
            _grid = new Cell[x, y];

            _gridChecker = FindObjectOfType<GridChecker>();
            
            InitGrid();
            
            InitCells();
        }

        private void InitGrid()
        {
            for (var i = 0; i < y; i++)
            {
                var row = Instantiate(rowPrefab, rowsParent);
                
                for (var j = 0; j < x; j++)
                {
                    var cell = Instantiate(cellPrefab, row);

                    _grid[x, y] = cell;
                }
            }
        }

        private void InitCells()
        {
            var counter = 0;
            
            while (counter < emptyCellCount)
            {
                var i = Random.Range(0, x);
                var j = Random.Range(0, x);
                
                if(_grid[i, j].Empty) continue;

                counter++;
            }
            
            foreach (var cell in _grid)
            {
                if(cell.Empty) continue;
                
                var variant = variants[Random.Range(0, variants.Count)];
                    
                cell.SetVariant(variant);
            }
        }
    }
}

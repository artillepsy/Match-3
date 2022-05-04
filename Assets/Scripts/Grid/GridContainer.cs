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
        [SerializeField] private GameObject rowPrefab;
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private CellButton cellPrefab;
        
        private CellButton[,] _grid;
        
        private void Start() => InitBoard();
        
        private void InitBoard()
        {
            CellButton[,] grid;
            
            for (var i = 0; i < x; i++)
            {
                
            }
                
        }
        

    }
}

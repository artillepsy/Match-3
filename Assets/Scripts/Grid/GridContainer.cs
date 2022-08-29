using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Grid
{
    /// <summary>
    /// Класс, хранящий данные массива ячеек. Имеет параметры инициализации в начале уровня.
    /// </summary>
    public class GridContainer : MonoBehaviour
    {
        [Header("Grid dimension")]
        [Range(1, 10)]
        [SerializeField] private int x = 6;
        [Range(1, 10)]
        [SerializeField] private int y = 6;
        
        [Header("Empty space")] 
        [SerializeField] private int emptyCellCount = 3;

        [Header("Unique items")] 
        [SerializeField] private List<ItemVariant> variants;
        
        [Header("Grid creation prefabs")] 
        [SerializeField] private Transform rowsParent;
        [SerializeField] private Transform rowPrefab;
        [SerializeField] private Cell cellPrefab;
        
        private Cell[,] _grid;
        public static GridContainer Inst { get; private set; }
        public Cell[,] Grid => _grid;
        public float Ystep => Mathf.Abs(_grid[0, 0].transform.position.y - _grid[0, 1].transform.position.y);
        public int X => x;
        public int Y => y;
        public ItemVariant Variant => variants[Random.Range(0, variants.Count)];
        public List<ItemVariant> Variants => variants;
        /// <summary>
        /// Возврат ячеек в виде списка
        /// </summary>
        public List<Cell> GetFilledCells()
        {
            var cellBuff = new List<Cell>();

            foreach (var cell in _grid)
            {
                if(cell.Empty) continue;
                cellBuff.Add(cell);
            }
            return cellBuff;
        }

        private void Awake() => Inst = this;

        private void Start()
        {
            _grid = new Cell[x, y];

            InitGrid();
            FillCells();
            SetEmptyCells();
            
            if (GridCheckHelper.FindPossibleMatches()) return;
            FindObjectOfType<GridReformer>().ReformGrid();
        }
        /// <summary>
        /// Создание объектов-строк и ячеек в них
        /// </summary>
        private void InitGrid()
        {
            for (var j = 0; j < y; j++)
            {
                var row = Instantiate(rowPrefab, rowsParent);
                
                for (var i = 0; i < x; i++)
                {
                    var cell = Instantiate(cellPrefab, row);
                    cell.SetPositionInGrid(i, j);
                    _grid[i, j] = cell;
                }
            }
        }
        /// <summary>
        /// Заполнение ячеек предметами с проверкой на три+ в ряд
        /// </summary>
        private void FillCells()
        {
            for (var j = 0; j < y; j++)
            {
                for (var i = 0; i < x; i++)
                {
                    if(_grid[i, j].Empty) continue;
                    var variant = GetFreeVariant(i, j);
                    _grid[i, j].InstantiateItem(variant);
                }
            }
        }
        /// <summary>
        /// Заполнение сетки пустышками
        /// </summary>
        private void SetEmptyCells()
        {
            var counter = 0;
            while (counter < emptyCellCount)
            {
                var i = Random.Range(0, x);
                var j = Random.Range(0, y);

                if (_grid[i, j].Empty) continue;

                var item = _grid[i, j].Item;
                Destroy(item.gameObject);
                _grid[i, j].Item = null;
                _grid[i, j].MakeEmpty();
                counter++;
            }
        }
        /// <summary>
        /// Взятие рандомного значения предмета с
        /// проверкой на собирание три+ в ряд
        /// </summary>
        private ItemVariant GetFreeVariant(int i, int j)
        {
            while (true)
            {
                var variant = variants[Random.Range(0, variants.Count)];
                
                if(i > 1 && _grid[i-1, j].Item.Id == _grid[i-2, j].Item.Id && _grid[i-1, j].Item.Id == variant.Id) continue;
                if(j > 1 && _grid[i, j-1].Item.Id == _grid[i, j-2].Item.Id && _grid[i, j-1].Item.Id == variant.Id) continue;
                
                return variant;
            }
        }
    }
}

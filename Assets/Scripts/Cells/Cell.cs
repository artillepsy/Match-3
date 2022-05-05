using Core;
using Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cells
{
    /// <summary>
    /// Класс, хранящийся в GridContainer. В нём изменяется лишь информация о текущем предмете
    /// </summary>
    public class Cell : MonoBehaviour
    {
        [SerializeField] private CellItem iconPrefab;
        
        public static UnityEvent<Cell> OnClickCell = new UnityEvent<Cell>();
        public CellItem Item { get; set; }
        private Button _btn;
        private bool _empty = false;
        
        private int _x;
        private int _y;

        public bool Empty => _empty;
        public int X => _x;
        public int Y => _y;
        
        public void SetVariant(ItemVariant newVariant)
        {
            Item = Instantiate(iconPrefab, transform);
            
            Item.SetVariant(newVariant);
        }

        public void SetGridPosition(int x, int y)
        {
            _x = x;
            
            _y = y;
        }
        
        public void SetEmptyStatus()
        {
            SetButtonEnableStatus(false);
            
            _empty = true;
        }

        private void Awake()
        {
            _btn = GetComponent<Button>();

            _btn.onClick.AddListener(() => OnClickCell?.Invoke(this));
        }

        private void Start()
        {
            InputListener.OnInputStatusChanged.AddListener(SetButtonEnableStatus);
        }

        private void SetButtonEnableStatus(bool status)
        {
            if (_empty) return;
  
            _btn.enabled = status;
        }
    }
}
using Inputs;
using Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cells
{
    /// <summary>
    /// Класс, хранящийся в GridContainer. В нём изменяется лишь информация о текущем предмете
    /// GO имеет компонент кнопки, является кликабельным
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
        
        /// <summary>
        /// Создание предмета в текущей ячейке
        /// </summary>
        public void InstantiateItem(ItemVariant newVariant)
        {
            Item = Instantiate(iconPrefab, transform);
            Item.SetVariant(newVariant);
        }
        /// <summary>
        /// Запоминание позиции в сетке при старте
        /// </summary>
        public void SetPositionInGrid(int x, int y)
        {
            _x = x;
            _y = y;
        }
        /// <summary>
        /// Помечание ячейки "пустышкой"
        /// </summary>
        public void MakeEmpty()
        {
            SetButtonEnabled(false);
            _empty = true;
        }
    
        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(() => OnClickCell?.Invoke(this));
        }

        private void Start() => CellButtonsListener.OnInputStatusChanged.AddListener(SetButtonEnabled);
        /// <summary>
        /// Активация и деактивация кнопки. Кнопка неактивна во время
        /// использования поля другими объектами
        /// </summary>
        private void SetButtonEnabled(bool status)
        {
            if (_empty) return;
            _btn.enabled = status;
            _btn.interactable = status;
        }
    }
}
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Cells
{
    /// <summary>
    /// Класс, хранящийся в GridContainer. В нём изменяется лишь информация о текущем тайле и хранится текущий ID
    /// </summary>
    public class Cell : MonoBehaviour
    {
        [SerializeField] private MovableIcon iconPrefab;
        
        public static UnityEvent<Cell> OnClickCell;
        private MovableIcon _icon;
        private CellVariant _variant;
        private Button _btn;
        private bool _empty = false;
        private int _id;
        private int _x;
        private int _y;

        public bool Empty => _empty;

        public int Id => _id;

        public int X => _x;
        public int Y => _y;
        
        public void SetVariant(CellVariant newVariant)
        {
            _variant = newVariant;

            _id = newVariant.Id;
            
            _icon = Instantiate(iconPrefab, transform);
            
            _icon.SetColor(newVariant.CellColor);
            
            Debug.Log("New variant setted. Id: "+ newVariant.Id);
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            
            _y = y;
        }

        public void SetEmpty()
        {
            _empty = true;
            
            InputListener.OnInputSettingsChanged.RemoveListener(SetButtonEnableStatus);
            
            SetButtonEnableStatus(false);
        }

        private void Awake()
        {
            OnClickCell = new UnityEvent<Cell>();
            
            _btn = GetComponent<Button>();
            
            _btn.onClick.AddListener(() => OnClickCell?.Invoke(this));
        }

        private void Start()
        {
            InputListener.OnInputSettingsChanged.AddListener(SetButtonEnableStatus);
        }

        private void SetButtonEnableStatus(bool status) => _btn.enabled = status;
    }
}
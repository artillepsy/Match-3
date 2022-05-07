using System.Collections;
using Cells;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Items
{
    /// <summary>
    /// Класс кликабельной ячейки. Содержит в себе объект с
    /// уникальным идентификатором
    /// </summary>
    public class CellItem : MonoBehaviour
    {
        [SerializeField] private float speed = 500f;
        
        public static UnityEvent OnMoved = new UnityEvent();
        private Vector2 _translation;
        private Vector2 _endPosition;
        private int _id;
        
        public int Id => _id;
        /// <summary>
        /// Создание предмета с заданными данными
        /// внутри себя
        /// </summary>
        public void SetVariant(ItemVariant variant)
        {
            var image = GetComponent<Image>();
            image.color = variant.ItemColor;
            _id = variant.Id;
        }
        /// <summary>
        /// Движение к позиции указанной ячейки
        /// </summary>
        public void MoveToCell(Cell cell)
        {
            _endPosition = cell.transform.position;
            transform.SetParent(cell.transform);
            StartCoroutine(MoveCO());
        }
        /// <summary>
        /// Движение с константной скоростью,
        /// пока текущая позиция не будет равна заданной.
        /// Скорость не зависит от FPS
        /// </summary>
        private IEnumerator MoveCO()
        {
            while (true)
            {
               var pos = Vector2.MoveTowards(
                   transform.position, 
                   _endPosition, 
                   speed * Time.deltaTime);

               transform.position = pos;
               if (pos == _endPosition)  break;
               yield return null;
            }
            OnMoved?.Invoke();
        }
    }
}
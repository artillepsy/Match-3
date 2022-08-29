using System.Collections.Generic;
using Cells;
using Inputs;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    /// <summary>
    /// Класс, проверяющий сетку с ячейками после перемещения предметов
    /// </summary>
    public class GridChecker : MonoBehaviour
    {
        [SerializeField] private float delayOnNoPossibleMatches = 3f;
        private CellButtonsListener _listener;
        public static readonly UnityEvent<List<Cell>> OnFoundMatches = new UnityEvent<List<Cell>>();
        public static readonly UnityEvent<float> OnNoPossibleMatchesFound = new UnityEvent<float>();
        public static readonly UnityEvent OnNoMatchesFound = new UnityEvent();

        /// <summary>
        /// Проверка сетки на три и более в ряд.
        /// Если есть три+ в ряд, то вызывается ивент, передающий
        /// ячейки в качестве параметров. Если три в ряд нет, то идёт проверка
        /// на возможные три+ в ряд. В результате успеха или провала проверки
        /// вызываются соответствующие ивенты
        /// </summary>
        private void CheckForMatches()
        {
            var cellsToClear = new List<Cell>();

            GridCheckHelper.CheckXMatches(ref cellsToClear);
            GridCheckHelper.CheckYMatches(ref cellsToClear);

            if (cellsToClear.Count > 0) OnFoundMatches?.Invoke(cellsToClear);
            else if (GridCheckHelper.FindPossibleMatches()) OnNoMatchesFound?.Invoke();
            else if(!_listener.ShouldUndo) OnNoPossibleMatchesFound?.Invoke(delayOnNoPossibleMatches);
        }

        private void Start()
        {
            _listener = FindObjectOfType<CellButtonsListener>();
            ItemMover.OnAllMoved.AddListener(CheckForMatches);
            GridReformer.OnReformedToNoPossibleMatches.AddListener(CheckForMatches);
        }
    }
}
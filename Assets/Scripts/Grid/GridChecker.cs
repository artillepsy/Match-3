using System.Collections.Generic;
using Cells;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Grid
{
    public class GridChecker : MonoBehaviour
    {
        [SerializeField] private float delayOnNoPossibleMatches = 3f;
        public static readonly UnityEvent<List<Cell>> OnFoundMatches = new UnityEvent<List<Cell>>();
        public static readonly UnityEvent OnNoMatchesFound = new UnityEvent();
        public static readonly UnityEvent<float> OnNoPossibleMatchesFound = new UnityEvent<float>();

        private void CheckForMatches()
        {
            var cellsToClear = new List<Cell>();

            GridCheckHelper.CheckXMatches(ref cellsToClear);
            GridCheckHelper.CheckYMatches(ref cellsToClear);

            if (cellsToClear.Count > 0)  OnFoundMatches?.Invoke(cellsToClear);
            else if (GridCheckHelper.FindPossibleMatches()) OnNoMatchesFound?.Invoke();
            else OnNoPossibleMatchesFound?.Invoke(delayOnNoPossibleMatches);
        }

        private void Start()
        {
            ItemMover.OnAllMoved.AddListener(CheckForMatches);
            GridReformer.OnReformedToNoPossibleMatches.AddListener(CheckForMatches);
        }
    }
}
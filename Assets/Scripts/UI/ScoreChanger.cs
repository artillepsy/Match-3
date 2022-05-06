using System.Collections.Generic;
using System.Linq;
using Cells;
using Grid;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreChanger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountLabel;
        private int _totalScore = 0;
        private void Start() => GridChecker.OnFoundMatches.AddListener(CalculateScoreAmount);

        private void CalculateScoreAmount(List<Cell> cells)
        {
            var remainingCells = cells;
            while (remainingCells.Count != 0)
            {
                var id = remainingCells[0].Item.Id;
                var cellsToRemove = remainingCells.Where(cell => cell.Item.Id == id).ToList();

                switch (cellsToRemove.Count)
                {
                    case 3:
                        _totalScore += 10;
                        break;
                    case 4:
                        _totalScore += 15;
                        break;
                    case 5:
                        _totalScore += 20;
                        break;
                }
                remainingCells = remainingCells.Except(cellsToRemove).ToList();
            }
            amountLabel.text = _totalScore.ToString();
        }
    }
}
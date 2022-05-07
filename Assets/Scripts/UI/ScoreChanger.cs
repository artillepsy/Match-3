using System.Collections.Generic;
using System.Linq;
using Grid;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Класс, инкрементирующий счетчик в случае собирания три+ в ряд
    /// </summary>
    public class ScoreChanger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI amountLabel;
        private int _totalScore = 0;
        private void Start()
        {
            GridUpdater.OnCachedClearedIds.AddListener(CalculateScoreAmount);
        }
        /// <summary>
        /// ПОдсчитывание счёта, исходя из собранных
        /// id предметов
        /// </summary>
        private void CalculateScoreAmount(List<int> ids)
        {
            var remainingIds = ids;
            while (remainingIds.Count != 0)
            {
                var totalId = remainingIds[0];
                var idsToCalculate = remainingIds.Where(id => id == totalId).ToList();

                switch (idsToCalculate.Count)
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
                remainingIds = remainingIds.Except(idsToCalculate).ToList();
            }
            amountLabel.text = _totalScore.ToString();
        }
    }
}
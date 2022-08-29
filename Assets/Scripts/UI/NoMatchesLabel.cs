using Grid;
using TMPro;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Класс, показывающий надпись в случае отсутствия возможных ходов
    /// </summary>
    public class NoMatchesLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI noMatchesLabel;
        private void Start()
        {
            noMatchesLabel.enabled = false;
            GridChecker.OnNoPossibleMatchesFound.AddListener((delay) =>
            {
                noMatchesLabel.enabled = true;
                Invoke(nameof(DisableLabel), delay);
            });
        }
        
        private void DisableLabel() => noMatchesLabel.enabled = false;
    }
}
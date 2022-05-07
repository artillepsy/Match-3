using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Inputs
{
    /// <summary>
    /// Класс, содержащий обработчики нажатий на
    /// кнопки тестирования игры
    /// </summary>
    public class InputCanvasButtons : MonoBehaviour
    {
        [SerializeField] private Button reformBtn;
        
        private GridReformer _gridReformer;
        private bool _inputStatus = true;
        /// <summary>
        /// Перезагрузка текущего уровня. Сброс счёта
        /// </summary>
        public void OnClickRestartLevel()
        {
            SceneManager.LoadSceneAsync("Game");
        }
        /// <summary>
        /// Реформирование сетки таким образом,
        /// чтобы в ней не было доступных ходов
        /// для собирания три+ в ряд элементов.
        /// В данном слуае сетка заполнится новыми
        /// предметами
        /// </summary>
        public void OnClickReformWithNoMatches()
        {
            if (!_inputStatus) return;
            _gridReformer.ReformGridWithNoPossibleMatches();
        }

        public void OnLickExit() => Application.Quit();

        private void Start()
        {
            _gridReformer = FindObjectOfType<GridReformer>();
            CellButtonsListener.OnInputStatusChanged.AddListener((status) =>
            {
                _inputStatus = status;
                reformBtn.interactable = status;
            });
        }
    }
}
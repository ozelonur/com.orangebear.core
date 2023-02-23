using System.Linq;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace OrangeBear.Core
{
    public abstract class UIBear : Bear
    {
        #region Serialized Fields

        #region Panels

        [Header("Panels")] [SerializeField] private PanelData[] panels;

        #endregion

        #region Buttons

        [Header("Buttons")] [SerializeField] private Button startButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button nextButton;

        #endregion

        #endregion

        #region MonoBehaviour Methods

        protected virtual void Awake()
        {
            startButton.onClick.AddListener(StartGame);
            retryButton.onClick.AddListener(RetryGame);
            nextButton.onClick.AddListener(NextLevel);
            
            Activate(PanelEnums.MainMenu);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
                Register(GameEvents.ActivatePanel, ActivatePanel);
                Register(GameEvents.GetLevelNumber, GetLevelNumber);
            }

            else
            {
                Unregister(GameEvents.InitLevel, InitLevel);
                Unregister(GameEvents.ActivatePanel, ActivatePanel);
                Unregister(GameEvents.GetLevelNumber, GetLevelNumber);
            }
        }

        protected virtual void GetLevelNumber(object[] arguments)
        {
            
        }

        private void ActivatePanel(object[] arguments)
        {
            PanelEnums panelType = (PanelEnums) arguments[0];
            
            Activate(panelType);
        }

        protected virtual void InitLevel(object[] arguments)
        {
            Activate(PanelEnums.MainMenu);
        }

        #endregion

        #region Protected Methods

        protected virtual void StartGame()
        {
            Activate(PanelEnums.Game);
            Roar(GameEvents.OnGameStart);
        }

        protected virtual void RetryGame()
        {
            NextLevel();
        }

        protected virtual void NextLevel()
        {
            Roar(GameEvents.NextLevel);
        }

        #endregion

        #region Private Methods

        private void Activate(PanelEnums panelType)
        {
            panels.Where(panel => panel.panelType != panelType).ToList().ForEach(panel => panel.panel.SetActive(false));
            panels.FirstOrDefault(panel => panel.panelType == panelType)?.panel.SetActive(true);
        }

        #endregion
    }
}

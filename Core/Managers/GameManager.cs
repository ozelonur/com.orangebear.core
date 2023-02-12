using OrangeBear.EventSystem;

namespace OrangeBear.Core
{
    public class GameManager : Manager<GameManager>
    {
        #region Public Variables

        public bool IsGameStarted;
        public bool IsGameCompleted;

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.OnGameComplete, OnGameComplete);
                Register(GameEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Unregister(GameEvents.OnGameComplete, OnGameComplete);
                Unregister(GameEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] arguments)
        {
            IsGameStarted = true;
            IsGameCompleted = false;
        }

        private void OnGameComplete(object[] arguments)
        {
            IsGameCompleted = true;
            
            bool status = (bool)arguments[0];
            
            Roar(GameEvents.ActivatePanel, status ? PanelEnums.GameWin : PanelEnums.GameOver);
        }

        #endregion
    }
}
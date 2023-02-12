using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Core
{
    public class LevelManager : Manager<LevelManager>
    {
        #region Serialized Fields

        [Header("Levels")]
        [SerializeField] private Level[] levels;

        #endregion

        #region Private Variables

        private Level _level;
        private GameObject _tempLevel;
        private int _levelCount;

        private int LevelIndex
        {
            get => PlayerPrefs.GetInt(GlobalStrings.LevelIndex, 0);
            set => PlayerPrefs.SetInt(GlobalStrings.LevelIndex, value);
        }
        
        private int LevelCount
        {
            get => PlayerPrefs.GetInt(GlobalStrings.LevelCount, 1);
            set => PlayerPrefs.SetInt(GlobalStrings.LevelCount, value);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            CreateLevel();
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(GameEvents.InitLevel, InitLevel);
                Register(GameEvents.OnGameComplete, OnGameComplete);
                Register(GameEvents.NextLevel, NextLevel);
            }

            else
            {
                Unregister(GameEvents.InitLevel, InitLevel);
                Unregister(GameEvents.OnGameComplete, OnGameComplete);
                Unregister(GameEvents.NextLevel, NextLevel);
            }
        }

        private void NextLevel(object[] arguments)
        {
            CreateLevel();
        }

        private void InitLevel(object[] arguments)
        {
            Roar(GameEvents.GetLevelNumber, LevelCount);
        }

        private void OnGameComplete(object[] arguments)
        {
            bool status = (bool)arguments[0];

            if (!status)
            {
                return;
            }
            
            LevelIndex++;
            LevelCount++;
        }

        #endregion

        #region Private Methods

        private void CreateLevel()
        {
            if (_tempLevel != null)
            {
                Destroy(_tempLevel);
            }

            if (LevelIndex >= levels.Length)
            {
                LevelIndex = 0;
            }
            
            InstantiateLevel();
        }

        private void InstantiateLevel()
        {
            _level = levels[LevelIndex];
            _tempLevel = Instantiate(_level.LevelPrefab);
            _tempLevel.GetComponent<LevelBear>().InitLevel();
            
        }

        #endregion
    }
}
using OrangeBear.EventSystem;
using UnityEngine;

namespace OrangeBear.Core
{
    public class Manager<T> : Bear where T : Bear
    {
        #region Private Variables

        private static T _instance;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError("No instance of " + typeof(T) + " found!");
                }

                return _instance;
            }
        }

        #endregion
    }
}
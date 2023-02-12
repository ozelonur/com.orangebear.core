using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OrangeBear.EventSystem
{ 
    [DefaultExecutionOrder(-1001)]
    public class BearManager : MonoBehaviour
    {
        #region Public Variables

        public static BearManager Instance;

        #endregion
        
        #region Private Variables

        private Dictionary<string, List<Roaring>> _events = new();

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        #endregion

        #region Public Methods

        public void Roar(string roarName, params object[] arguments)
        {
            if (!_events.ContainsKey(roarName))
            {
                return;
            }

            List<KeyValuePair<string, List<Roaring>>> events = _events.Where(x => x.Key == roarName).ToList();

            foreach (Roaring roaring in events.Select(myEvent => myEvent.Value.ToList())
                         .SelectMany(roarings => roarings))
            {
                roaring?.Invoke(arguments);
            }
        }

        public void Register(string roarName, Roaring roaring)
        {
            List<Roaring> roarings;

            if (!_events.ContainsKey(roarName))
            {
                roarings = new List<Roaring> { roaring };
                
                _events.Add(roarName, roarings);
            }

            else
            {
                roarings = _events[roarName];

                roarings.Add(roaring);
            }
        }

        public void Unregister(string roarName, Roaring roaring)
        {
            List<Roaring> roarings = _events[roarName];

            if (roarings.Count > 0)
            {
                roarings.Remove(roaring);
            }
            
            if (roarings.Count == 0)
            {
                _events.Remove(roarName);
            }
        }

        #endregion
    }
}

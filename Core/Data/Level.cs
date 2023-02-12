using UnityEngine;

namespace OrangeBear.Core
{
    [CreateAssetMenu(fileName = "Level", menuName = "Orange Bear / Level", order = 1)]
    public class Level : ScriptableObject
    {
        [SerializeField] private GameObject levelPrefab;

        public GameObject LevelPrefab
        {
            get => levelPrefab;
            set => levelPrefab = value;
        }
    }
}
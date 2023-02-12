using UnityEditor;
using UnityEngine;

namespace OrangeBear.Core
{
    public class OrangeBearEditorMenu : MonoBehaviour
    {
        [MenuItem("Orange Bear / Clear All PlayerPrefs", priority = 0)]
        private static void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log($"All Player Prefs cleared!");
        }
    }
}
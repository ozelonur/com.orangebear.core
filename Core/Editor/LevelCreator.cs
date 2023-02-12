using System.IO;
using UnityEditor;
using UnityEngine;

namespace OrangeBear.Core
{
    public class LevelCreator : EditorWindow
    {
        #region Fields

        private GameObject _level;
        private Level _levelData;

        private DirectoryInfo _directoryInfo;

        private int _count;

        private SerializedObject _serializedObject;
        private SerializedProperty _serializedProperty;

        #endregion

        #region Serialized Fields

        [SerializeField] private int levelCount;

        #endregion

        #region Editor Window Methods

        [MenuItem("Orange Bear / Level Creator")]
        public static void OpenLevelCreatorWindow()
        {
            GetWindow<LevelCreator>("Level Creator");
        }

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(this);
            _serializedProperty = _serializedObject.FindProperty("levelCount");
        }

        private void OnGUI()
        {
            _serializedObject.Update();

            GUILayout.Label("Level Prefab", EditorStyles.boldLabel);
            _level = (GameObject)EditorGUILayout.ObjectField(_level, typeof(GameObject), false);

            GUILayout.Label("Level Data", EditorStyles.boldLabel);
            _levelData = (Level)EditorGUILayout.ObjectField(_levelData, typeof(Level), false);

            GUILayout.Label("Level Count", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_serializedProperty, true);

            _serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Create Levels"))
            {
                CreateLevels();
            }
        }

        #endregion

        #region Private Methods

        private void CreateLevels()
        {
            for (int i = 0; i < levelCount; i++)
            {
                Create();
            }
        }

        private void Create()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Assets/[GAME]/Levels");

            FileInfo[] fileInfo = directoryInfo.GetFiles();

            _count = fileInfo.Length;

            if (_count == 1)
            {
                _count = 1;
            }

            else
            {
                _count = ((_count - 1) / 4) + 1;
            }

            GameObject levelReference = (GameObject)PrefabUtility.InstantiatePrefab(_level);
            
            GameObject prefabVariant = PrefabUtility.SaveAsPrefabAsset(levelReference, $"Assets/[GAME]/Levels/_Level {_count}.prefab");
            
            DestroyImmediate(levelReference);

            string dataPath = AssetDatabase.GetAssetPath(_levelData.GetInstanceID());
            
            AssetDatabase.CopyAsset(dataPath, $"Assets/[GAME]/Levels/Level {_count}.asset");
            
            Level asset = AssetDatabase.LoadAssetAtPath<Level>($"Assets/[GAME]/Levels/Level {_count}.asset");
            
            asset.LevelPrefab = prefabVariant;
            
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }

        #endregion
    }
}
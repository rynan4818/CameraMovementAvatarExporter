#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
public class MissingAllDelete
{
    [MenuItem("CameraMovement/MissingAllDelete/Scene", false, 3)]
    public static void SceneMissingAllDelete()
    {
        foreach (var targetGameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(targetGameObject);
        Debug.Log("Missing scripts in the scene have been removed.");
    }
    [MenuItem("CameraMovement/MissingAllDelete/Prefab", false, 3)]
    public static void PrefabMissingAllDelete()
    {
        foreach (var targetGUID in AssetDatabase.FindAssets("t:prefab"))
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(targetGUID)));
        AssetDatabase.Refresh();
        Debug.Log("Missing scripts in all prefabs have been removed.");
    }
}
#endif

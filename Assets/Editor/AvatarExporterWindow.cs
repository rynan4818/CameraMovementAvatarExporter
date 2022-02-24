﻿//  Beat Saber Custom Avatars - Custom player models for body presence in Beat Saber.
//  Copyright © 2018-2021  Nicolas Gnyra and Beat Saber Custom Avatars Contributors
//
//  This library is free software: you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation, either
//  version 3 of the License, or (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class CameraMovementAvatarExporterWindow : EditorWindow
{
     private GameObject avatar;
    
    [MenuItem("CameraMovement/Avatar Exporter")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CameraMovementAvatarExporterWindow), false, "CameraMovement Avatar Exporter");
    }

    #region Behaviour Lifecycle

    internal void OnFocus()
    {
        avatar = Selection.activeGameObject;
        Repaint();
    }

    internal void OnGUI()
    {
        var titleLabelStyle = new GUIStyle(EditorStyles.largeLabel)
        {
            fontSize = 16
        };

        if (avatar != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            GUILayout.Label(avatar.name, titleLabelStyle);

            EditorGUILayout.LabelField("Game Object: ", avatar.gameObject.name);

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Export " + avatar.name))
            {
                SaveAvatar(avatar);
            }

            GUILayout.Space(20);
        }
    }

    #endregion

    private void SaveAvatar(GameObject avatar)
    {
        string destinationPath = EditorUtility.SaveFilePanel("Save avatar file", null, avatar.name + ".avatar", "avatar");

        if (string.IsNullOrEmpty(destinationPath)) return;

        string destinationFileName = Path.GetFileName(destinationPath);
        string tempFolder = Application.temporaryCachePath;
        string prefabPath = Path.Combine("Assets", "_CustomAvatar.prefab");

        PrefabUtility.SaveAsPrefabAsset(avatar.gameObject, prefabPath);

        var assetBundleBuild = new AssetBundleBuild
        {
            assetBundleName = destinationFileName,
            assetNames = new[] { prefabPath }
        };

        assetBundleBuild.assetBundleName = destinationFileName;

        BuildTargetGroup selectedBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;

        AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(tempFolder, new[] { assetBundleBuild }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        // switch back to what it was before creating the asset bundle
        EditorUserBuildSettings.SwitchActiveBuildTarget(selectedBuildTargetGroup, activeBuildTarget);

        AssetDatabase.DeleteAsset(prefabPath);
        AssetDatabase.Refresh();

        if (!manifest)
        {
            EditorUtility.DisplayDialog("Export Failed", "Failed to create asset bundle! Please check the Unity console for more information.", "OK");
            return;
        }

        string[] assetBundleNames = manifest.GetAllAssetBundles();
        string tempAssetBundlePath = Path.Combine(tempFolder, assetBundleNames[0]);

        try
        {
            File.Copy(tempAssetBundlePath, destinationPath, true);

            EditorUtility.DisplayDialog("Export Successful!", $"{avatar.name} was exported successfully!", "OK");
        }
        catch (IOException ex)
        {
            Debug.LogError(ex);

            EditorUtility.DisplayDialog("Export Failed", $"Could not copy avatar to selected folder. Please check the Unity console for more information.", "OK");
        }
    }
}
#endif

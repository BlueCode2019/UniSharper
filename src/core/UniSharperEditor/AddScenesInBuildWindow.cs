/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2018 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Displays the window for adding scenes in build.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class AddScenesInBuildWindow : EditorWindow
    {
        #region Fields

        private List<SceneAsset> sceneAssets = new List<SceneAsset>();
        private Vector2 scrollPosition;

        #endregion Fields

        #region Methods

        [MenuItem("Tools/UniSharper/Build Settings.../Add Scenes In Build", false, MenuItemPriorities.BuildSettingsMenuItemsPriority)]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            GetWindow(typeof(AddScenesInBuildWindow), true, "Add Scenes In Build");
        }

        public void SetEditorBuildSettingsScenes()
        {
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (var sceneAsset in sceneAssets)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (!string.IsNullOrEmpty(scenePath))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Scenes to include in build:", EditorStyles.boldLabel);
            for (int i = 0; i < sceneAssets.Count; ++i)
            {
                sceneAssets[i] = (SceneAsset)EditorGUILayout.ObjectField(sceneAssets[i], typeof(SceneAsset), false);
            }
            if (GUILayout.Button("Add"))
            {
                sceneAssets.Add(null);
            }

            GUILayout.Space(8);

            if (GUILayout.Button("Apply To Build Settings"))
            {
                SetEditorBuildSettingsScenes();
            }

            GUILayout.EndScrollView();
        }

        #endregion Methods
    }
}
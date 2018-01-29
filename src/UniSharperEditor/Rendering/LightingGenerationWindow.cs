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
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniSharperEditor.Rendering
{
    /// <summary>
    /// Displays the window for generating lightmap for adding scenes.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class LightingGenerationWindow : EditorWindow
    {
        #region Fields

        private List<SceneAsset> scenes = new List<SceneAsset>();
        private Vector2 scrollPosition;

        #endregion Fields

        #region Methods

        [MenuItem("Tools/UniSharper/Rendering/Generate Lighting for Scenes", false, MenuItemPriorities.RenderingMenuItemsPriority + 1)]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            LightingGenerationWindow window = GetWindow<LightingGenerationWindow>(true, "Lighting Generation", true);
            window.position = new Rect(200, 200, 500, 500);
        }

        private void GenerateLighting()
        {
            List<string> scenePaths = new List<string>();

            foreach (var sceneAsset in scenes)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);

                if (!string.IsNullOrEmpty(scenePath))
                {
                    scenePaths.Add(scenePath);
                }
            }

            for (int i = 0, length = scenePaths.Count; i < length; i++)
            {
                string scenePath = scenePaths[i];
                Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                EditorUtility.DisplayProgressBar("Baking...", string.Format("Baking the scene {0}... {1}/{2}", scene.name, i + 1, length), (float)(i + 1) / length);
                UnityEditor.Lightmapping.Bake();
                EditorSceneManager.SaveScene(scene);
            }

            EditorUtility.ClearProgressBar();
        }

        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Scenes to generate lighting:", EditorStyles.boldLabel);
            for (int i = 0; i < scenes.Count; ++i)
            {
                scenes[i] = (SceneAsset)EditorGUILayout.ObjectField(scenes[i], typeof(SceneAsset), false);
            }
            if (GUILayout.Button("Add"))
            {
                scenes.Add(null);
            }

            GUILayout.Space(8);

            if (GUILayout.Button("Generate Lighting for Scenes"))
            {
                GenerateLighting();
            }

            GUILayout.EndScrollView();
        }

        #endregion Methods
    }
}
/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
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

using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// <see cref="EditorWindow"/> of about.
    /// </summary>
    internal class AboutWindow : EditorWindow
    {
        /// <summary>
        /// The menu item priority.
        /// </summary>
        public const int MenuItemPriority = int.MaxValue;

        [MenuItem("Tools/UniSharper/Help/About UniSharper...", false, MenuItemPriority)]
        private static void ShowAboutWindow()
        {
            AboutWindow windowWithRect = GetWindowWithRect<AboutWindow>(new Rect(100f, 100f, 230f, 150f), true, "About UniSharper");
            windowWithRect.position = new Rect(200f, 200f, 570f, 340f);
        }

        #region Messages

        private void OnGUI()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space(20f);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            GUILayout.Label("UniSharper", new GUIStyle() { fontStyle = FontStyle.Bold, fontSize = 30, normal = new GUIStyleState() { textColor = Color.white } });
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.Space(110f);
            GUILayout.Label(string.Format("Version {0}.{1}", Version.MajorVersion, Version.MinorVersion));
            GUILayout.EndHorizontal();
            GUILayout.Space(30f);
            GUILayout.Label("Copyright (c) 2017 Jerry Lee");
            GUILayout.Label("cosmos53076@163.com");
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);
        }

        #endregion Messages
    }
}
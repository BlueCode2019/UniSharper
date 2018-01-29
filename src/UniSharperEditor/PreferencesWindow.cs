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
    /// Displays the preference options.
    /// </summary>
    /// <seealso cref="EditorWindow"/>
    internal class PreferencesWindow : EditorWindow
    {
        #region Fields

        private static Constants constants = null;

        private bool refreshCustomPreferences;

        private Vector2 scrollPosition;

        private List<Section> sections;

        private Vector2 sectionScrollPos;

        private int selectedSectionIndex;

        #endregion Fields

        #region Delegates

        private delegate void OnUIDraw();

        #endregion Delegates

        #region Properties

        private Section SelectedSection
        {
            get
            {
                return sections[selectedSectionIndex];
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Shows the preferences window.
        /// </summary>
        [MenuItem("Tools/UniSharper/Preferences...", false, MenuItemPriorities.PreferencesMenuItemPriority)]
        private static void ShowPreferencesWindow()
        {
            EditorWindow window = GetWindow<PreferencesWindow>(true, "UniSharper Preferences");
            window.minSize = new Vector2(500f, 400f);
            window.maxSize = new Vector2(window.minSize.x, window.maxSize.y);
            window.position = new Rect(new Vector2(100f, 100f), window.minSize);
        }

        private void AddCustomSections()
        {
        }

        private void OnEnable()
        {
            sections = new List<Section>();
            sections.Add(new Section("Auto Save", ShowAutoSave));
            refreshCustomPreferences = true;
        }

        private void OnGUI()
        {
            if (refreshCustomPreferences)
            {
                AddCustomSections();
                refreshCustomPreferences = false;
            }

            EditorGUIUtility.labelWidth = 200f;

            if (constants == null)
            {
                constants = new Constants();
            }

            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            sectionScrollPos = GUILayout.BeginScrollView(sectionScrollPos, constants.SectionScrollView, new GUILayoutOption[]
            {
                GUILayout.Width(120f)
            });
            GUILayout.Space(40f);

            for (int i = 0; i < sections.Count; i++)
            {
                Section section = sections[i];
                Rect rect = GUILayoutUtility.GetRect(section.Content, constants.SectionElement, new GUILayoutOption[]
                {
                    GUILayout.ExpandWidth(true)
                });

                if (section == SelectedSection && Event.current.type == EventType.Repaint)
                {
                    constants.Selected.Draw(rect, false, false, false, false);
                }
                EditorGUI.BeginChangeCheck();

                if (GUI.Toggle(rect, this.selectedSectionIndex == i, section.Content, constants.SectionElement))
                {
                    this.selectedSectionIndex = i;
                }
                if (EditorGUI.EndChangeCheck())
                {
                    GUIUtility.keyboardControl = 0;
                }
            }

            GUILayout.EndScrollView();
            GUILayout.Space(10f);
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            GUILayout.Label(SelectedSection.Content, constants.SectionHeader, new GUILayoutOption[0]);
            GUILayout.Space(10f);
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, new GUILayoutOption[0]);
            SelectedSection.UIDrawCallback.Invoke();
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void ShowAutoSave()
        {
            // AutoSave toggle button.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            AutoSave.Instance.IsAutoSaveEnabled = EditorGUILayout.Toggle(AutoSave.TextStyles.IsAutoSaveEnabledStyle, AutoSave.Instance.IsAutoSaveEnabled);
            GUILayout.EndVertical();

            EditorGUI.BeginDisabledGroup(!AutoSave.Instance.IsAutoSaveEnabled);

            // AutoSave scenes toggle button.
            GUILayout.BeginVertical();
            AutoSave.Instance.IsAutoSaveScenesEnabled = EditorGUILayout.Toggle("Save Scenes", AutoSave.Instance.IsAutoSaveScenesEnabled);
            GUILayout.EndVertical();

            // AutoSave assets toggle button.
            GUILayout.BeginVertical();
            AutoSave.Instance.IsAutoSaveAssetsEnabled = EditorGUILayout.Toggle("Save Assets", AutoSave.Instance.IsAutoSaveAssetsEnabled);
            GUILayout.EndVertical();

            // Draws property AutoSaveTimeMinutes of the instance of class AutoSave.
            GUILayout.BeginVertical();
            int value = EditorGUILayout.IntField(AutoSave.TextStyles.FrequencyInMinutesStyle, (int)AutoSave.Instance.AutoSaveTimeMinutes);

            if (value >= 1)
            {
                AutoSave.Instance.AutoSaveTimeMinutes = (uint)value;
            }
            else
            {
                AutoSave.Instance.AutoSaveTimeMinutes = 1;
            }

            GUILayout.EndVertical();

            // Draws property AskWhenSaving of the instance of class AutoSave.
            GUILayout.BeginVertical();
            AutoSave.Instance.AskWhenSaving = EditorGUILayout.Toggle(AutoSave.TextStyles.AskWhenSavingStyle, AutoSave.Instance.AskWhenSaving);
            GUILayout.EndVertical();

            EditorGUI.EndDisabledGroup();
        }

        #endregion Methods

        #region Classes

        internal class Constants
        {
            #region Fields

            public GUIStyle CacheFolderLocation = new GUIStyle(GUI.skin.label);
            public GUIStyle ErrorLabel = "WordWrappedLabel";
            public GUIStyle EvenRow = "CN EntryBackEven";
            public GUIStyle KeysElement = "PreferencesKeysElement";
            public GUIStyle OddRow = "CN EntryBackOdd";
            public GUIStyle SectionElement = "PreferencesSection";
            public GUIStyle SectionHeader = new GUIStyle(EditorStyles.largeLabel);
            public GUIStyle SectionScrollView = "PreferencesSectionBox";

            public GUIStyle Selected = "ServerUpdateChangesetOn";
            public GUIStyle SettingsBox = "OL Box";
            public GUIStyle SettingsBoxTitle = "OL Title";
            public GUIStyle WarningIcon = "CN EntryWarn";

            #endregion Fields

            #region Constructors

            public Constants()
            {
                SectionScrollView = new GUIStyle(SectionScrollView);
                SectionScrollView.overflow.bottom++;
                SectionHeader.fontStyle = FontStyle.Bold;
                SectionHeader.fontSize = 18;
                SectionHeader.margin.top = 10;
                SectionHeader.margin.left++;

                if (!EditorGUIUtility.isProSkin)
                {
                    SectionHeader.normal.textColor = new Color(0.4f, 0.4f, 0.4f, 1f);
                }
                else
                {
                    SectionHeader.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1f);
                }

                CacheFolderLocation.wordWrap = true;
            }

            #endregion Constructors
        }

        private class Section
        {
            #region Constructors

            public Section(string name, OnUIDraw uiDrawCallback = null)
            {
                Content = new GUIContent(name);
                UIDrawCallback = uiDrawCallback;
            }

            public Section(GUIContent content, OnUIDraw uiDrawCallback = null)
            {
                Content = content;
                UIDrawCallback = uiDrawCallback;
            }

            #endregion Constructors

            #region Properties

            public GUIContent Content
            {
                get;
                private set;
            }

            public OnUIDraw UIDrawCallback
            {
                get;
                private set;
            }

            #endregion Properties
        }

        #endregion Classes
    }
}
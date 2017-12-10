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

using ReSharp.Patterns;
using System;
using UniSharper.Timers;
using UniSharperEditor.Timers;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// <see cref="AutoSave"/> to implement the feature of saving current opening scene automatically.
    /// </summary>
    /// <seealso cref="ReSharp.Patterns.Singleton{AutoSave}"/>
    [InitializeOnEditorStartup]
    internal class AutoSave : Singleton<AutoSave>
    {
        #region Fields

        /// <summary>
        /// The configuration section.
        /// </summary>
        private const string configSection = "AutoSave";

        /// <summary>
        /// Whether to show confirm dialog when autosave.
        /// </summary>
        private bool askWhenSaving = true;

        /// <summary>
        /// The time interval after which to autosave.
        /// </summary>
        private uint autoSaveTimeMinutes = 10;

        /// <summary>
        /// The timer of autosave.
        /// </summary>
        private EditorTimer autosaveTimer;

        /// <summary>
        /// Whether initialize is complete.
        /// </summary>
        private bool initialized = false;

        /// <summary>
        /// Whether to save assets automatically.
        /// </summary>
        private bool isAutoSaveAssetsEnabled = true;

        /// <summary>
        /// Whether to enable AutoSave feature.
        /// </summary>
        private bool isAutoSaveEnabled = false;

        /// <summary>
        /// Whether to auto save scenes.
        /// </summary>
        private bool isAutoSaveScenesEnabled = true;

        /// <summary>
        /// Whether the data is dirty.
        /// </summary>
        private bool isDirty = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="AutoSave"/> class.
        /// </summary>
        static AutoSave()
        {
            Instance.Initialize();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AutoSave"/> class from being created.
        /// </summary>
        private AutoSave()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [show confirm dialog].
        /// </summary>
        /// <value><c>true</c> if [show confirm dialog]; otherwise, <c>false</c>.</value>
        public bool AskWhenSaving
        {
            get
            {
                if (!initialized)
                {
                    askWhenSaving = EditorConfig.GetConfigValue(configSection, ConfigKeys.AskWhenSavingKey, true);
                }

                return askWhenSaving;
            }

            set
            {
                if (askWhenSaving != value)
                {
                    askWhenSaving = value;
                    EditorConfig.SetConfigValue(configSection, ConfigKeys.AskWhenSavingKey, value);
                    isDirty = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets the automatic save time minutes.
        /// </summary>
        /// <value>The automatic save time minutes.</value>
        public uint AutoSaveTimeMinutes
        {
            get
            {
                if (!initialized)
                {
                    autoSaveTimeMinutes = EditorConfig.GetConfigValue<uint>(configSection, ConfigKeys.AutoSaveTimeMinutesKey, 10);
                }

                return autoSaveTimeMinutes;
            }

            set
            {
                if (autoSaveTimeMinutes != value)
                {
                    autoSaveTimeMinutes = value;
                    EditorConfig.SetConfigValue(configSection, ConfigKeys.AutoSaveTimeMinutesKey, value);
                    isDirty = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is automatic save assets enabled.
        /// </summary>
        /// <value><c>true</c> if it is automatic save assets enabled; otherwise, <c>false</c>.</value>
        public bool IsAutoSaveAssetsEnabled
        {
            get
            {
                if (!initialized)
                {
                    isAutoSaveAssetsEnabled = EditorConfig.GetConfigValue(configSection, ConfigKeys.IsAutoSaveAssetsEnabledKey, true);
                }

                return isAutoSaveAssetsEnabled;
            }

            set
            {
                if (isAutoSaveAssetsEnabled != value)
                {
                    isAutoSaveAssetsEnabled = value;
                    EditorConfig.SetConfigValue(configSection, ConfigKeys.IsAutoSaveAssetsEnabledKey, value);
                    isDirty = true;
                }
            }
        }

        /// <summary>
        /// Sets a value indicating whether to enable AutoSave feature.
        /// </summary>
        /// <value><c>true</c> if the feature of AutoSave is enabled; otherwise, <c>false</c>.</value>
        public bool IsAutoSaveEnabled
        {
            get
            {
                if (!initialized)
                {
                    isAutoSaveEnabled = EditorConfig.GetConfigValue(configSection, ConfigKeys.IsAutoSaveEnabledKey, false);
                }

                return isAutoSaveEnabled;
            }

            set
            {
                if (isAutoSaveEnabled != value)
                {
                    isAutoSaveEnabled = value;
                    EditorConfig.SetConfigValue(configSection, ConfigKeys.IsAutoSaveEnabledKey, value);
                    isDirty = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it is automatic save scenes enabled.
        /// </summary>
        /// <value><c>true</c> if it is automatic save scenes enabled; otherwise, <c>false</c>.</value>
        public bool IsAutoSaveScenesEnabled
        {
            get
            {
                if (!initialized)
                {
                    isAutoSaveScenesEnabled = EditorConfig.GetConfigValue(configSection, ConfigKeys.IsAutoSaveScenesEnabledKey, true);
                }

                return isAutoSaveScenesEnabled;
            }

            set
            {
                if (isAutoSaveScenesEnabled != value)
                {
                    isAutoSaveScenesEnabled = value;
                    EditorConfig.SetConfigValue(configSection, ConfigKeys.IsAutoSaveScenesEnabledKey, value);
                    isDirty = true;
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            if (!EditorApplication.isPlaying && !initialized)
            {
                initialized = false;

                isAutoSaveEnabled = IsAutoSaveEnabled;
                isAutoSaveScenesEnabled = IsAutoSaveScenesEnabled;
                isAutoSaveAssetsEnabled = IsAutoSaveAssetsEnabled;
                autoSaveTimeMinutes = AutoSaveTimeMinutes;
                askWhenSaving = AskWhenSaving;

                initialized = true;

                // Initialize autosave timer.
                if (autosaveTimer == null)
                {
                    autosaveTimer = new EditorTimer(1, AutoSaveTimeMinutes * 60);
                    autosaveTimer.Ticking += OnAutosaveTimerTicking;
                    autosaveTimer.Completed += OnAutosaveTimerCompleted;
                }
            }
        }

        /// <summary>
        /// Save project automatically.
        /// </summary>
        private void AutoSaveProject()
        {
            if (IsAutoSaveScenesEnabled)
            {
                // Save scenes.
                EditorSceneManager.SaveOpenScenes();
            }

            if (isAutoSaveAssetsEnabled)
            {
                // Save assets.
                AssetDatabase.SaveAssets();
            }

            //autosaveTimer.Start();
        }

        /// <summary>
        /// Called when timer completed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the timer event data.</param>
        private void OnAutosaveTimerCompleted(object sender, EventArgs e)
        {
            if (isAutoSaveEnabled && !EditorApplication.isPlaying)
            {
                if (AskWhenSaving)
                {
                    if (EditorUtility.DisplayDialog("Auto Save", "Do you want to save project?", "Yes", "No"))
                    {
                        AutoSaveProject();
                    }
                    else
                    {
                        autosaveTimer.Start();
                    }
                }
                else
                {
                    AutoSaveProject();
                }
            }
        }

        /// <summary>
        /// Called when timer is ticking.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="TimerTickingEventArgs"/> instance containing the timer event data.
        /// </param>
        private void OnAutosaveTimerTicking(object sender, TimerTickingEventArgs e)
        {
            if (isDirty)
            {
                autosaveTimer.Reset();
                autosaveTimer.RepeatCount = AutoSaveTimeMinutes * 60;
                isDirty = false;
                autosaveTimer.Start();
            }
        }

        #endregion Methods

        #region Classes

        /// <summary>
        /// The collections of text <see cref="GUIContent"/> s.
        /// </summary>
        public static class TextStyles
        {
            #region Fields

            /// <summary>
            /// The style of property askWhenSaving.
            /// </summary>
            public static readonly GUIContent AskWhenSavingStyle = new GUIContent("Ask When Saving", "Whether to show confirm dialog when saving");

            /// <summary>
            /// The style of property frequencyInMinutes.
            /// </summary>
            public static readonly GUIContent FrequencyInMinutesStyle = new GUIContent("Frequency in Minutes", "The time interval after which to auto save");

            /// <summary>
            /// The style of property isAutoSaveAssetsEnabled.
            /// </summary>
            public static readonly GUIContent IsAutoSaveAssetsEnabledStyle = new GUIContent("Save Assets", "Whether to automatically save assets during an autosave");

            /// <summary>
            /// The style of property isAutoSaveEnabled.
            /// </summary>
            public static readonly GUIContent IsAutoSaveEnabledStyle = new GUIContent("Enable AutoSave", "Whether to automatically save after a time interval");

            /// <summary>
            /// The style of property isAutoSaveScenesEnabled.
            /// </summary>
            public static readonly GUIContent IsAutoSaveScenesEnabledStyle = new GUIContent("Save Scenes", "Whether to automatically save scenes during an autosave");

            #endregion Fields
        }

        /// <summary>
        /// Class contains all config keys.
        /// </summary>
        private class ConfigKeys
        {
            #region Fields

            /// <summary>
            /// The key of config parameter "askWhenSaving".
            /// </summary>
            public const string AskWhenSavingKey = "askWhenSaving";

            /// <summary>
            /// The key of config parameter "autoSaveTimeMinutes".
            /// </summary>
            public const string AutoSaveTimeMinutesKey = "autoSaveTimeMinutes";

            /// <summary>
            /// The key of config parameter "isAutoSaveAssetsEnabled".
            /// </summary>
            public const string IsAutoSaveAssetsEnabledKey = "isAutoSaveAssetsEnabled";

            /// <summary>
            /// The key of config parameter "isAutoSaveEnabled".
            /// </summary>
            public const string IsAutoSaveEnabledKey = "isAutoSaveEnabled";

            /// <summary>
            /// The key of config parameter "isAutoSaveScenesEnabled".
            /// </summary>
            public const string IsAutoSaveScenesEnabledKey = "isAutoSaveScenesEnabled";

            #endregion Fields
        }

        #endregion Classes
    }
}
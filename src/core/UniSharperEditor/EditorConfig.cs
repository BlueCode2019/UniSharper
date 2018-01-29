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

using System;
using System.IO;
using System.Reflection;
using UnityEditor;

namespace UniSharperEditor
{
    /// <summary>
    /// <see cref="EditorConfig"/> to load and save configuration for editor.
    /// </summary>
    internal static class EditorConfig
    {
        #region Fields

        private static readonly string configFilePath = Path.Combine(new FileInfo(EditorApplication.applicationPath).DirectoryName, "EditorApp.config");

        #endregion Fields

        #region Properties

        private static string ConfigFilePath
        {
            get
            {
                if (!File.Exists(configFilePath))
                {
                    using (FileStream fs = File.Create(configFilePath))
                    {
                    }
                }

                return configFilePath;
            }
        }

        #endregion Properties

        #region Methods

        public static T GetConfigValue<T>(string section, string key, T defaultValue = default(T))
        {
            return default(T);
            //IniFile file = new IniFile(ConfigFilePath);
            //IniSectionInfo sectionInfo = file.GetSection(section);
            //T value = defaultValue;

            //if (sectionInfo != null && sectionInfo.ContainsKey(key))
            //{
            //    string strValue = sectionInfo[key];
            //    bool success = TryParse(strValue, out value);

            //    if (!success)
            //    {
            //        value = (T)(object)strValue;
            //    }
            //}

            //return value;
        }

        public static void SetConfigValue<T>(string section, string key, T value)
        {
            //IniFile file = new IniFile(ConfigFilePath);
            //IniSectionInfo sectionInfo = file.GetSection(section);

            //if (sectionInfo == null)
            //{
            //    sectionInfo = new IniSectionInfo(section);
            //    file.AddSection(section, sectionInfo);
            //}

            //string strValue = value.ToString();

            //if (sectionInfo.ContainsKey(key))
            //{
            //    sectionInfo[key] = strValue;
            //}
            //else
            //{
            //    sectionInfo.Add(key, strValue);
            //}

            //File.WriteAllText(ConfigFilePath, file.ToString());
        }

        /// <summary>
        /// Try parse the string object to the value of target type.
        /// </summary>
        /// <typeparam name="T">The type definition of the value out.</typeparam>
        /// <param name="source">A string containing a object of the type to convert.</param>
        /// <param name="result">The value of conversion result.</param>
        /// <returns><c>true</c> if source was converted successfully; otherwise, <c>false</c>.</returns>
        //private static bool TryParse<T>(string source, out T result)
        //{
        //    Type targetType = typeof(T);
        //    T value = default(T);
        //    object[] args = new object[2] { source, value };
        //    bool success = false;

        // try { Type[] types = new Type[2] { typeof(string), targetType.MakeByRefType() }; success =
        // (bool)ReflectionUtility.InvokeStaticMethod<T>("TryParse", types, args); result =
        // (T)args[1]; } catch (Exception) { result = default(T); }

        //    return success;
        //}

        #endregion Methods
    }
}
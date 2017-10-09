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

using System.Collections.Generic;
using UnityEngine;

namespace UniSharper.Utils
{
    /// <summary>
    /// This class provides some useful utilities for <see cref="GUIContent"/>.
    /// </summary>
    public static class GuiUtil
    {
        /// <summary>
        /// The cached GUI contents.
        /// </summary>
        private static Dictionary<string, GUIContent> textGUIContents;

        /// <summary>
        /// Initializes static members of the <see cref="EditorGUIUtility"/> class.
        /// </summary>
        static GuiUtil()
        {
            textGUIContents = new Dictionary<string, GUIContent>();
        }

        /// <summary>
        /// Gets the text of <see cref="GUIContent"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="tooltip">The tooltip.</param>
        /// <returns>The <see cref="GUIContent"/>.</returns>
        public static GUIContent TextContent(string text, string tooltip)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            GUIContent guiContent = null;

            if (!textGUIContents.ContainsKey(text))
            {
                guiContent = new GUIContent(text);

                if (!string.IsNullOrEmpty(tooltip))
                {
                    guiContent.tooltip = tooltip;
                }

                textGUIContents.Add(text, guiContent);
            }
            else
            {
                guiContent = textGUIContents[text];
            }

            return guiContent;
        }
    }
}
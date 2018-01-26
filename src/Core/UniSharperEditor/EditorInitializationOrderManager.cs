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
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor
{
    /// <summary>
    /// Class for managing editor scripts initialization order.
    /// </summary>
    [InitializeOnLoad]
    internal sealed class EditorInitializationOrderManager
    {
        #region Fields

        /// <summary>
        /// The loaded types.
        /// </summary>
        private static Type[] loadedTypes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="EditorInitializationOrderManager"/> class.
        /// </summary>
        static EditorInitializationOrderManager()
        {
            List<Type> typeList = new List<Type>();

            for (int i = 0, length = LoadedTypes.Length; i < length; ++i)
            {
                Type type = LoadedTypes[i];

                if (type.IsDefined(typeof(InitializeOnEditorStartupAttribute), false))
                {
                    typeList.Add(type);
                }
            }

            typeList.Sort(new InitializationOrderComparer());
            typeList.ForEach(type =>
            {
                try
                {
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
                catch (TypeInitializationException ex)
                {
                    Debug.LogException(ex.InnerException);
                }
            });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the loaded types.
        /// </summary>
        /// <value>The loaded types.</value>
        internal static Type[] LoadedTypes
        {
            get
            {
                if (loadedTypes == null)
                {
                    try
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        loadedTypes = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException)
                    {
                        return new Type[0];
                    }
                }

                return loadedTypes;
            }
        }

        #endregion Properties

        #region Classes

        /// <summary>
        /// The comparer of initialization order.
        /// </summary>
        /// <seealso cref="IComparer{Type}"/>
        private class InitializationOrderComparer : IComparer<Type>
        {
            #region Methods

            /// <summary>
            /// Compares the specified x with y.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// A signed integer that indicates the relative values of x and y, as shown in the
            /// following table.
            /// </returns>
            public int Compare(Type x, Type y)
            {
                InitializeOnEditorStartupAttribute[] xAttrs = x.GetCustomAttributes(typeof(InitializeOnEditorStartupAttribute), false) as InitializeOnEditorStartupAttribute[];
                InitializeOnEditorStartupAttribute[] yAttrs = y.GetCustomAttributes(typeof(InitializeOnEditorStartupAttribute), false) as InitializeOnEditorStartupAttribute[];

                if (xAttrs[0].ExecutionOrder > yAttrs[0].ExecutionOrder)
                {
                    return -1;
                }
                else if (xAttrs[0].ExecutionOrder < yAttrs[0].ExecutionOrder)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            #endregion Methods
        }

        #endregion Classes
    }
}
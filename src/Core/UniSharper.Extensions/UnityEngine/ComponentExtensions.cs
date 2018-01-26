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
using System.Reflection;

namespace UnityEngine
{
    /// <summary>
    /// Extension methods collection of <see cref="Component"/>.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Copies values from the <see cref="Component"/> to another <see cref="Component"/> of the
        /// <see cref="GameObject"/>.
        /// </summary>
        /// <param name="component">The <see cref="Component"/>.</param>
        /// <param name="targetGameObject">The target <see cref="GameObject"/>.</param>
        public static void CopyComponentValues(this Component component, GameObject targetGameObject)
        {
            if (targetGameObject)
            {
                Type type = component.GetType();
                Component copyComponent = targetGameObject.GetOrAddComponent(type);
                MemberInfo[] memberCollection = type.GetMembers();

                foreach (MemberInfo item in memberCollection)
                {
                    if (item.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fieldInfo = (FieldInfo)item;

                        if (!fieldInfo.IsLiteral)
                        {
                            object fieldValue = fieldInfo.GetValue(component);

                            if (fieldValue is ICloneable)
                            {
                                fieldInfo.SetValue(copyComponent, (fieldValue as ICloneable).Clone());
                            }
                            else
                            {
                                fieldInfo.SetValue(copyComponent, fieldInfo.GetValue(component));
                            }
                        }
                    }
                    else if (item.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo propertyInfo = (PropertyInfo)item;
                        MethodInfo setMethodInfo = propertyInfo.GetSetMethod(false);

                        if (setMethodInfo != null)
                        {
                            object propertyValue = propertyInfo.GetValue(component, null);

                            if (propertyValue is ICloneable)
                            {
                                propertyInfo.SetValue(copyComponent, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(copyComponent, propertyInfo.GetValue(component, null), null);
                            }
                        }
                    }
                }
            }
        }
    }
}
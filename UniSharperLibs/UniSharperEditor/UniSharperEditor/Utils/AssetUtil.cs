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

using System.IO;

namespace UniSharperEditor.Utils
{
    /// <summary>
    /// This class provides some useful asset utilities.
    /// </summary>
    internal static class AssetUtil
    {
        /// <summary>
        /// The extension of Scene asset.
        /// </summary>
        private const string sceneAssetExtension = ".unity";

        /// <summary>
        /// Determines whether the asset by the path is a scene asset.
        /// </summary>
        /// <param name="path">The path of the asset.</param>
        /// <returns><c>true</c> if it is a scene asset; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c>path</c> is <c>null</c>.</exception>
        public static bool IsSceneAsset(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string ext = Path.GetExtension(path);

                if (ext.Equals(sceneAssetExtension))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
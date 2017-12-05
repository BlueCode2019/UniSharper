using System;
using System.IO;

namespace UniSharper.Net.Http.VO
{
    /// <summary>
    /// Container for HTTP file.
    /// </summary>
	public class HttpFile
    {
        #region Properties

        public long ContentLength { get; set; }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public Action<Stream> Writer { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("ContentLength={0}, ContentType={1}, FileName={2}, Name={3}", ContentLength, ContentType, FileName, Name);
        }

        #endregion Methods
    }
}
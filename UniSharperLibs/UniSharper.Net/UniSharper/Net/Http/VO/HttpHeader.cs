namespace UniSharper.Net.Http.VO
{
    /// <summary>
    /// Representation of an HTTP header.
    /// </summary>
	public class HttpHeader
    {
        #region Properties

        /// <summary>
        /// Name of the header.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the header.
        /// </summary>
        public string Value { get; set; }

        #endregion Properties

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", Name, Value);
        }
    }
}
namespace UniSharper.Net.Http.VO
{
    /// <summary>
    /// Representation of an HTTP parameter (QueryString or Form value).
    /// </summary>
    public class HttpParameter
    {
        #region Properties

        /// <summary>
        /// Name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter.
        /// </summary>
        public string Value { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", Name, Value);
        }

        #endregion Methods
    }
}
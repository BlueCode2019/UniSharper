namespace UniSharper.Net.Http
{
    /// <summary>
    /// Status for HTTP responses.
    /// </summary>
    public enum HttpResponseStatus
    {
        None,
        Completed,
        Error,
        TimedOut,
        Aborted
    }
}
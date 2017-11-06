using System.Net;

namespace UniSharper.Net.Http
{
    public class HttpRequestAsyncHandle
    {
        private HttpWebRequest webRequest;

        internal HttpRequestAsyncHandle(HttpWebRequest webRequest)
        {
            this.webRequest = webRequest;
        }

        public void Abort()
        {
            if (webRequest != null)
            {
                webRequest.Abort();
            }
        }
    }
}
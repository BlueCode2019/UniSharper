using System;
using System.Net;

namespace UniSharper.Net.Http
{
    public partial class MonoHttpConnection : HttpConnection
    {
        #region Methods

        partial void AddSyncHeaderActions()
        {
            restrictedHeaderActions.Add(HttpHeaderField.Connection.Name, (r, v) => r.Connection = v);
            restrictedHeaderActions.Add(HttpHeaderField.ContentLength.Name, (r, v) => r.ContentLength = Convert.ToInt64(v));
            restrictedHeaderActions.Add(HttpHeaderField.Expect.Name, (r, v) => r.Expect = v);
            restrictedHeaderActions.Add(HttpHeaderField.IfModifiedSince.Name, (r, v) => r.IfModifiedSince = Convert.ToDateTime(v));
            restrictedHeaderActions.Add(HttpHeaderField.Referer.Name, (r, v) => r.Referer = v);
            restrictedHeaderActions.Add(HttpHeaderField.TransferEncoding.Name, (r, v) => { r.TransferEncoding = v; r.SendChunked = true; });
            restrictedHeaderActions.Add(HttpHeaderField.UserAgent.Name, (r, v) => r.UserAgent = v);
        }

        private HttpWebRequest ConfigureWebRequest(HttpMethod method, Uri url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.UseDefaultCredentials = false;
            ServicePointManager.Expect100Continue = false;

            AppendHeaders(webRequest);
            AppendCookies(webRequest);

            webRequest.Method = method.Method;
            webRequest.AutomaticDecompression = DecompressionMethods.None;

            if (!HasFiles)
            {
                webRequest.ContentLength = 0;
            }

            if (ClientCertificates != null)
            {
                webRequest.ClientCertificates = ClientCertificates;
            }

            if (UserAgent.HasValue())
            {
                webRequest.UserAgent = UserAgent;
            }

            if (Timeout != 0)
            {
                webRequest.Timeout = Timeout;
            }

            if (Credentials != null)
            {
                webRequest.Credentials = Credentials;
            }

            if (Proxy != null)
            {
                webRequest.Proxy = Proxy;
            }

            webRequest.AllowAutoRedirect = FollowRedirects;

            if (FollowRedirects && MaxRedirects.HasValue)
            {
                webRequest.MaximumAutomaticRedirections = MaxRedirects.Value;
            }

            return webRequest;
        }

        #endregion Methods
    }
}
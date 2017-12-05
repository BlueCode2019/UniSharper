using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using UniSharper.Net.Http.VO;

namespace UniSharper.Net.Http
{
    public partial class MonoHttpConnection : HttpConnection
    {
        #region Fields

        public CookieContainer CookieContainer { get; set; }

        public X509CertificateCollection ClientCertificates { get; set; }

        public string UserAgent { get; set; }

        public ICredentials Credentials { get; set; }

        public IWebProxy Proxy { get; set; }

        public bool FollowRedirects { get; set; }

        private readonly IDictionary<string, Action<HttpWebRequest, string>> restrictedHeaderActions;

        #endregion Fields

        #region Constructors

        public MonoHttpConnection()
            : base()
        {
            restrictedHeaderActions = new Dictionary<string, Action<HttpWebRequest, string>>(StringComparer.OrdinalIgnoreCase);
            AddSharedHeaderActions();
            AddSyncHeaderActions();
        }

        #endregion Constructors

        #region Methods

        partial void AddAsyncHeaderActions();

        private void AddRange(HttpWebRequest r, string range)
        {
            Match m = Regex.Match(range, "=(\\d+)-(\\d+)$");

            if (!m.Success)
            {
                return;
            }

            int from = Convert.ToInt32(m.Groups[1].Value);
            int to = Convert.ToInt32(m.Groups[2].Value);
            r.AddRange(from, to);
        }

        private void AddSharedHeaderActions()
        {
            restrictedHeaderActions.Add(HttpHeaderField.Accept.Name, (r, v) => r.Accept = v);
            restrictedHeaderActions.Add(HttpHeaderField.ContentType.Name, (r, v) => r.ContentType = v);
            restrictedHeaderActions.Add(HttpHeaderField.Date.Name, (r, v) => { /* Set by system */ });
            restrictedHeaderActions.Add(HttpHeaderField.Host.Name, (r, v) => { /* Set by system */ });
            restrictedHeaderActions.Add(HttpHeaderField.Range.Name, (r, v) => { AddRange(r, v); });
        }

        partial void AddSyncHeaderActions();

        private void AppendCookies(HttpWebRequest webRequest)
        {
            webRequest.CookieContainer = CookieContainer ?? new CookieContainer();

            foreach (HttpCookie httpCookie in Cookies)
            {
                Cookie cookie = new Cookie()
                {
                    Name = httpCookie.Name,
                    Value = httpCookie.Value,
                    Domain = webRequest.RequestUri.Host
                };

                webRequest.CookieContainer.Add(cookie);
            }
        }

        private void AppendHeaders(HttpWebRequest webRequest)
        {
            foreach (HttpHeader header in Headers)
            {
                if (restrictedHeaderActions.ContainsKey(header.Name))
                {
                    restrictedHeaderActions[header.Name].Invoke(webRequest, header.Value);
                }
                else
                {
                    webRequest.Headers.Add(header.Name, header.Value);
                }
            }
        }

        #endregion Methods
    }
}
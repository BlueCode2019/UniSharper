using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace UniSharper.Net.Http
{
    public interface IMonoHttpConnection : IHttpConnection
    {
        string UserAgent { get; set; }

        X509CertificateCollection ClientCertificates { get; set; }

        CookieContainer CookieContainer { get; set; }

        ICredentials Credentials { get; set; }

        IWebProxy Proxy { get; set; }

        bool FollowRedirects { get; set; }
    }
}
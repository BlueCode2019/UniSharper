using System.Collections.Generic;
using UniSharper.Net.Http.VO;

namespace UniSharper.Net.Http
{
    public interface IHttpConnection
    {
        int Timeout { get; set; }

        int? MaxRedirects { get; set; }

        IList<HttpHeader> Headers { get; }
        IList<HttpParameter> Parameters { get; }
        IList<HttpCookie> Cookies { get; }
        IList<HttpFile> Files { get; }
    }
}
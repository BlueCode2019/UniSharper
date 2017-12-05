using System;
using System.Collections.Generic;
using System.Net;
using UniSharper.Net.Http.VO;

namespace UniSharper.Net.Http
{
    public interface IHttpResponse
    {
        IHttpRequest Request { get; set; }

        string ContentType { get; set; }

        long ContentLength { get; set; }

        string ContentEncoding { get; set; }

        string Content { get; set; }

        HttpStatusCode StatusCode { get; set; }

        string StatusDescription { get; set; }

        byte[] Data { get; set; }

        Uri ResponseUri { get; set; }

        IList<HttpCookie> Cookies { get; }

        IList<Parameter> Headers { get; }

        HttpResponseStatus ResponseStatus { get; set; }

        string ErrorMessage { get; set; }

        Exception ErrorException { get; set; }
    }
}
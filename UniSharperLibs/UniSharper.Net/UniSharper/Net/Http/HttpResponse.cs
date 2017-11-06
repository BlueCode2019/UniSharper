using System;
using System.Collections.Generic;
using System.Net;

namespace UniSharper.Net.Http
{
    public abstract class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            Cookies = new List<HttpCookie>();
            Headers = new List<Parameter>();
        }

        public IHttpRequest Request { get; set; }

        public string ContentType { get; set; }

        public long ContentLength { get; set; }

        public string ContentEncoding { get; set; }

        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public byte[] Data { get; set; }

        public Uri ResponseUri { get; set; }

        public IList<HttpCookie> Cookies { get; }

        public IList<Parameter> Headers { get; }

        public HttpResponseStatus ResponseStatus { get; set; }

        public string ErrorMessage { get; set; }

        public Exception ErrorException { get; set; }
    }
}
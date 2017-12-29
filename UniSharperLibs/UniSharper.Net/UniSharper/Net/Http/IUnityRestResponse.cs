using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace UniSharper.Net.Http
{
    public interface IUnityRestResponse
    {
        IUnityRestRequest Request { get; set; }

        string ContentType { get; set; }

        long ContentLength { get; set; }

        string ContentEncoding { get; set; }

        string Content { get; set; }

        byte[] RawBytes { get; set; }

        IList<Parameter> Headers { get; }

        IList<NameValueObject> Cookies { get; }

        HttpResponseStatus ResponseStatus { get; set; }

        string ErrorMessage { get; set; }

        Exception ErrorException { get; set; }

        HttpStatusCode StatusCode { get; set; }
    }
}
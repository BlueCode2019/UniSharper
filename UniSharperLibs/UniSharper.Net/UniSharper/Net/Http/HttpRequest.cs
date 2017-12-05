using System;
using System.Collections.Generic;
using UniSharper.Net.Http.VO;

namespace UniSharper.Net.Http
{
    public abstract class HttpRequest : IHttpRequest
    {
        #region Constructors

        public HttpRequest()
        {
            Parameters = new List<Parameter>();
        }

        public HttpRequest(HttpMethod method)
            : this()
        {
            Method = method;
        }

        public HttpRequest(string resource)
            : this(resource, HttpMethod.Get)
        {
        }

        public HttpRequest(string resource, HttpMethod method)
            : this()
        {
            Resource = resource;
            Method = method;
        }

        public HttpRequest(Uri resource)
            : this(resource, HttpMethod.Get)
        {
        }

        public HttpRequest(Uri resource, HttpMethod method)
            : this(resource.IsAbsoluteUri ? resource.AbsolutePath + resource.Query : resource.OriginalString, method)
        {
        }

        #endregion Constructors

        #region Properties

        public HttpMethod Method { get; set; }

        public List<Parameter> Parameters { get; private set; }

        public string Resource { get; set; }

        public int Timeout { get; set; }

        #endregion Properties

        #region Methods

        public void AddCookie(string name, string value)
        {
            Parameters.Add(new Parameter() { Name = name, Value = value, Type = ParameterType.Cookie });
        }

        public void AddHeader(string name, string value)
        {
            Parameters.Add(new Parameter() { Name = name, Value = value, Type = ParameterType.HttpHeader });
        }

        public void AddQueryParameter(string name, string value)
        {
            Parameters.Add(new Parameter() { Name = name, Value = value, Type = ParameterType.Query });
        }

        public void AddUrlSegement(string name, string value)
        {
            Parameters.Add(new Parameter() { Name = name, Value = value, Type = ParameterType.UrlSegment });
        }

        #endregion Methods
    }
}
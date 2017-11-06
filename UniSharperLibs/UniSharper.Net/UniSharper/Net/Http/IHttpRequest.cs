using System.Collections.Generic;

namespace UniSharper.Net.Http
{
    public interface IHttpRequest
    {
        HttpMethod Method { get; set; }

        string Resource { get; set; }

        int Timeout { get; set; }

        List<Parameter> Parameters { get; }

        void AddUrlSegement(string name, string value);

        void AddQueryParameter(string name, string value);

        void AddHeader(string name, string value);

        void AddCookie(string name, string value);
    }
}
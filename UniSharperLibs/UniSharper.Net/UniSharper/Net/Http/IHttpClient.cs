using System;

namespace UniSharper.Net.Http
{
    public interface IHttpClient
    {
        IHttpResponse Get(IHttpRequest request);

        IHttpResponse Post(IHttpRequest request);

        IHttpResponse SendRequest(IHttpRequest request);

        HttpRequestAsyncHandle GetAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback);

        HttpRequestAsyncHandle PostAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback);

        HttpRequestAsyncHandle SendRequestAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback);
    }
}
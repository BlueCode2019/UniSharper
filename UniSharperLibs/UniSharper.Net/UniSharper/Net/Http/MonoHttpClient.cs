using System;

namespace UniSharper.Net.Http
{
    public class MonoHttpClient : IHttpClient
    {
        public IHttpResponse Get(IHttpRequest request)
        {
            return null;
        }

        public HttpRequestAsyncHandle GetAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback)
        {
            return null;
        }

        public IHttpResponse Post(IHttpRequest request)
        {
            return null;
        }

        public HttpRequestAsyncHandle PostAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback)
        {
            return null;
        }

        public IHttpResponse SendRequest(IHttpRequest request)
        {
            return null;
        }

        public HttpRequestAsyncHandle SendRequestAsync(IHttpRequest request, Action<IHttpResponse, HttpRequestAsyncHandle> callback)
        {
            return null;
        }
    }
}
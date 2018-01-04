using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace UniSharper.Net.Http
{
    public interface IUnityRestRequest
    {
        #region Properties

        List<FileParameter> Files { get; }

        HttpMethod Method { get; set; }

        List<Parameter> Parameters { get; }

        int ReadWriteTimeout { get; set; }

        string Resource { get; set; }

        Action<Stream> ResponseWriter { get; set; }

        int Timeout { get; set; }

        #endregion Properties

        #region Methods

        IUnityRestRequest AddBody(object obj);

        IUnityRestRequest AddCookie(string name, string value);

        IUnityRestRequest AddFile(string name, string path, string contentType = null);

        IUnityRestRequest AddFile(string name, byte[] bytes, string fileName, string contentType = null);

        IUnityRestRequest AddFile(string name, Action<Stream> writer, string fileName, string contentType = null);

        IUnityRestRequest AddFileBytes(string name, byte[] bytes, string filename, string contentType = "application/x-gzip");

        IUnityRestRequest AddHeader(string name, string value);

        IUnityRestRequest AddParameter(Parameter p);

        IUnityRestRequest AddParameter(string name, object value);

        IUnityRestRequest AddParameter(string name, object value, ParameterType type);

        IUnityRestRequest AddParameter(string name, object value, string contentType, ParameterType type);

        IUnityRestRequest AddQueryParameter(string name, string value);

        IUnityRestRequest AddUrlSegment(string name, string value);

        #endregion Methods
    }
}
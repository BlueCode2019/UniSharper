using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using System.Text.RegularExpressions;
using RestSharp.Extensions;

namespace UniSharper.Net.Http
{
    public class UnityRestRequest : IUnityRestRequest
    {
        #region Constructors

        public UnityRestRequest()
        {
            Method = HttpMethod.Get;
            Parameters = new List<Parameter>();
            Files = new List<FileParameter>();
        }

        public UnityRestRequest(HttpMethod method)
            : this()
        {
            Method = method;
        }

        public UnityRestRequest(string resource)
            : this(resource, HttpMethod.Get)
        {
        }

        public UnityRestRequest(string resource, HttpMethod method)
            : this()
        {
            Resource = resource;
            Method = method;
        }

        public UnityRestRequest(Uri resource)
            : this(resource, HttpMethod.Get)
        {
        }

        public UnityRestRequest(Uri resource, HttpMethod method)
            : this(resource.IsAbsoluteUri
                ? resource.AbsolutePath + resource.Query
                : resource.OriginalString, method)
        {
        }

        #endregion Constructors

        #region Properties

        public List<FileParameter> Files
        {
            get;
            private set;
        }

        public HttpMethod Method
        {
            get;
            set;
        }

        public List<Parameter> Parameters
        {
            get;
            private set;
        }

        public int ReadWriteTimeout
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }

        public Action<Stream> ResponseWriter
        {
            get;
            set;
        }

        public int Timeout
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public IUnityRestRequest AddBody(object obj)
        {
            return AddParameter("application/octet-stream", obj, ParameterType.RequestBody);
        }

        public IUnityRestRequest AddCookie(string name, string value)
        {
            return AddParameter(name, value, ParameterType.Cookie);
        }

        public IUnityRestRequest AddFile(string name, string path, string contentType = null)
        {
            FileInfo f = new FileInfo(path);
            long fileLength = f.Length;

            return AddFile(new FileParameter
            {
                Name = name,
                FileName = Path.GetFileName(path),
                ContentLength = fileLength,
                Writer = s =>
                {
                    using (StreamReader file = new StreamReader(path))
                    {
                        file.BaseStream.CopyTo(s);
                    }
                },
                ContentType = contentType
            });
        }

        public IUnityRestRequest AddFile(string name, Action<Stream> writer, string fileName, string contentType = null)
        {
            return AddFile(new FileParameter
            {
                Name = name,
                Writer = writer,
                FileName = fileName,
                ContentType = contentType
            });
        }

        public IUnityRestRequest AddFile(string name, byte[] bytes, string fileName, string contentType = null)
        {
            return AddFile(FileParameter.Create(name, bytes, fileName, contentType));
        }

        public IUnityRestRequest AddFileBytes(string name, byte[] bytes, string filename, string contentType = "application/x-gzip")
        {
            long length = bytes.Length;

            return AddFile(new FileParameter
            {
                Name = name,
                FileName = filename,
                ContentLength = length,
                ContentType = contentType,
                Writer = s =>
                {
                    using (StreamReader file = new StreamReader(new MemoryStream(bytes)))
                    {
                        file.BaseStream.CopyTo(s);
                    }
                }
            });
        }

        public IUnityRestRequest AddHeader(string name, string value)
        {
            const string portSplit = @":\d+";
            Func<string, bool> invalidHost =
                host => Uri.CheckHostName(Regex.Split(host, portSplit)[0]) == UriHostNameType.Unknown;

            if (name == "Host" && invalidHost(value))
            {
                throw new ArgumentException("The specified value is not a valid Host header string.", "value");
            }

            return AddParameter(name, value, ParameterType.HttpHeader);
        }

        public IUnityRestRequest AddParameter(Parameter p)
        {
            Parameters.Add(p);
            return this;
        }

        public IUnityRestRequest AddParameter(string name, object value)
        {
            return AddParameter(new Parameter
            {
                Name = name,
                Value = value,
                Type = ParameterType.GetOrPost
            });
        }

        public IUnityRestRequest AddParameter(string name, object value, ParameterType type)
        {
            return AddParameter(new Parameter
            {
                Name = name,
                Value = value,
                Type = type
            });
        }

        public IUnityRestRequest AddParameter(string name, object value, string contentType, ParameterType type)
        {
            return AddParameter(new Parameter
            {
                Name = name,
                Value = value,
                ContentType = contentType,
                Type = type
            });
        }

        public IUnityRestRequest AddQueryParameter(string name, string value)
        {
            return AddParameter(name, value, ParameterType.QueryString);
        }

        public IUnityRestRequest AddUrlSegment(string name, string value)
        {
            return AddParameter(name, value, ParameterType.UrlSegment);
        }

        private IUnityRestRequest AddFile(FileParameter file)
        {
            Files.Add(file);
            return this;
        }

        #endregion Methods
    }
}
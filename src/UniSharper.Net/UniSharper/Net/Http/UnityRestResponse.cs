using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace UniSharper.Net.Http
{
    public class UnityRestResponse : IUnityRestResponse
    {
        #region Properties

        public string Content
        {
            get;
            set;
        }

        public string ContentEncoding
        {
            get;
            set;
        }

        public long ContentLength
        {
            get;
            set;
        }

        public string ContentType
        {
            get;
            set;
        }

        public IList<NameValueObject> Cookies
        {
            get;
            private set;
        }

        public Exception ErrorException
        {
            get;
            set;
        }

        public string ErrorMessage
        {
            get;
            set;
        }

        public IList<Parameter> Headers
        {
            get;
            private set;
        }

        public byte[] RawBytes
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IUnityRestRequest Request
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public HttpResponseStatus ResponseStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion Properties
    }
}
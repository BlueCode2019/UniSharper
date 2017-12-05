using System.Collections.Generic;
using System.Linq;
using UniSharper.Net.Http.VO;

namespace UniSharper.Net.Http
{
    public abstract class HttpConnection : IHttpConnection
    {
        #region Constructors

        public HttpConnection()
        {
            Headers = new List<HttpHeader>();
            Parameters = new List<HttpParameter>();
            Cookies = new List<HttpCookie>();
            Files = new List<HttpFile>();
        }

        #endregion Constructors

        #region Properties

        public int Timeout { get; set; }

        public int? MaxRedirects { get; set; }

        public IList<HttpCookie> Cookies
        {
            get;
            private set;
        }

        public IList<HttpFile> Files
        {
            get;
            private set;
        }

        public IList<HttpHeader> Headers
        {
            get;
            private set;
        }

        public IList<HttpParameter> Parameters
        {
            get;
            private set;
        }

        protected bool HasFiles
        {
            get
            {
                return Files.Any();
            }
        }

        #endregion Properties
    }
}
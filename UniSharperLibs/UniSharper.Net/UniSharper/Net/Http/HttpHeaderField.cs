namespace UniSharper.Net.Http
{
    public class HttpHeaderField
    {
        #region Fields

        private static readonly HttpHeaderField acceptCharsetHeader = new HttpHeaderField("Accept-Charset");
        private static readonly HttpHeaderField acceptEncodingHeader = new HttpHeaderField("Accept-Encoding");
        private static readonly HttpHeaderField acceptHeader = new HttpHeaderField("Accept");
        private static readonly HttpHeaderField acceptLanguageHeader = new HttpHeaderField("Accept-Language");

        private static readonly HttpHeaderField acceptRangesHeader = new HttpHeaderField("Accept-Ranges");

        private static readonly HttpHeaderField ageHeader = new HttpHeaderField("Age");

        private static readonly HttpHeaderField allowHeader = new HttpHeaderField("Allow");

        private static readonly HttpHeaderField authorizationHeader = new HttpHeaderField("Authorization");

        private static readonly HttpHeaderField cacheControlHeader = new HttpHeaderField("Cache-Control");

        private static readonly HttpHeaderField connectionHeader = new HttpHeaderField("Connection");

        private static readonly HttpHeaderField contentEncodingHeader = new HttpHeaderField("Content-Encoding");

        private static readonly HttpHeaderField contentLanguageHeader = new HttpHeaderField("Content-Language");

        private static readonly HttpHeaderField contentLengthHeader = new HttpHeaderField("Content-Length");

        private static readonly HttpHeaderField contentLocationHeader = new HttpHeaderField("Content-Location");

        private static readonly HttpHeaderField contentMD5Header = new HttpHeaderField("Content-MD5");

        private static readonly HttpHeaderField contentRangeHeader = new HttpHeaderField("Content-Range");

        private static readonly HttpHeaderField contentTypeHeader = new HttpHeaderField("Content-Type");

        private static readonly HttpHeaderField dateHeader = new HttpHeaderField("Date");

        private static readonly HttpHeaderField eTagHeader = new HttpHeaderField("ETag");

        private static readonly HttpHeaderField expectHeader = new HttpHeaderField("Expect");

        private static readonly HttpHeaderField expiresHeader = new HttpHeaderField("Expires");

        private static readonly HttpHeaderField fromHeader = new HttpHeaderField("From");

        private static readonly HttpHeaderField hostHeader = new HttpHeaderField("Host");

        private static readonly HttpHeaderField ifMatchHeader = new HttpHeaderField("If-Match");

        private static readonly HttpHeaderField ifModifiedSinceHeader = new HttpHeaderField("If-Modified-Since");

        private static readonly HttpHeaderField ifNoneMatchHeader = new HttpHeaderField("If-None-Match");

        private static readonly HttpHeaderField ifRangeHeader = new HttpHeaderField("If-Range");

        private static readonly HttpHeaderField ifUnmodifiedSinceHeader = new HttpHeaderField("If-Unmodified-Since");

        private static readonly HttpHeaderField lastModifiedHeader = new HttpHeaderField("Last-Modified");

        private static readonly HttpHeaderField locationHeader = new HttpHeaderField("Location");

        private static readonly HttpHeaderField maxForwardsHeader = new HttpHeaderField("Max-Forwards");

        private static readonly HttpHeaderField pragmaHeader = new HttpHeaderField("Pragma");

        private static readonly HttpHeaderField proxyAuthenticateHeader = new HttpHeaderField("Proxy-Authenticate");

        private static readonly HttpHeaderField proxyAuthorizationHeader = new HttpHeaderField("Proxy-Authorization");

        private static readonly HttpHeaderField rangeHeader = new HttpHeaderField("Range");

        private static readonly HttpHeaderField refererHeader = new HttpHeaderField("Referer");

        private static readonly HttpHeaderField retryAfterHeader = new HttpHeaderField("Retry-After");

        private static readonly HttpHeaderField serverHeader = new HttpHeaderField("Server");

        private static readonly HttpHeaderField trailerHeader = new HttpHeaderField("Trailer");
        private static readonly HttpHeaderField transferCodingHeader = new HttpHeaderField("TE");
        private static readonly HttpHeaderField transferEncodingHeader = new HttpHeaderField("Transfer-Encoding");

        private static readonly HttpHeaderField upgradeHeader = new HttpHeaderField("Upgrade");

        private static readonly HttpHeaderField userAgentHeader = new HttpHeaderField("User-Agent");

        private static readonly HttpHeaderField varyHeader = new HttpHeaderField("Vary");

        private static readonly HttpHeaderField viaHeader = new HttpHeaderField("Via");

        private static readonly HttpHeaderField warningHeader = new HttpHeaderField("Warning");

        private static readonly HttpHeaderField wwwAuthenticateHeader = new HttpHeaderField("WWW-Authenticate");

        private string name;

        #endregion Fields

        #region Properties

        public static HttpHeaderField Accept
        {
            get
            {
                return acceptHeader;
            }
        }

        public static HttpHeaderField AcceptCharset
        {
            get
            {
                return acceptCharsetHeader;
            }
        }

        public static HttpHeaderField AcceptEncoding
        {
            get
            {
                return acceptEncodingHeader;
            }
        }

        public static HttpHeaderField AcceptLanguage
        {
            get
            {
                return acceptLanguageHeader;
            }
        }

        public static HttpHeaderField AcceptRanges
        {
            get
            {
                return acceptRangesHeader;
            }
        }

        public static HttpHeaderField Age
        {
            get
            {
                return ageHeader;
            }
        }

        public static HttpHeaderField Allow
        {
            get
            {
                return allowHeader;
            }
        }

        public static HttpHeaderField Authorization
        {
            get
            {
                return authorizationHeader;
            }
        }

        public static HttpHeaderField CacheControl
        {
            get
            {
                return cacheControlHeader;
            }
        }

        public static HttpHeaderField Connection
        {
            get
            {
                return connectionHeader;
            }
        }

        public static HttpHeaderField ContentEncoding
        {
            get
            {
                return contentEncodingHeader;
            }
        }

        public static HttpHeaderField ContentLanguage
        {
            get
            {
                return contentLanguageHeader;
            }
        }

        public static HttpHeaderField ContentLength
        {
            get
            {
                return contentLengthHeader;
            }
        }

        public static HttpHeaderField ContentLocation
        {
            get
            {
                return contentLocationHeader;
            }
        }

        public static HttpHeaderField ContentMD5
        {
            get
            {
                return contentMD5Header;
            }
        }

        public static HttpHeaderField ContentRange
        {
            get
            {
                return contentRangeHeader;
            }
        }

        public static HttpHeaderField ContentType
        {
            get
            {
                return contentTypeHeader;
            }
        }

        public static HttpHeaderField Date
        {
            get
            {
                return dateHeader;
            }
        }

        public static HttpHeaderField ETag
        {
            get
            {
                return eTagHeader;
            }
        }

        public static HttpHeaderField Expect
        {
            get
            {
                return expectHeader;
            }
        }

        public static HttpHeaderField Expires
        {
            get
            {
                return expiresHeader;
            }
        }

        public static HttpHeaderField From
        {
            get
            {
                return fromHeader;
            }
        }

        public static HttpHeaderField Host
        {
            get
            {
                return hostHeader;
            }
        }

        public static HttpHeaderField IfMatch
        {
            get
            {
                return ifMatchHeader;
            }
        }

        public static HttpHeaderField IfModifiedSince
        {
            get
            {
                return ifModifiedSinceHeader;
            }
        }

        public static HttpHeaderField IfNoneMatch
        {
            get
            {
                return ifNoneMatchHeader;
            }
        }

        public static HttpHeaderField IfRange
        {
            get
            {
                return ifRangeHeader;
            }
        }

        public static HttpHeaderField IfUnmodifiedSince
        {
            get
            {
                return ifUnmodifiedSinceHeader;
            }
        }

        public static HttpHeaderField LastModified
        {
            get
            {
                return lastModifiedHeader;
            }
        }

        public static HttpHeaderField Location
        {
            get
            {
                return locationHeader;
            }
        }

        public static HttpHeaderField MaxForwards
        {
            get
            {
                return maxForwardsHeader;
            }
        }

        public static HttpHeaderField Pragma
        {
            get
            {
                return pragmaHeader;
            }
        }

        public static HttpHeaderField ProxyAuthenticate
        {
            get
            {
                return proxyAuthenticateHeader;
            }
        }

        public static HttpHeaderField ProxyAuthorization
        {
            get
            {
                return proxyAuthorizationHeader;
            }
        }

        public static HttpHeaderField Range
        {
            get
            {
                return rangeHeader;
            }
        }

        public static HttpHeaderField Referer
        {
            get
            {
                return refererHeader;
            }
        }

        public static HttpHeaderField RetryAfter
        {
            get
            {
                return retryAfterHeader;
            }
        }

        public static HttpHeaderField Server
        {
            get
            {
                return serverHeader;
            }
        }

        public static HttpHeaderField TE
        {
            get
            {
                return transferCodingHeader;
            }
        }

        public static HttpHeaderField Trailer
        {
            get
            {
                return trailerHeader;
            }
        }

        public static HttpHeaderField TransferEncoding
        {
            get
            {
                return transferEncodingHeader;
            }
        }

        public static HttpHeaderField Upgrade
        {
            get
            {
                return upgradeHeader;
            }
        }

        public static HttpHeaderField UserAgent
        {
            get
            {
                return userAgentHeader;
            }
        }

        public static HttpHeaderField Vary
        {
            get
            {
                return varyHeader;
            }
        }

        public static HttpHeaderField Via
        {
            get
            {
                return viaHeader;
            }
        }

        public static HttpHeaderField Warning
        {
            get
            {
                return warningHeader;
            }
        }

        public static HttpHeaderField WwwAuthenticate
        {
            get
            {
                return wwwAuthenticateHeader;
            }
        }

        #endregion Properties

        #region Constructors

        private HttpHeaderField(string name)
        {
            this.name = name;
        }

        #endregion Constructors



        #region Properties

        public string Name
        {
            get
            {
                return name;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return name.ToUpperInvariant().GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return name;
        }

        #endregion Methods
    }
}
using NUnit.Framework;
using System;

namespace UniSharper.Net.Http
{
    [TestFixture]
    internal class MonoHttpRequestTests
    {
        [Test]
        public void SendRequestTest()
        {
            string uri = "http://localhost:8080/httptest/SendRequestTest.php";
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri);

            using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            {
                request.KeepAlive = false;

                using (HttpResponseMessage respMessage = request.SendRequest())
                {
                    string expected = "SendRequestTest";
                    string actual = respMessage.Entity.Text;
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [Test]
        public void SendGetRequestTest()
        {
            string uri = "http://localhost:8080/httptest/SendGetRequestTest.php";
            HttpUrlQuery query = new HttpUrlQuery();
            query.Add("name", "test123");
            query.Add("sex", "male");
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri, query);

            using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            {
                request.KeepAlive = false;

                using (HttpResponseMessage respMessage = request.SendRequest())
                {
                    string expected = "name=test123&sex=male";
                    string actual = respMessage.Entity.Text;
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}
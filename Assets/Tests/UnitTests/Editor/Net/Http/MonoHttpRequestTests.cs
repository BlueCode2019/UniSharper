using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using UnityEngine;

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

        [Test]
        public void SendPostRequestTest()
        {
            string uri = "http://localhost:8080/httptest/SendPostRequestTest.php";
            HttpFormData formData = new HttpFormData();
            formData.AddField("name", "test123");
            formData.AddField("sex", "male");
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri, HttpMethod.Post);
            reqMessage.Entity = new HttpRequestMessageEntity(formData);

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

        [Test]
        public void SendFileRequestTest()
        {
            byte[] data = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "text.txt"));
            HttpFormData formData = new HttpFormData();
            formData.AddBinaryData("file", data, "text.txt");
            string uri = "http://localhost:8080/httptest/SendFileRequestTest.php";
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri, HttpMethod.Post);
            reqMessage.Entity = new HttpRequestMessageEntity(formData);

            using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            {
                request.KeepAlive = false;

                using (HttpResponseMessage respMessage = request.SendRequest())
                {
                    string expected = Encoding.UTF8.GetString(data);
                    string actual = respMessage.Entity.Text;
                    Assert.AreEqual(expected, actual);
                }
            }
        }

        [Test]
        public void SimplifiedGetTest()
        {
            string uri = "http://localhost:8080/httptest/SendGetRequestTest.php";
            HttpResponseMessage respMessage = MonoHttpRequest.Get(uri, new HttpUrlQuery()
            {
                { "name", "test" }
            });
            string expected = "name=test";
            string actual = respMessage.Entity.Text;
            Assert.AreEqual(expected, actual);
            respMessage.Dispose();
        }

        [Test]
        public void SimplifiedPostTest()
        {
            string uri = "http://localhost:8080/httptest/SendPostRequestTest.php";
            HttpFormData formData = new HttpFormData();
            formData.AddField("name", "test");
            HttpResponseMessage respMessage = MonoHttpRequest.Post(uri, formData);
            string expected = "name=test";
            string actual = respMessage.Entity.Text;
            Assert.AreEqual(expected, actual);
            respMessage.Dispose();
        }
    }
}
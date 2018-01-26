using NUnit.Framework;
using RestSharp;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniSharper.Net.Http.Tests
{
    public class MonoRestClientTest
    {
        [UnityTest]
        public IEnumerator DownloadDataAsyncTest()
        {
            yield return new MonoBehaviourTest<DownloadDataAsyncMonoBehaviourTest>();
        }
    }

    public class DownloadDataAsyncMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
    {
        private bool isTestFinished;

        public bool IsTestFinished
        {
            get
            {
                return isTestFinished;
            }
        }

        private IMonoRestClient client;

        private string expected;

        private void Start()
        {
            isTestFinished = false;
            client = new MonoRestClient("http://localhost:8080/");
            IRestRequest request = new RestRequest("httptest/SendGetRequestTest.php", Method.GET);
            Parameter p = new Parameter() { Name = "id", Value = "test", Type = ParameterType.QueryString };
            request.AddParameter(p);
            expected = p.ToString();
            client.DownloadDataAsync(request, DownloadDataCallback);
        }

        private void DownloadDataCallback(byte[] data, RestRequestAsyncHandle handle)
        {
            if (data == null)
            {
                Assert.Fail();
            }
            else
            {
                string text = Encoding.UTF8.GetString(data);

                if (text.Equals(expected))
                {
                    isTestFinished = true;
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
    }
}
using RestSharp;
using System.Text;
using UnityEngine;

namespace UniSharper.Net.Http
{
    [IntegrationTest.DynamicTest("HttpClientTests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class MonoRestClientDownloadDataAsyncTest : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            IMonoRestClient client = new MonoRestClient("http://localhost:8080/");
            IRestRequest request = new RestRequest("httptest/SendGetRequestTest.php", Method.GET);
            Parameter p = new Parameter() { Name = "id", Value = "test", Type = ParameterType.QueryString };
            request.AddParameter(p);
            string expected = p.ToString();
            client.DownloadDataAsync(request, (byte[] data, RestRequestAsyncHandle handle) =>
            {
                if (data == null)
                {
                    IntegrationTest.Fail();
                }
                else
                {
                    string text = Encoding.UTF8.GetString(data);

                    if (text.Equals(expected))
                    {
                        IntegrationTest.Pass();
                    }
                    else
                    {
                        IntegrationTest.Fail();
                    }
                }
            });
        }
    }
}
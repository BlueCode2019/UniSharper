using UnityEngine;

namespace UniSharper.Net.Http
{
    [IntegrationTest.DynamicTest("MonoHttpRequestTests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class SendGetRequestAsyncTest : MonoBehaviour
    {
        private MonoHttpRequest request;

        private void Start()
        {
            string uri = "http://localhost:8080/httptest/SendGetRequestTest.php";
            HttpUrlQuery query = new HttpUrlQuery();
            query.Add("name", "test123");
            query.Add("sex", "male");
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri, query);
            request = new MonoHttpRequest(reqMessage);
            request.SendRequestAsync(OnResponse);
        }

        private void OnDestroy()
        {
            if (request != null)
            {
                request.Dispose();
                request = null;
            }
        }

        private void OnResponse(HttpResponseMessage message)
        {
            string expected = "name=test123&sex=male";
            string actual = message.Entity.Text;

            if (expected == actual)
            {
                IntegrationTest.Pass();
            }
            else
            {
                IntegrationTest.Fail();
            }
        }
    }
}
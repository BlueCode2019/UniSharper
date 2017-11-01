using UnityEngine;

namespace UniSharper.Net.Http
{
    [IntegrationTest.DynamicTest("MonoHttpRequestTests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class SendRequestAsyncTest : MonoBehaviour
    {
        private MonoHttpRequest request;

        private void Start()
        {
            string uri = "http://localhost:8080/httptest/SendRequestTest.php";
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri);
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
            string expected = "SendRequestTest";
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
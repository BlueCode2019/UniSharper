using UnityEngine;

namespace UniSharper.Net.Http
{
    [IntegrationTest.DynamicTest("MonoHttpRequestTests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class SimplifiedPostAsyncTest : MonoBehaviour
    {
        private MonoHttpRequest request;

        private void Start()
        {
            string uri = "http://localhost:8080/httptest/SendPostRequestTest.php";
            HttpFormData formData = new HttpFormData();
            formData.AddField("name", "test");
            request = MonoHttpRequest.PostAsync(uri, formData, OnResponse);
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
            string expected = "name=test";
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
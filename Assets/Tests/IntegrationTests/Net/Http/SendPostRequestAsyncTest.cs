using System.IO;
using System.Text;
using UnityEngine;

namespace UniSharper.Net.Http
{
    [IntegrationTest.DynamicTest("MonoHttpRequestTests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class SendPostRequestAsyncTest : MonoBehaviour
    {
        private MonoHttpRequest request;

        private string expected;

        private void Start()
        {
            byte[] data = File.ReadAllBytes(Path.Combine(Application.streamingAssetsPath, "text.txt"));
            expected = Encoding.UTF8.GetString(data);
            HttpFormData formData = new HttpFormData();
            formData.AddBinaryData("file", data, "text.txt");
            string uri = "http://localhost:8080/httptest/SendFileRequestTest.php";
            HttpRequestMessage reqMessage = new HttpRequestMessage(uri, HttpMethod.Post);
            reqMessage.Entity = new HttpRequestMessageEntity(formData);
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
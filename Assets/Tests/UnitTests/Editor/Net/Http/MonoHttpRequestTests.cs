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
            //string uri = "http://www.baidu.com/";
            //HttpRequestMessage reqMessage = new HttpRequestMessage(uri);

            //using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            //{
            //    request.KeepAlive = false;

            //    using (HttpResponseMessage respMessage = request.SendRequest())
            //    {
            //        Console.WriteLine(respMessage.Entity.Text);
            //        Assert.IsNotNull(respMessage.Entity.Text);
            //    }
            //}
        }

        [Test]
        public void SendGetRequestTest()
        {
            //string uri = "https://docs.microsoft.com/zh-cn/dotnet/api/system.net.webheadercollection.add";
            //HttpUrlQuery query = new HttpUrlQuery();
            //query.Add("view", "netcore-2.0");
            //HttpRequestMessage reqMessage = new HttpRequestMessage(uri, query);

            //using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            //{
            //    request.KeepAlive = false;

            //    using (HttpResponseMessage respMessage = request.SendRequest())
            //    {
            //        Console.WriteLine(respMessage.Entity.Text);
            //        Assert.IsNotNull(respMessage.Entity.Text);
            //    }
            //}
        }
    }
}
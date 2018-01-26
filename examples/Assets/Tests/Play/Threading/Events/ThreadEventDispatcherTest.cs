using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniSharper.Threading.Events.Tests
{
    public class ThreadEventDispatcherTest
    {
        [UnityTest]
        public IEnumerator DispatchTest()
        {
            yield return new MonoBehaviourTest<DispatchMonoBehaviourTest>();
        }
    }

    public class DispatchMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
    {
        private ThreadTextReader threadImageReader;

        private bool isTestFinished;

        public bool IsTestFinished
        {
            get
            {
                return isTestFinished;
            }
        }

        private void Start()
        {
            isTestFinished = false;
            threadImageReader = new ThreadTextReader();
            threadImageReader.AddEventListener(TestEvent.Complete, OnThreadImageReaderComplete);
            threadImageReader.BeginRead();
        }

        private void OnThreadImageReaderComplete(Event e)
        {
            threadImageReader.RemoveAllEventListeners();
            isTestFinished = true;
        }
    }
}
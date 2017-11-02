using System.Collections;
using UnityEngine;

namespace UniSharper
{
    [IntegrationTest.DynamicTest("Tests")]
    [IntegrationTest.SucceedWithAssertions]
    internal class CoroutineEnumeratorTest : MonoBehaviour
    {
        private void Start()
        {
            CoroutineEnumerator enumerator = new CoroutineEnumerator(TestMethodA(), TestMethodB(), TestPassed());
            StartCoroutine(enumerator);
        }

        private IEnumerator TestMethodA()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("TestMethodA executed");
        }

        private IEnumerator TestMethodB()
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("TestMethodB executed");
        }

        private IEnumerator TestPassed()
        {
            yield return new WaitForFixedUpdate();
            IntegrationTest.Pass();
        }
    }
}
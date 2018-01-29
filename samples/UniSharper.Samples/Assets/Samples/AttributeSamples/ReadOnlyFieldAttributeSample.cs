using UnityEngine;

namespace UniSharper.Samples
{
    public class ReadOnlyFieldAttributeSample : MonoBehaviour
    {
        [ReadOnlyField]
        [SerializeField]
        private float privateReadonlyField;

        [ReadOnlyField]
        public int PublicReadonlyField;
    }
}
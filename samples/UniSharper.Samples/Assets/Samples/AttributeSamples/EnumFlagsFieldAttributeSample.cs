using System;
using UnityEngine;

namespace UniSharper.Samples
{
    public class EnumFlagsFieldAttributeSample : MonoBehaviour
    {
        [Flags]
        public enum EnumFlagsTest
        {
            Value1,
            Value2,
            Value3
        }

        [EnumFlagsField]
        public EnumFlagsTest enumFlagsField;
    }
}
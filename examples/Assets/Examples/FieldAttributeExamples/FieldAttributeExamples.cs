using System;
using UniSharper;
using UnityEngine;

public class FieldAttributeExamples : MonoBehaviour
{
    [Flags]
    public enum EnumFlagsTest
    {
        Value1,
        Value2,
        Value3
    }

    [ReadonlyField]
    [SerializeField]
    private float privateReadonlyField;

    [ReadonlyField]
    public int PublicReadonlyField;

    [EnumFlagsField]
    public EnumFlagsTest enumFlagsField;
}
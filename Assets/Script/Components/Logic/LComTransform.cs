using System.Collections;
using System.Collections.Generic;
using Deterministics.Math;
using Unity.Entities;
using UnityEngine;

public struct LComTransform : IComponentData
{
    public fp2 pos;
    public float angle;
}

using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;

public struct VComTransform : IComponentData
{
    public float2 pos;
    public float angle;
}

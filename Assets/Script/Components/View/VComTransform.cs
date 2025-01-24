using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct VComTransform : IComponentData
{
    public Vector2 pos;
    public float angle;
}

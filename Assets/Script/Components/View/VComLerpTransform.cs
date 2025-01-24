using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct VComLerpTransform : IComponentData
{
    public VComTransform pre;
    public float _totalTime;
}

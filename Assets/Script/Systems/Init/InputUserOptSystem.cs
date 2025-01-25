using Deterministics.Math;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial class InputUserOptSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        float2 fp = new float2(x, y);
        fp = math.normalize(fp);

        LocalFrameGenerator.inputCache.SetRotation((fp2)fp);
    }
}
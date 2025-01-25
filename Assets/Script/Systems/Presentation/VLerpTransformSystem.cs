using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial class VLerpTransformSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var frameCount = SystemAPI.GetSingleton<ComFrameCount>();
        var isLogicFrame = frameCount.frameUnity == UnityEngine.Time.frameCount;
        var deltaTime = UnityEngine.Time.deltaTime;
        var timeInterval = ComFrameCount.DELTA_TIME;

        Entities.ForEach((ref LComTransform lTransform, ref VComLerpTransform vLerp, ref VComTransform vTransform)=>
        {
            vLerp.lerpTime += deltaTime;
            var percent = math.clamp(vLerp.lerpTime / timeInterval, 0, 1);
            vTransform.pos = math.lerp(vLerp.pre.pos, vLerp.target.pos, percent);

            if(isLogicFrame)
            {
                vLerp.pre = vTransform;
                vLerp.target = new VComTransform(){ pos = lTransform.pos};
                vLerp.lerpTime = 0;
            }
        }).Run();
    }
}

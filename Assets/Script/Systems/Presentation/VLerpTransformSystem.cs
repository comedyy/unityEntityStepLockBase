using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class VLerpTransformSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var frameCount = SystemAPI.GetSingleton<ComFrameCount>();

        var isLogicFrame = frameCount.changeFramePresentationTime == UnityEngine.Time.time;
        var logicTime = frameCount.escapedTime;
        var escaped = UnityEngine.Time.time - logicTime;
        var percent = Mathf.Clamp(escaped / ComFrameCount.DELTA_TIME, 0, 1);

        Entities.ForEach((ref LComTransform lTransform, ref VComLerpTransform vLerp, ref VComTransform vTransform)=>{

            if(isLogicFrame)
            {
                
            }

            var fromPos = vLerp.pre.pos;
            var targetPos = translation.Value;
            vTransform.pos = Mathf.lerp(fromPos, targetPos, percent);
        }).Run();
    }
}

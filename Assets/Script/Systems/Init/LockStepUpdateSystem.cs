using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial class LockStepUpdateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        LocalFrameGenerator.Instance.Update();
    }

    protected override void OnDestroy()
    {
        LocalFrameGenerator.Instance.Destroy();
    }
}
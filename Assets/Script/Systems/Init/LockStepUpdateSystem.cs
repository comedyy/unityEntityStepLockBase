using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public partial class LockStepUpdateSystem : SystemBase
{
    protected override void OnUpdate()
    {
        LocalFrame.Instance.Update();
    }

    protected override void OnDestroy()
    {
        LocalFrame.Instance.Destroy();
    }
}
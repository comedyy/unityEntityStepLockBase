using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class FirstSimulationSystem : BaseUnsortSystemGroup
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref DemoComponent c)=>{
            c.value++;
        }).Run();
    }
}

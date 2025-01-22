using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class FirstPresentationSystem : BaseUnsortSystemGroup
{
    protected override void OnUpdate()
    {
        if(SystemAPI.GetSingleton<DemoComponent>().value == 1000)
        {
            GameObject.Destroy(Object.FindAnyObjectByType<Main>());
        }
    }
}

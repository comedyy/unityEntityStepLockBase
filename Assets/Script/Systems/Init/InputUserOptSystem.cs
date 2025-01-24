using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class InputUserOptSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        LocalFrame.Instance.SetRotation(x, y);
    }
}
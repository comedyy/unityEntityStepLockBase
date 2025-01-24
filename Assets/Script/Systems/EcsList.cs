using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EcsList
{
    public static readonly List<Type> InitSystemList = new List<Type>()   
    {
        typeof(InputUserOptSystem),
        typeof(LockStepUpdateSystem),
    };

    public static List<Type> _simulationTypes = new List<Type>{
        typeof(SyncUserInputSystem),
        typeof(FirstSimulationSystem)
    };

    public static List<Type> _presentationTypes = new List<Type>()
    {
        typeof(FirstPresentationSystem),
        typeof(VLerpTransformSystem)
    };
}

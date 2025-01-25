using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Main : MonoBehaviour
{
    World _world;
    // Start is called before the first frame update
    void Start()
    {
        _world = new World("1");
        World.DefaultGameObjectInjectionWorld = _world;

        CreateSingletonEntity();

        var localFrame = new LocalFrameGenerator(1, 0.05f, new BattleStartMessage(){ seed = 1});

        // system group
        var initGroup = _world.GetOrCreateSystemManaged<InitSystemGroup>();
        _world.GetOrCreateSystemManaged<InitializationSystemGroup>().AddSystemToUpdateList(initGroup);

        var fixedTimeGroup = _world.GetOrCreateSystemManaged<FixedTimeSystemGroup>();
        _world.GetOrCreateSystemManaged<SimulationSystemGroup>().AddSystemToUpdateList(fixedTimeGroup);

        var presentaionGroup = _world.GetOrCreateSystemManaged<UnsortedPresentationSystemGroup>();
        _world.GetOrCreateSystemManaged<PresentationSystemGroup>().AddSystemToUpdateList(presentaionGroup);

        // add systems
        foreach(var x in EcsList.InitSystemList)
        {
            initGroup.AddSystemToUpdateList(_world.GetOrCreateSystemManaged(x));
        }

        foreach(var x in EcsList._simulationTypes)
        {
            fixedTimeGroup.AddSystemToUpdateList(_world.GetOrCreateSystemManaged(x));
        }

        // presentaion
        foreach(var x in EcsList._presentationTypes)
        {
            presentaionGroup.AddSystemToUpdateList(_world.GetOrCreateSystemManaged(x));
        }

        // inject
        fixedTimeGroup.InitLogicTime(localFrame);
        World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SyncUserInputSystem>().fetchFrame = localFrame.syncFrameCache;

        // create Logic
        _world.EntityManager.CreateEntity(ArchetypeSnake.entityArchetype);
    }

    void CreateSingletonEntity()
    {
        World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(ComFrameCount));
        World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity(typeof(ComGameState));
    }

    // Update is called once per frame
    void Update()
    {
        _world.Update();
    }

    private void OnDestroy() {
        _world.Dispose();
        _world = null;
    }
}

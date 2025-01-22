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

        _world.GetOrCreateSystemManaged<SimulationSystemGroup>().AddSystemToUpdateList(_world.GetOrCreateSystemManaged<FixedTimeSystemGroup>());
        _world.GetOrCreateSystemManaged<PresentationSystemGroup>().AddSystemToUpdateList(_world.GetOrCreateSystemManaged<UnsortedPresentationSystemGroup>());

        _world.GetOrCreateSystemManaged<FixedTimeSystemGroup>().AddSystemToUpdateList(_world.GetOrCreateSystemManaged<FirstSimulationSystem>());
        _world.GetOrCreateSystemManaged<PresentationSystemGroup>().AddSystemToUpdateList(_world.GetOrCreateSystemManaged<FirstSimulationSystem>());
        _world.EntityManager.CreateEntity(typeof(DemoComponent));
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

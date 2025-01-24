using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ArchetypeSnake : IArchetype
{
    static EntityArchetype _entityArchetype;
    public static EntityArchetype entityArchetype{
        get{
            if(_entityArchetype.Valid) return _entityArchetype;

            _entityArchetype = World.DefaultGameObjectInjectionWorld.EntityManager.CreateArchetype(
                typeof(LComTransform),
                typeof(VComLerpTransform),
                typeof(VComTransform)
            );

            return _entityArchetype;
        }
    }
}

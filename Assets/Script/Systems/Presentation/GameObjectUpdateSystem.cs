using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class GameObjectUpdateSystem : SystemBase
{
    Dictionary<Entity, GameObject> _allGameObject = new Dictionary<Entity, GameObject>();

    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((Entity entity, ref VComTransform transform)=>{
            if(!_allGameObject.TryGetValue(entity, out var obj))
            {
                obj = GameObject.Instantiate(Resources.Load<GameObject>("GameObject"));
                _allGameObject.Add(entity, obj);
            }

            obj.transform.position = new Vector3(transform.pos.x, transform.pos.y);
        }).Run();
    }
}

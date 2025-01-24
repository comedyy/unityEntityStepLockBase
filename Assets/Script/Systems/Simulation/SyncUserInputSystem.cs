using System;
using System.Collections.Generic;
using Unity.Entities;

public partial class SyncUserInputSystem : SystemBase
{
    public Action<int, List<MessageItem>> GetAllMessage;
    List<MessageItem> _lstTemp = new List<MessageItem>();
    protected override void OnUpdate()
    {
        if(GetAllMessage == null)
        {
            UnityEngine.Debug.LogError("GetAllMessage == null");
            return;
        }

        var frameComponent =  SystemAPI.GetSingleton<ComFrameCount>();

        GetAllMessage(frameComponent.currentFrame, _lstTemp);

        if (_lstTemp.Count == 0) return;

        foreach (var message in _lstTemp)
        {
            ProcessMove(message);
        }
    }

    private void ProcessMove(MessageItem message)
    {
        if(message.HasFlag(MessageBit.Dir))
        {
            ref var snake = ref SystemAPI.GetSingletonRW<LComTransform>().ValueRW;
            snake.pos += new UnityEngine.Vector2(message.posItem.dirX / 1000f * 0.05f, message.posItem.dirY / 1000f * 0.05f) ;
        }
    }
}
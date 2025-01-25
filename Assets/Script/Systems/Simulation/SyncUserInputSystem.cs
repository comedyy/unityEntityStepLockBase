using System;
using System.Collections.Generic;
using Deterministics.Math;
using Unity.Entities;

public interface IFetchFrame
{
    void GetAllMessage(int frame, List<MessageItem> messageItems);
}

public partial class SyncUserInputSystem : SystemBase
{
    public IFetchFrame fetchFrame;
    List<MessageItem> _lstTemp = new List<MessageItem>();
    protected override void OnUpdate()
    {
        if(fetchFrame == null)
        {
            UnityEngine.Debug.LogError("GetAllMessage == null");
            return;
        }

        var frameComponent =  SystemAPI.GetSingleton<ComFrameCount>();

        _lstTemp.Clear();
        fetchFrame.GetAllMessage(frameComponent.currentFrame, _lstTemp);

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
            var diff = new fp2(fp.FromRaw(message.posItem.dirX) * ComFrameCount.DELTA_TIME, fp.FromRaw(message.posItem.dirY) * ComFrameCount.DELTA_TIME) ;
            diff *= 3;
            snake.pos += diff;
        }
    }
}
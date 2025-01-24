using Unity.Core;
using Unity.Entities;

public partial class FixedTimeSystemGroup : BaseUnsortSystemGroup
{
    LocalFrame _localFrame;
    internal void InitLogicTime(LocalFrame localFrame)
    {
        _localFrame = localFrame;
    }

    protected override void OnUpdate()
    {
        int needFrame = GetNeedCalFrame();

        if (needFrame <= 0)
        {
            return;
        }

        for(int i = 0; i < needFrame; i++)
        {

            ref var frameCount = ref SystemAPI.GetSingletonRW<ComFrameCount>().ValueRW;
            frameCount.currentFrame++;
            frameCount.escapedTime = frameCount.currentFrame * ComFrameCount.DELTA_TIME;
            frameCount.changeFramePresentationTime = UnityEngine.Time.time;

            LocalFrame.Instance.GameFrame = frameCount.currentFrame;

            base.OnUpdate();  // Update
        }
    }

    private int GetNeedCalFrame()
    {
        var gameStateCom = SystemAPI.GetSingleton<ComGameState>();
        if(gameStateCom.IsEnd) return 0;

        return 1;
    }
}
using Unity.Core;
using Unity.Entities;

public partial class FixedTimeSystemGroup : BaseUnsortSystemGroup
{
    LocalFrameGenerator _localFrame;
    internal void InitLogicTime(LocalFrameGenerator localFrame)
    {
        _localFrame = localFrame;
    }

    protected override void OnUpdate()
    {
        ref var frameCount = ref SystemAPI.GetSingletonRW<ComFrameCount>().ValueRW;
        int needFrame = GetNeedCalFrame(frameCount.currentFrame);

        if (needFrame <= 0)
        {
            return;
        }

        for(int i = 0; i < needFrame; i++)
        {

            frameCount.currentFrame++;
            frameCount.escapedTime = frameCount.currentFrame * ComFrameCount.DELTA_TIME;
            frameCount.frameUnity = UnityEngine.Time.frameCount;

            base.OnUpdate();  // Update
        }
    }

    private int GetNeedCalFrame(int currentFrame)
    {
        var gameStateCom = SystemAPI.GetSingleton<ComGameState>();
        if(gameStateCom.IsEnd) return 0;

        return _localFrame.NeedExcuteFrame(currentFrame);
    }
}
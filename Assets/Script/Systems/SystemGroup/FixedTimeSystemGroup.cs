// using Deterministics.Math;
// using Game.Battle.Init;
// using Game.BattleShare.Controller.BattleData;
using Unity.Core;

public partial class FixedTimeSystemGroup : BaseUnsortSystemGroup
{
    public static FixedTimeSystemGroup Instance;
    // LocalFrame _localFrame;

    private bool _isLogicFrame = false;
    public bool IsLogicFrame => _isLogicFrame;
    
    protected override void OnCreate()
    {
        Instance = this;
        base.OnCreate();
    }

    // internal void InitLogicTime(LocalFrame localFrame)
    // {
    //     _localFrame = localFrame;
    // }

    protected override void OnUpdate()
    {
        int needFrame = GetNeedCalFrame();
        // int needFrame = 1;
        if (needFrame <= 0)
        {
            _isLogicFrame = false;
            World.SetTime(new TimeData(UnityEngine.Time.time, UnityEngine.Time.deltaTime));
        }
        
        for(int i = 0; i < needFrame; i++)
        {
            // if(_localFrame.BattleEnd) return; // 游戏已经结束
            
            // BattleDataController.ElapsedTime += BattleDataController.DeltaTime;
            // BattleDataController.FrameCount ++;
            // BattleDataController.changeFramePresentaionTime = UnityEngine.Time.time;
            // LocalFrame.Instance.GameFrame = BattleDataController.FrameCount;

            World.SetTime(new TimeData(UnityEngine.Time.time, UnityEngine.Time.deltaTime));
            base.OnUpdate();  // Update
            _isLogicFrame = true;
        }
    }

    private int GetNeedCalFrame()
    {
       return 1;
    }
}
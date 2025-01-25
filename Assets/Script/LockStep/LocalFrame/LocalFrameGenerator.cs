

using System;
using System.Collections.Generic;
using Unity.Assertions;
using UnityEngine;

public class LocalFrameGenerator
{
    public static LocalFrameGenerator Instance;
    protected BattleStartMessage _battleStartMessage;
    public int _controllerId = 0;
    public float totalTime;
    protected float _tick;
    public int GeneratedFrames;

    public IFetchFrame syncFrameCache => _syncFrameCache;
    SyncFrameCache _syncFrameCache = new SyncFrameCache();
    InputCache _inputCache;
    public static InputCache inputCache
    {
        get
        {
            return Instance._inputCache;
        }
    }
    
    public LocalFrameGenerator(int id, float tick, BattleStartMessage battleStartMessage)
    {
        Instance = this;
        _controllerId = id;
        _battleStartMessage = battleStartMessage;
        _tick = tick;
        _inputCache = new InputCache(id);
    }

    public void Update()
    {
        var deltaTime = Mathf.Min(Time.deltaTime, _tick);
        totalTime += deltaTime;
        var preFrameSeconds = GeneratedFrames * _tick;
        if(totalTime - preFrameSeconds <_tick)
        {
            return;
        }

        if(_syncFrameCache.Count > 0)
        {
            Debug.LogError("发现frame未取出" + _syncFrameCache.DebugInfo + " currentFrame " + GeneratedFrames);
        }

        GeneratedFrames++;
        _syncFrameCache.AddLocalFrame(GeneratedFrames, _inputCache.FetchItem());
    }
    
    public void Destroy(){}

    public int NeedExcuteFrame(int currentFrame)
    {
        return GeneratedFrames - currentFrame;
    }
}
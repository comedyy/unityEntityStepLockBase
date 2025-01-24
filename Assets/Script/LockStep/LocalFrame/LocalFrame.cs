

using System;
using System.Collections.Generic;
using Unity.Assertions;
using UnityEngine;

public class LocalFrame
{
    public static LocalFrame Instance;
    public int GameFrame;
    protected Dictionary<int , List<MessageItem>> _allMessage = new Dictionary<int, List<MessageItem>>();
    protected BattleStartMessage _battleStartMessage;
    public int _controllerId = 0;
    protected MessageItem? _messageItem;
    public float totalTime;
    protected float _tick;
    public int ReceivedServerFrame;

    public LocalFrame(int id, float tick, BattleStartMessage battleStartMessage)
    {
        Instance = this;
        _controllerId = id;
        _battleStartMessage = battleStartMessage;
        _tick = tick;
    }

    public void Update()
    {
        totalTime += Time.deltaTime;
        var preFrameSeconds = ReceivedServerFrame * _tick;
        if(totalTime - preFrameSeconds <_tick)
        {
            return;
        }

        Assert.AreEqual(_allMessage.Count, 0);

        AddLocalFrame();
    }
    
    public void GetFrameInput(int frame, List<MessageItem> listOut)
    {
        if(_allMessage.TryGetValue(frame, out var list))
        {
            listOut.AddRange(list);
            ListPool<MessageItem>.Release(list);
            _allMessage.Remove(frame);
        }
    }

    public void Destroy(){}

    internal void SetRotation(float xDir, float yDir)
    {
        MakeMessageHead();
        
        var x = _messageItem.Value;
        x.messageBit |= MessageBit.Dir;
        x.posItem = new MessagePosItem(){ dirX = (int)xDir * 1000, dirY = (int)yDir* 1000};
        _messageItem = x;
    }

    private void MakeMessageHead()
    {
        if(_messageItem == null)
        {
            _messageItem = new MessageItem()
            {
                id = (uint)_controllerId,
            };
        }
    }

    void AddLocalFrame()
    {
        ReceivedServerFrame ++;
        if(_messageItem.HasValue)
        {
            var list = ListPool<MessageItem>.Get();
            list.Add(_messageItem.Value);
            _allMessage.Add(ReceivedServerFrame, list);
            _messageItem = null;
        }
    }
}
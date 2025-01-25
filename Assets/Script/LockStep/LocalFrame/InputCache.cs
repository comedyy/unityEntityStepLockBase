

using Deterministics.Math;
using UnityEngine;

public class InputCache
{
    MessageItem? _messageItem;
    int _controllerId;
    public InputCache(int controllerId)
    {
        _controllerId = controllerId;
    }
    
    internal void SetRotation(fp2 dir)
    {
        if(dir.x == 0 && dir.y == 0) return;

        MakeMessageHead();
        
        var x = _messageItem.Value;
        x.messageBit |= MessageBit.Dir;
        x.posItem = new MessagePosItem(){ dirX = (int)dir.x.RawValue, dirY = (int)dir.y.RawValue};
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

    public MessageItem? FetchItem()
    {
        var x = _messageItem;
        _messageItem = null;
        return x;
    }
}
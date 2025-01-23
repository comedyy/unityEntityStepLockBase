using System;
using System.Collections.Generic;
using Unity.Entities;

public partial class InputUserPositionController : SystemBase
{
    public Action<int, List<MessageItem>> GetAllMessage;
    List<MessageItem> _lstTemp = new List<MessageItem>();
    protected override void OnUpdate()
    {
        GetAllMessage();
    }
}
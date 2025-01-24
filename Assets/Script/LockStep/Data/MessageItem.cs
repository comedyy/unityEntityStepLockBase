using System;
using System.Collections;
using System.Collections.Generic;
using LiteNetLib.Utils;
using UnityEngine;

public enum MessageBit : ushort
{
    Dir = 1 << 0,
    Skill = 1 << 1,
    SpeedUp = 1 << 2,
}

[Serializable]
public struct MessageItem
{
    public uint id;
    public MessageBit messageBit;
    public MessagePosItem posItem;
    public MessageSpeedup messageSpeedup;
    public MessageSkillItem messageSkillItem;

    public bool HasFlag(MessageBit bit)
    {
        return (bit & messageBit) > 0;
    }

    public static void ToWriter(NetDataWriter writer, MessageItem messageItem)
    {
        writer.Put((ushort)messageItem.messageBit);
        writer.Put((byte)messageItem.id);

        var messageBit = messageItem.messageBit;
        if ((messageBit & MessageBit.Dir) > 0)
        {
            writer.Put(messageItem.posItem.dirX);
            writer.Put(messageItem.posItem.dirY);
        }

        if ((messageBit & MessageBit.SpeedUp) > 0)
        {
            writer.Put(messageItem.messageSpeedup.isSpeedup);
        }

        if ((messageBit & MessageBit.Skill) > 0)
        {
            writer.Put(messageItem.messageSkillItem.skillIndex);
        }
    }

    public static MessageItem FromReader(NetDataReader reader)
    {
        MessageBit messageBit = (MessageBit)reader.GetUShort();
        var messageItem = new MessageItem()
        {
            id = reader.GetByte(),
            messageBit = messageBit
        };

        if ((messageBit & MessageBit.Dir) > 0)
        {
            messageItem.posItem = new MessagePosItem()
            {
                dirX = reader.GetInt(),
                dirY = reader.GetInt(),
            };
        }

        if ((messageBit & MessageBit.SpeedUp) > 0)
        {
            messageItem.messageSpeedup = new MessageSpeedup()
            {
                isSpeedup = reader.GetBool()
            };
        }

        if ((messageBit & MessageBit.Skill) > 0)
        {
            messageItem.messageSkillItem = new MessageSkillItem()
            {
                skillIndex = reader.GetInt()
            };
        }

        return messageItem;
    }
}


[Serializable]
public struct MessagePosItem
{
    public int dirX;
    public int dirY;
}


[Serializable]
public struct MessageSpeedup
{
    public bool isSpeedup;
}

public struct MessageSkillItem
{
    public int skillIndex;
}
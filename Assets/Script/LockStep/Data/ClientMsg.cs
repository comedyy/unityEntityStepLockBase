
using LiteNetLib.Utils;

public struct BattleStartMessage : INetSerializable
{
    public int seed;

    public void Deserialize(NetDataReader reader)
    {
        seed = reader.GetInt();
    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(seed);
    }
}


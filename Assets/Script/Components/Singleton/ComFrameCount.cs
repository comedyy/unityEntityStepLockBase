

using Unity.Entities;

public struct ComFrameCount : IComponentData
{
    public int currentFrame;
    public float escapedTime;
    public float changeFramePresentationTime;

    public const float DELTA_TIME = 0.05f;
}
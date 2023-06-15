using SFML.Graphics;

namespace Engine;

public static class TextureExt
{
    public static IntRect[] ToSpriteSheet(this Texture texture,int frameSize,int FrameCount)
    {
        List<IntRect> frames = new List<IntRect>();
        for (int i = 0; i < FrameCount; i++)
        {
            frames.Add(new IntRect(frameSize*i, 0, frameSize, frameSize));
        }

        return frames.ToArray();
    }
}
namespace Engine.Sound;

public class SoundPlayer
{
    public List<string> AudioClipsList = new List<string>();
    
    private static Dictionary<string, SFML.Audio.Sound> soundClips = new Dictionary<string, SFML.Audio.Sound>();

    public void LoadAudioClips()
    {
        soundClips.Clear();
        foreach(string pathToClip in AudioClipsList)
        {
            string name = pathToClip.Split('\\').Last().Split('.').First();
            
            soundClips.Add(name, new SFML.Audio.Sound(new SFML.Audio.SoundBuffer(pathToClip)));
        }
    }
    
    public static void PlayAudioClip(string name, bool loop = false)
    {
        if (soundClips.TryGetValue(name, out var audioClip))
        {
            audioClip.Loop = loop;
            audioClip.Play();
        }
    }
    
    public void SetVolume(float volume) 
    {
        foreach (var audioClip in soundClips)
        {
            audioClip.Value.Volume = volume;
        }
    }

    public static SFML.Audio.Sound GetSoundByName(string name)
    {
        if (soundClips.TryGetValue(name, out var audioClip))
        {
            return audioClip;
        }

        return null;
    }
    
}

using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public enum Volume
    {
        none,
        MusicVolume,
        SoundVolume
    }


    public static AudioClip GetRandomAudio(List<AudioClip> options)
    {
        return options[Random.Range(0, options.Count)];
    }

    public static float RandomPitch(Vector2 Range)
    {
        return Random.Range(Range.x, Range.y);
    }


    public static void ReturnVolume(Volume volume, AudioSource source)
    {
        source.volume = PlayerPrefs.GetFloat(volume.ToString());
    }
}

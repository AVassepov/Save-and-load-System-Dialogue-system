using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Sound Library", menuName = "Utility/Sound Library", order = 1)]
public class SoundLibrary : ScriptableSingleton<SoundLibrary>
{
   public List<AudioClip> BookSounds;



   public AudioClip GetRandomAudio( List<AudioClip> options)
   {
      return options[Random.Range(0,options.Count)];
   }

   public float RandomPitch(Vector2 Range)
   {
       return Random.Range(Range.x, Range.y);
   }
   
}

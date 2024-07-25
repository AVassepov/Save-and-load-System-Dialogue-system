using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Sound Library", menuName = "Utility/Sound Library", order = 1)]
public class SoundLibrary : ScriptableObject
{
   public List<AudioClip> Sounds;

   
}

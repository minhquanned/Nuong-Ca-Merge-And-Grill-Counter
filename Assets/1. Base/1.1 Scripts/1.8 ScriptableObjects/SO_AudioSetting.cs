using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Audios", menuName = "Create Audios File")]
public class SO_AudioSetting : ScriptableObject
{
    public List<AudioModel> SFXs;
    public List<AudioModel> Musics;
}

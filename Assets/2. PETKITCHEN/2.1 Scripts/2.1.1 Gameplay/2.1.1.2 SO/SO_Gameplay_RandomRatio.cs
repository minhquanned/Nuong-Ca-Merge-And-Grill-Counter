using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Gameplay_RandomRatio", menuName = "Scriptable Objects/SO_Gameplay_RandomRatio")]
public class SO_Gameplay_RandomRatio : ScriptableObject
{
    public List<RandomWeightEntry> randomWeightEntries;
}

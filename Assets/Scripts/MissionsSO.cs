using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName= "ScripableObject/Mission", order = 1)]
public class MissionsSO : ScriptableObject
{
    public int targetValue;
   public string description;
    public int rewards;
    public MissionType missionType;
    public bool isClaimed = false;
    
}

public enum MissionType
{
    Stars,
    Friends,
    Stardust
}
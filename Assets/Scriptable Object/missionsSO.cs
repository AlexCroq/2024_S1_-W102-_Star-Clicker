using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName= "Mission")]
public class missionsSO : ScriptableObject
{
    public string missionDescription;
    public int missionReward;
}

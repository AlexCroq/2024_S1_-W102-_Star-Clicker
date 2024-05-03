using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPlayer
{
    private const String ALL_DATA = "all_data"; // key for privacy
    private static AllData allData; // all data variable
 static DataPlayer(){
    allData = JsonUtility.FromJson<AllData>(PlayerPrefs.GetString(ALL_DATA)); // using get string to get data
    if (allData == null){
        var defaultStarId = 1;
        allData = new AllData{
            starlist = new List<int> {defaultStarId}
        };
        SaveData();
    }
 
    }
  private static void SaveData(){
    var data = JsonUtility.ToJson(allData);  
    PlayerPrefs.SetString(ALL_DATA, data);  // using set string to save data
    }
    public static bool IsOwnedStarWithId(int id){
        return allData.IsOwnedStarWithId(id);
    }

    public static void AddStar(int id){
       allData.AddStar(id);
       //SaveData();
    }

    public static void SellStar(int id){
        allData.SellStar(id);
    }


}
public class AllData{
public List<int> starlist;

public bool IsOwnedStarWithId(int id){
    return starlist.Contains(id);
}

public void AddStar(int id){
    if(!IsOwnedStarWithId(id)){
        starlist.Add(id);
    }
}

public void SellStar(int id){
    if(IsOwnedStarWithId(id)){
        starlist.Remove(id);
    }
}
}
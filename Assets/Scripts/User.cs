using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class User{

    private string username {get; set;}
    private string password_hashed;
    private int star_dust {get; set;}
    private int user_id;
    private List<int> friendsList;
    private List<int> starIDList;


    public User(){
        username = "";
        starIDList = new List<int>();
        star_dust = -1;

    }

    #region starManagement

    public bool IsOwned(int starId){
        for(int i =0; i<starIDList.Count;i++){
            if(starIDList[i] == starId){
                return true;
            }
        }
        return false;
    }

    public void BuyStar(int id){
        if(!IsOwned(id)){
            starIDList.Add(id);
        }
    }

    public void SellStar(int id){
        if(IsOwned(id)){
            starIDList.Remove(id);
        }
    }

    #endregion

    public List<int> getStarIDList() {
        return starIDList;
    }

}
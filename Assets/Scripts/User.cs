using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class User{

    private string username {get; set;}
    private int star_dust {get; set;}
    private List<int> starIDList;

    private List<Star> stars;


    public User(){
        username = "";
        starIDList = new List<int>();
        star_dust = -1;

    }

    public void LoadStar(){
        StarDataLoader sdl = new();
        stars = sdl.LoadData();
        foreach(Star star in stars){
            int idStar =(int)star.catalog_number;
            starIDList.Add(idStar);
        }  
    }



    public void Login(){
        // will be the login method.
        // For the moment we can mock a user for test purpose

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
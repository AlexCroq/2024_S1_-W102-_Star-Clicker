using System.Collections.Generic;
using System.Data.Common;
using PlasticPipe.Tube;
using UnityEngine;

public class User{

    private string username {get; set;}
    private string password_hashed;
    private int star_dust {get; set;}
    private int user_id;
    private List<int> friendsList;
    private List<int> starIDList;
    private CardScript cardScript = new CardScript();
    private float starPower;

  

    public User(){
        username = "";
        starIDList = new List<int>();
        star_dust = 0;
        starPower = 1;

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

    public void AddScore(){
        star_dust =star_dust + (int)starPower;
    }

    #endregion

    public List<int> getStarIDList() {
        return starIDList;
    }

    public void AddStarIDList(int id){
        starIDList.Add(id);
    }

    public void RemoveStarIDList(int id){
        starIDList.Remove(id);
    }

    public int getStar_dust(){
        return star_dust;
    }

    public void setStar_dust(int i){
        star_dust += i;
    }

    public void setStarPower(float i){
        starPower = i +1;
    }

    public float getStarPower(){
        return starPower;
    }
}
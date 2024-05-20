using System.Collections.Generic;
using System.Data.Common;
using PlasticPipe.Tube;
using UnityEngine;
using System.Text;

[System.Serializable]
public class User{

    public string username;
    public string password_hashed;
    public int star_dust;
    public int user_id;
    public List<int> friendsList;
    public List<int> starIDList;
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

        public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Username: ").Append(username).Append("\n");
        sb.Append("Star Dust: ").Append(star_dust).Append("\n");
        sb.Append("User ID: ").Append(user_id).Append("\n");
        sb.Append("Friends List: [");
        if (friendsList != null)
        {
            foreach (int friendId in friendsList)
            {
                sb.Append(friendId).Append(", ");
            }
        }
        sb.Append("]\n");
        sb.Append("Star ID List: [");
        if (starIDList != null)
        {
            foreach (int starId in starIDList)
            {
                sb.Append(starId).Append(", ");
            }
        }
        sb.Append("]\n");
        return sb.ToString();
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

    public void increaseStarDust(int i){
        star_dust += i;
    }

    public void setStarPower(float i){
        starPower = i +1;
    }

    public float getStarPower(){
        return starPower;
    }
}
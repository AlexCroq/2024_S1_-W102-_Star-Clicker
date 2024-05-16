using System.Collections.Generic;
using System.Data.Common;
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


}
using UnityEngine;

public class User : MonoBehaviour{

    private string username {get; set;}
    private int star_dust {get; set;}
    private Sky sky {get; set;}

    public User(){
        username = "";
        sky = new Sky();
        star_dust = -1;

    }



    public void login(){
        // will be the login method.
        // For the moment we can mock a user for test purpose

    }

    public void buyStar(){
        // for @Giang 
    }

    public void sellStar(){
        // for @Giang 
    }


}
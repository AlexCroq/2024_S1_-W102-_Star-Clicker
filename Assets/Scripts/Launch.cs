using UnityEngine;

public class Launch: MonoBehaviour{
    private bool hasStart = false;
    public void Start(){
        if(!hasStart){
            UserManager.InitializeUser();
            hasStart = true;
        }

    
    }

}
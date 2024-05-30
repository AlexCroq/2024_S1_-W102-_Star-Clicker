using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallangeList : MonoBehaviour
{
    
    
    
    
    
    public User currentUser;
    public MissionsSO[] missionSO;
    public Missions[] missionsPanel;
    
    void Start()
    {
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();
        LoadPanel();
        updateStatus();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LoadPanel(){
        for(int i  = 0 ; i < missionSO.Length; i++ ){
           missionsPanel[i].descriptionTxt.text = missionSO[i].description;
           missionsPanel[i].rewardTxt.text = missionSO[i].rewards.ToString();
        }
    }

    private bool IsMissionCompleted(MissionsSO mission)
    {
        switch (mission.missionType)
        {
            case MissionType.Stars:
                return currentUser.getStarIDList().Count>= mission.targetValue;
            case MissionType.Friends:
                return currentUser.getFriendIDList().Count >= mission.targetValue;
            case MissionType.Stardust:
                return currentUser.getStar_dust() >= mission.targetValue;     // i dont't know if i use the correct variable
            default:
                return false;
        }
    }

    public void ClaimReward(int btnNo){
        missionsPanel[btnNo].doneButton.gameObject.SetActive(false);
        missionsPanel[btnNo].doneStatus.gameObject.SetActive(true);
        currentUser.increaseStarDust(missionSO[btnNo].rewards);
    }

    public void updateStatus(){
        for(int i = 0 ; i < missionSO.Length;i++){
            if(/*IsMissionCompleted(missionSO[i])*/ missionSO[i].targetValue < 100 ){
                missionsPanel[i].doneButton.gameObject.SetActive(true);
                missionsPanel[i].ongoingStatus.gameObject.SetActive(false);
            }
        }
    }

    

    

}

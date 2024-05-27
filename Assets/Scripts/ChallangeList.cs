using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallangeList : MonoBehaviour
{
    
    public Text missionDescriptionTxt;
    
    private List<missions> missions;
    public Button claimMission;
    public Text doneStatus;
    public Text ongoingStatus;
    private User currentUser;
    
    void Awake()
    {
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();
        missions = new List<missions>{
            new missions("Obtain 5 Stars", user => user.starIDList != null && user.starIDList.Count >= 5),
            new missions("Obtain 10 Stars", user => user.starIDList != null && user.starIDList.Count >= 10),
            new missions("Obtain 20 Stars", user => user.starIDList != null && user.starIDList.Count >= 20),
            new missions("Obtain 100 Stars", user => user.starIDList != null && user.starIDList.Count >= 100),
            new missions("Have 5 Friends", user => user.friendsList != null && user.friendsList.Count >= 5),
            new missions("Have 10 Friends", user => user.friendsList != null && user.friendsList.Count >= 10),
            new missions("Have 20 Friends", user => user.friendsList != null && user.friendsList.Count >= 20),
            new missions("Have 100 Friends", user => user.friendsList != null && user.friendsList.Count >= 100),
            new missions("Have 1000 Stardust", user => user.star_dust >= 1000),
            new missions("Have 1500 Stardust", user => user.star_dust >= 1500),
            new missions("Have 1600 Stardust", user => user.star_dust >= 1600)
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

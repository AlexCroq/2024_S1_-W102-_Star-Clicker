using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallangeList : MonoBehaviour
{
    
    public Text missionDescriptionTxt;
    
    private List<Mission> missions;
    public Button claimMission;
    public Text doneStatus;
    public Text ongoingStatus;
    private User currentUser;
    
    void Awake()
    {
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();
        missions = new List<mission>{
          new mission("Obtain 5 Stars", user => user.starIDList != null && user.starIDList.Count >= 5),
            new mission("Obtain 10 Stars", user => user.starIDList != null && user.starIDList.Count >= 10),
            new mission("Obtain 20 Stars", user => user.starIDList != null && user.starIDList.Count >= 20),
            new mission("Obtain 100 Stars", user => user.starIDList != null && user.starIDList.Count >= 100),
            new mission("Have 5 Friends", user => user.friendsList != null && user.friendsList.Count >= 5),
            new mission("Have 10 Friends", user => user.friendsList != null && user.friendsList.Count >= 10),
            new mission("Have 20 Friends", user => user.friendsList != null && user.friendsList.Count >= 20),
            new mission("Have 100 Friends", user => user.friendsList != null && user.friendsList.Count >= 100),
            new mission("Have 1000 Stardust", user => user.star_dust >= 1000),
            new mission("Have 1500 Stardust", user => user.star_dust >= 1500),
            new mission("Have 1600 Stardust", user => user.star_dust >= 1600)
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

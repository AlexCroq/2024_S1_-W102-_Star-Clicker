using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlasticPipe.PlasticProtocol.Messages;


public class Clicker : MonoBehaviour
{
  
    public float powerPerSecond = 1.0f;
    public TMP_Text powerText;
    public TMP_Text powerStar;

    private User currentUser;

    public void Start()
    {
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();

    }
    public void Awake()
    {
        
    }

    void Update()
    {
        powerText.text = "Total Star Dust: " + currentUser.getStar_dust();
        powerStar.text = "Star power: " + (int)currentUser.getStarPower();
    }

    public void MainButtonPress()
    {
        currentUser.AddScore();
    }
}

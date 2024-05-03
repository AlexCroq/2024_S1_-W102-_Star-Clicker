using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Clicker : MonoBehaviour
{
  
    public int power = 0;
    public float powerPerSecond = 1.0f;
    public TMP_Text powerText;

    void Start()
    {
        
    }

    void Update()
    {
        powerText.text = "Total Star Dust: " + power;
    }

    public void MainButtonPress() {
        power++;
    }
}

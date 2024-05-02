using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Clicker : MonoBehaviour
{
  
    public int power = 0;
    public Text powerText;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class afkBoost : MonoBehaviour
{
    public int power = 0;
    public Text powerText;
    public float autoHarvestDuration = 10f;
    private bool autoHarvestActive = false;
    private float autoHarvestTimer = 0f;

    void Update()
    {
        powerText.text = "Total Star Dust: " + power;

        if (autoHarvestActive)
        {
            autoHarvestTimer -= Time.deltaTime;
            if (autoHarvestTimer <= 0)
            {
                autoHarvestActive = false;
                HarvestPower();
            }
        }
    }

    public void MainButtonPress()
    {
        power++;
    }

    public void StartAutoHarvest()
    {
        autoHarvestActive = true;
        autoHarvestTimer = autoHarvestDuration;
    }

    private void HarvestPower()
    {
        power++;
    }
}

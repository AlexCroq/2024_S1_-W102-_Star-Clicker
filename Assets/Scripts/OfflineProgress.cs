using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class OfflineProgress : MonoBehaviour
{
    public TMP_Text offlineTimeText;
    public TMP_Text pointsGainedText;
    public GameObject offlinePanel;
    private User currentUser;

    void Start()
    {
        if (PlayerPrefs.HasKey("exitTime"))
        {
            currentUser = UserManager.currentUser;
            offlinePanel.SetActive(true);
            DateTime lastTime = DateTime.Parse(PlayerPrefs.GetString("exitTime"));
            DateTime currentTime = DateTime.Now;

            TimeSpan timeAway = currentTime - lastTime;

            offlineTimeText.text = string.Format("{0} Days {1} Hours {2} Mins {3} Secs", timeAway.Days, timeAway.Hours, timeAway.Minutes, timeAway.Seconds);

            Clicker clickerInstance = GameObject.FindObjectOfType<Clicker>();
            float powerGain = clickerInstance.powerPerSecond * (float)timeAway.TotalSeconds;
            pointsGainedText.text = powerGain.ToString("0.00");

            currentUser.setStar_dust((int)powerGain);
        }
        else
        {
            offlinePanel.SetActive(false);
        }
    }

    public void CloseOfflinePanel()
    {
        offlinePanel.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("exitTime", DateTime.Now.ToString());
    }
}

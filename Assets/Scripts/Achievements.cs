using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievements : MonoBehaviour
{
    public GameObject panel; 
    public GameObject userEntryPrefab; 
    public TextMeshProUGUI statusText;

    void Start()
    {
        StartCoroutine(DisplayLeaderboard());
    }

    IEnumerator DisplayLeaderboard()
    {
       
        List<User> users = UserDatabaseManager.Instance.GetUsers();

        if (users == null)
        {
            statusText.text = "Error loading users.";
            yield break;
        }

        users.Sort((x, y) => y.getStarPower().CompareTo(x.getStarPower()));

        foreach (var user in users)
        {
            GameObject userEntry = Instantiate(userEntryPrefab, panel.transform);
            TextMeshProUGUI[] texts = userEntry.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = user.username; 
            texts[1].text = user.getStarPower().ToString(); 
        }
    }
}

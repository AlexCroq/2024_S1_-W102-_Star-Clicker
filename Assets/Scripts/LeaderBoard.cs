using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour
{
    public GameObject panel; 
    public GameObject userEntryPrefab; 
    public TextMeshProUGUI statusText; 
    public string mode;
    public TextMeshProUGUI button_text;

    public Transform contentArea;
    public TextMeshProUGUI headerText;

    void Start()
    {
        mode = "leaderboard";
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
        
        statusText.text = "";
        if (mode=="leaderboard"){
            users.Sort((x, y) => y.star_dust.CompareTo(x.star_dust));
        }
        else if  (mode=="achievements"){
            users.Sort((x, y) => y.getStarPower().CompareTo(x.getStarPower()));
        }

        int y_position=0;
        foreach (var user in users)
        {
            GameObject userEntry = Instantiate(userEntryPrefab, contentArea.transform);
            TextMeshProUGUI[] texts = userEntry.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = user.username; 
            if (mode=="leaderboard"){
                texts[1].text = user.star_dust.ToString(); 
            }
            else if  (mode=="achievements"){
                texts[1].text = user.getStarPower().ToString(); 
            }

            //new position of object is y_pos
            RectTransform rectTransform = userEntry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y_position);
    
            y_position -= 200;
        }
    }

    public void changeStats(){
        button_text.text = mode;
        if(mode == "leaderboard"){
            mode = "achievements";
            headerText.text =  char.ToUpper(mode[0]) + mode.Substring(1);
        } else if(mode == "achievements"){
            mode = "leaderboard";
            headerText.text =  char.ToUpper(mode[0]) + mode.Substring(1);
        }
        RemoveAllCards();
        StartCoroutine(DisplayLeaderboard());

    }

    private void RemoveAllCards()
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }
    }


}

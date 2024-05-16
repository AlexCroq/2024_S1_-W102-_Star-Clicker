using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class starInfoScript : MonoBehaviour
{
    public TextMeshProUGUI starName;
    public TextMeshProUGUI starDescr;
    private int cardStarId = -1;
    private string starDescription;
    private StarInfo starInfo;
    private GameObject infoCard;
    
    public void Update()
    {
        if (cardStarId != -1 && starDescr.text == "Waiting for description..." && starInfo != null)
        {
            starInfo.GetStringStarInfo(cardStarId);
            starDescription = starInfo.getStarDescription();
            if (starDescription != null && starDescription.Length > 0)
            {
                starDescr.text = starDescription;
            }
        }
    }

    public void setCardData(int starID)
    {
        EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (eventSystem != null)
        {
            starInfo = eventSystem.GetComponent<StarInfo>();
            if (starInfo != null)
            {
                cardStarId = starID;
                starName.text = $"HR {cardStarId}";
            }
            else
            {
                Debug.LogError("StarInfo component not found on EventSystem.");
            }
        }
        else
        {
            Debug.LogError("EventSystem not found in the scene.");
        }
    }

    public void Destroy(){
        infoCard = GameObject.Find("Star Info Card(Clone)");
        starDescr.text = "Waiting for description...";
        cardStarId = -1;
        starInfo.setStarDescription("");
        Destroy(infoCard);
    }
}

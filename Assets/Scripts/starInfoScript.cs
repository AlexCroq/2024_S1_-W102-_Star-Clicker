using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class starInfoScript : MonoBehaviour
{
    public TextMeshProUGUI starName;
    public TextMeshProUGUI starDescr;
    public GameObject canvas;
    public void setCardData(int starID){
        starName.text = $"HR {starID}";
        EventSystem eventSystem= GameObject.FindObjectOfType<EventSystem>();
        StarInfo starInfo = eventSystem.GetComponent<StarInfo>();
        starDescr.text = starInfo.GetStringStarInfo(starID);
    }
}

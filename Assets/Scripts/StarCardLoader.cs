using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using System.Threading;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class StarCardLoader : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject starInfoPrefab;
    public Transform contentArea;
    public TextMeshProUGUI star_dustUI;

    public List<int> starsIDList; // List of your star data

    public float spacing = 10f; // Spacing between cards

    public int starListType;
    private User currentUser;
    private float totalHeight = 0f;
    public TextMeshProUGUI inventoryShopButtonText;

    void Start()
    {   
        currentUser = UserManager.currentUser;
        refreshContent();
        
        
    }
    public void refreshContent(){
        RemoveAllCards();
        refreshStarPower();
        star_dustUI.text = $"Star dust :{currentUser.getStar_dust()}";
        if (starListType == 0){
            // Show shopping list
            starsIDList = RandomizeShop();
        }
        else if (starListType == 1){
            // Show inventory list
            starsIDList = currentUser.getStarIDList();
        }
        totalHeight = 0f;
        foreach (int starID in starsIDList){
            GameObject card = Instantiate(cardPrefab, contentArea);

            // Set the position of the card
            RectTransform cardRectTransform = card.GetComponent<RectTransform>();
            cardRectTransform.anchoredPosition = new Vector2(0f, -totalHeight);

            CardScript cardScript = card.GetComponent<CardScript>();
            cardScript.setCardData(starID);
            totalHeight += cardRectTransform.sizeDelta.y + spacing;


            // Adding listeners on cards click 
            EventTrigger eventTrigger = card.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {OnCardClicked(starID);});
            eventTrigger.triggers.Add(entry);

        }

        RectTransform contentRectTransform = contentArea.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, totalHeight);
        
    }

    private void OnCardClicked(int starID)
    {
        GameObject starCard = Instantiate(starInfoPrefab, transform.position, Quaternion.identity);
        starInfoScript infoCardScript = starCard.GetComponent<starInfoScript>();
        infoCardScript.setCardData(starID);

    }



    private void RemoveAllCards()
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }
    }



    private List<int> RandomizeShop(){
        System.Random random = new System.Random();
        List<int> shopIDList = new List<int>();
        StarDataLoader sdl = new();
        List<Star> stars = sdl.LoadData();
        int fullList =0;
        bool class1 = true;
        bool class2 = true;
        bool class3 = true;
        bool class4 = true;
        while (fullList!=4){
            int randomNumber = random.Next(1, 9111);
                foreach(Star star in stars){
                    if(star.catalog_number==randomNumber){
                    if(star.getStarClass()==1 & class1 ){
                        shopIDList.Add(randomNumber);
                        class1=false;
                        fullList+=1;
                    }
                    else if(star.getStarClass()==2 & class2 ){
                        shopIDList.Add(randomNumber);
                        class2=false;
                        fullList+=1;
                    }
                    else if(star.getStarClass()==3 & class3 ){
                        shopIDList.Add(randomNumber);
                        class3=false;
                        fullList+=1;
                    }
                    else if(star.getStarClass()==4 & class4){
                        shopIDList.Add(randomNumber);
                        class4=false;
                        fullList+=1;
                    }
                    }
                }
        }
        shopIDList.Sort();
        return shopIDList;
   }

   private void refreshStarPower(){
    float power=0;
    foreach(int i in currentUser.getStarIDList()){
        StarDataLoader sdl = new();
        List<Star> stars = sdl.LoadData();
        foreach(Star star in stars){
                    if(star.catalog_number==i){
                        power += star.size;
                    }
    }
   }
    currentUser.setStarPower(power);
   }
}

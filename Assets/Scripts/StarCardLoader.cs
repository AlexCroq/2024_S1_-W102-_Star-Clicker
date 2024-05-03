using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Codice.Client.Selector;
using System.Threading;
using TMPro;

public class StarCardLoader : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform contentArea;
    public List<int> starsIDList; // List of your star data

    public float spacing = 10f; // Spacing between cards

    public int starListType;
    private User currentUser;
    private float totalHeight = 0f;
    public TextMeshProUGUI inventoryShopButtonText;

    void Start()
    {
        currentUser = loadUser();
        refreshContent();
    }
    private void refreshContent(){
        RemoveAllCards();
        if (starListType == 0){
            // Show shopping list
            starsIDList = RandomizeShop();
        }
        else if (starListType == 1){
            // Show inventory list
            starsIDList = currentUser.getStarIDList();
        }

        foreach (int starID in starsIDList){
            GameObject card = Instantiate(cardPrefab, contentArea);

            // Set the position of the card
            RectTransform cardRectTransform = card.GetComponent<RectTransform>();
            cardRectTransform.anchoredPosition = new Vector2(0f, -totalHeight);

            CardScript cardScript = card.GetComponent<CardScript>();
            cardScript.setCardData(starID);
            totalHeight += cardRectTransform.sizeDelta.y + spacing;
        }

        RectTransform contentRectTransform = contentArea.GetComponent<RectTransform>();
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, totalHeight);
    }

    private void RemoveAllCards()
    {
        foreach (Transform child in contentArea)
        {
            Destroy(child.gameObject);
        }
        totalHeight = 0f; // Reset total height after removing cards
    }

    private User loadUser(){
        // TODO implement in SPRINT 2
        currentUser = new User();
        currentUser.BuyStar(1);
        currentUser.BuyStar(10);
        return currentUser;
    }

    private List<int> RandomizeShop(){
        System.Random random = new System.Random();
        List<int> shopIDList = new List<int>();
        for (int i = 0; i < 5; i++){
            int randomNumber = random.Next(1, 9111);
            shopIDList.Add(randomNumber);
        }
        return shopIDList;
   }


}

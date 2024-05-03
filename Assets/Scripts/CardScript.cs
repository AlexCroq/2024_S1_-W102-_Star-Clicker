using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardScript : MonoBehaviour
{
   public int id;
   public int price;
   public TextMeshProUGUI priceTxt; 
   public TextMeshProUGUI idTxt;
   public Button purchaseBtn;
   
   public Button sellBtn;
   private User currentUser;

    public void Awake(){
        currentUser = loadUser();
        purchaseBtn.onClick.AddListener(OnPurchase);
        sellBtn.onClick.AddListener(OnSale);
   }

    public void OnPurchase(){
      currentUser.BuyStar(id);
      setCardData(id);
   }

    public void OnSale(){
      currentUser.SellStar(id);
      setCardData(id);
   }

    public User loadUser(){
        // TODO implement in SPRINT 2
        currentUser = new User();
        currentUser.BuyStar(1);
        currentUser.BuyStar(10);
        return currentUser;
    }

    public void setCardData(int starID){
        var IsOwned = currentUser.IsOwned(starID);
        idTxt.text = starID.ToString();
        if(IsOwned){
            purchaseBtn.enabled = false;
            sellBtn.enabled = true;
            sellBtn.gameObject.SetActive(true);
            purchaseBtn.gameObject.SetActive(false);
            priceTxt.text = "owned";
        }else{
            purchaseBtn.enabled = true;
            sellBtn.enabled = false;
            sellBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
            setPrice(starID);
            priceTxt.text = price.ToString();
        }

    }

    

    private void setPrice(int id){
        bool flag = false;
        StarDataLoader sdl = new();
        List<Star> stars = sdl.LoadData();
        foreach (Star star in stars) {
            if (star.catalog_number == id){
                flag = true;
                price = id *61; // TODO change logic
            }
        }
        if(!flag){
            price = -1;
        }
    }

}

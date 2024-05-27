using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Data.Common;
using System.Threading.Tasks;

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
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();
        purchaseBtn.onClick.AddListener(OnPurchase);
        sellBtn.onClick.AddListener(OnSale);
   }

    public void OnPurchase(){
        BuyStar(Int32.Parse(idTxt.text));
        setCardData(Int32.Parse(idTxt.text));
   }

    public void OnSale(){
        setPrice(Int32.Parse(idTxt.text));
        SellStar(Int32.Parse(idTxt.text));
        setCardData(Int32.Parse(idTxt.text));
   }

    public void setCardData(int starID){
        var IsOwned = currentUser.IsOwned(starID);
        idTxt.text = starID.ToString();
        id = Int32.Parse(idTxt.text);
        if(IsOwned){
            purchaseBtn.enabled = false;
            sellBtn.enabled = true;
            sellBtn.gameObject.SetActive(true);
            purchaseBtn.gameObject.SetActive(false);
            priceTxt.text = "owned";
        }else{
            purchaseBtn.enabled = true;
            sellBtn.enabled = false;
            setPrice(starID);
            sellBtn.gameObject.SetActive(false);
            purchaseBtn.gameObject.SetActive(true);
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
                price = (int)(star.size*10000);
            }
        }
        
        if(!flag){
            price = -1;
        }
    }

    public void BuyStar(int id){
    if(!currentUser.IsOwned(id) & currentUser.getStar_dust()>price){
        currentUser.AddStarIDList(id);
        currentUser.increaseStarDust(-price);
        }
    }

    public void SellStar(int id){
    if(currentUser.IsOwned(id)){
        currentUser.RemoveStarIDList(id);
        currentUser.increaseStarDust((int)(0.5*price));
        }
    }



}

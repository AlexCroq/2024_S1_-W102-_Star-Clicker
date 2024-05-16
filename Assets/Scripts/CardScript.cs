using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Codice.Client.Common;
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

   public GameObject canvasPrefab;

   private int priceMin=10000000;

   private int priceMax;


    public void Awake(){
        currentUser = UserManager.currentUser;
        purchaseBtn.onClick.AddListener(OnPurchase);
        sellBtn.onClick.AddListener(OnSale);
   }

    public void OnPurchase(){
      BuyStar(Int32.Parse(idTxt.text));
      setCardData(Int32.Parse(idTxt.text));
   }

    public void OnSale(){
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
        currentUser.setStar_dust(-price);
        }
    }

    public void SellStar(int id){
    if(currentUser.IsOwned(id)){
        currentUser.RemoveStarIDList(id);
        currentUser.setStar_dust((int)0.5*price);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopElement : MonoBehaviour
{
   public int id;
   public int price;
   public Text priceTxt; 
   public Text idTxt;
   public Button purchaseBtn;
   
   public Button sellBtn;
   public void Awake(){
    
    purchaseBtn.onClick.AddListener(OnPurchase);
    sellBtn.onClick.AddListener(OnSale);
   UpdateView();
   }
    
    public void SetData(int id){
        this.id = id;
        price = id*10;

        UpdateView();
    }
   private void UpdateView(){
    var IsOwned = DataPlayer.IsOwnedStarWithId(id);
    priceTxt.text = price.ToString();
    idTxt.text = id.ToString();
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
        priceTxt.text = price.ToString();
    }


    
   } 
   public void OnPurchase(){
      DataPlayer.AddStar(id);
      UpdateView();
   }

   public void OnSale(){
      DataPlayer.SellStar(id);
      UpdateView();
   }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIBuyElement : MonoBehaviour
{
   public int id;
   public int price;
   public TextMeshProUGUI priceTxt; 
   public TextMeshProUGUI idTxt;
   public Button purchaseBtn;
   
   private List<int> buyIDList;



   public void RandomShop(){
        System.Random random = new System.Random();
        for (int i = 0; i < 5; i++)
        {
            int randomNumber = random.Next(1, 9111);
            buyIDList.Add(randomNumber);
        }

   }

    
   public void SetBuyData(int i){

   }

}

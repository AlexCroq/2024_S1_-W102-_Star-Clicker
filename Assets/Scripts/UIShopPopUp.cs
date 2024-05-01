using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopPopUP : MonoBehaviour
{
    public UIShopElement[] shopElements;

    private void OnValidate(){
  if(shopElements == null || shopElements.Length == 0){
    shopElements = GetComponentsInChildren<UIShopElement>();
  }
}



    private void SetData(){
        for(int i = 0 ; i< shopElements.Length;i++){
            shopElements[i].SetData(i+1);
        }
    }
    public void Awake(){
        
        SetData();
    }
}

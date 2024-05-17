using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TMPro;

public class StarInfo : MonoBehaviour
{   [System.Serializable]
    public class WikipediaResponse
    {
        public string extract;
    }
    private string starDescription;
    private List<Star> stars;

    public IEnumerator GetWikipediaPage(float catalog_number,Action<string> onSuccess, Action<string> onError){
        string fullUrl ="https://en.wikipedia.org/api/rest_v1/page/summary/HR_" + catalog_number;
        UnityWebRequest request = UnityWebRequest.Get(fullUrl);
        // send the request
        yield return request.SendWebRequest();
        // Error check
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            string fastDescription = "This star is named HR " + catalog_number;
            onError?.Invoke(fastDescription);
            yield break;
        }

        WikipediaResponse response = JsonUtility.FromJson<WikipediaResponse>(request.downloadHandler.text);
        string longDescription = response.extract;
        onSuccess?.Invoke(longDescription);
    }

    public void FetchStarDescription(float catalog_number){
        StartCoroutine(GetWikipediaPage(catalog_number,OnStarDescriptionReceived, OnStarDescriptionError));
    }


    private void OnStarDescriptionReceived(string description){
        starDescription = description;
        
    }   

    private void OnStarDescriptionError(string fastDescription){
        starDescription = fastDescription;
    }


    public void GetStringStarInfo(float starNumber){
        FetchStarDescription(starNumber);
    }

    public String getStarDescription(){
        return starDescription;
    }

    public void setStarDescription(string description){
        starDescription =description;
    }
}

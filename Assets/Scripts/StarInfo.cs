using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common.GameUI;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using TMPro;

public class StarInfo : MonoBehaviour
{   [System.Serializable]
    public class WikipediaResponse
    {
        public string extract;
    }

    //[SerializeField] private TextMeshProUGUI _StarNumber;
    //[SerializeField] private TextMeshProUGUI _StarDescription;

    
    public IEnumerator GetWikipediaPage(float catalog_number,Action<string> onSuccess, Action<string> onError){
    //Build the full URL
    string fullUrl ="https://en.wikipedia.org/api/rest_v1/page/summary/HR_" + catalog_number;
    

    // Create the request
    UnityWebRequest request = UnityWebRequest.Get(fullUrl);

    // send the request
    yield return request.SendWebRequest();

    // Error check
    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    {
        string fastDescription = "This star is named HR "+catalog_number;
        onError?.Invoke(fastDescription);
        yield break;
    }

    

    WikipediaResponse response = JsonUtility.FromJson<WikipediaResponse>(request.downloadHandler.text);
    string starDescription = response.extract;
    onSuccess?.Invoke(starDescription);
    }

    public void FetchStarDescription(float catalog_number)
    {
        StartCoroutine(GetWikipediaPage(catalog_number,OnStarDescriptionReceived, OnStarDescriptionError));
    }


    private void OnStarDescriptionReceived(string description)
    {
        Debug.Log(description);
    }   

    private void OnStarDescriptionError(string fastDescription)
    {
        Debug.Log(fastDescription);
    }
    private List<Star> stars;
    private string descr;
    public void GetStarInfo(float starNumber){
        // Loading the skymap
        StarDataLoader sdl = new();
        stars = sdl.LoadData();
        foreach (Star star in stars){
            if(star.catalog_number==starNumber){
            FetchStarDescription(starNumber);
            return;
            }
        }
    }

    

    void Start(){
        GetStarInfo(4555);
    }

}

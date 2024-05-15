using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class UserLoader : MonoBehaviour
{
    private static string userPhpScriptUrl = "https://piraeiterie.fr/users.php";

    private List<User> users = new List<User>();
    public TextMeshProUGUI text;

    void Start(){
        StartCoroutine(LoadUsers());
    }

    IEnumerator LoadUsers()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(userPhpScriptUrl);
        webRequest.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError){
            Debug.LogError("Error: " + webRequest.error);
        }
        else{
            string jsonResponse = webRequest.downloadHandler.text;
            users = ParseUsers(jsonResponse);
        }
    }

    public List<User> ParseUsers(string jsonResponse){
        jsonResponse = jsonResponse.Replace("\"[", "[").Replace("]\"", "]");
        UserWrapper userWrapper = JsonUtility.FromJson<UserWrapper>("{\"users\":" + jsonResponse + "}");
        return userWrapper.users;
    }

    [System.Serializable]
    public class UserWrapper {
        public List<User> users;
    }    
    public void assignUserText(){

        foreach (User user in users)
        {
            text.text += user.ToString() + ",";
        }
        
    }

    public List<User> GetUsers(){
        return users;
    }
}

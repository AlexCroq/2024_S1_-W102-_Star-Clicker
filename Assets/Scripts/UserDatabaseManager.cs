using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using System.Security.Cryptography;
using System.Text;

public class UserDatabaseManager : MonoBehaviour
{
    private static string userPhpScriptUrl = "https://piraeiterie.fr/users.php";

    private List<User> users = new List<User>();
    private static UserDatabaseManager instance;

    public bool CurrentSaveStatus = false;
    void Start(){
        StartCoroutine(LoadUsers());
    }

    public static UserDatabaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UserDatabaseManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("UserDatabaseManager");
                    instance = go.AddComponent<UserDatabaseManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        // Ensure there's only one instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator LoadUsers()
    {
        // Loads all users from database
        // If app is scaling be careful with the use of this method.
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

    public IEnumerator SaveNewUser(string username, string password)
    {
        // Saves user in Database from username and password
        if(!UsernameExists(username)){
            CurrentSaveStatus = false;
        
            string jsonPayload = "{\"username\": \"" + username + "\", \"password_hashed\": \"" + HashPassword(password) + "\"}";

            UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(userPhpScriptUrl, jsonPayload);
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                if (!string.IsNullOrEmpty(webRequest.downloadHandler.text))
                    {
                        Debug.LogError("Response: " + webRequest.downloadHandler.text);
                    }
            }
            else
            {
                CurrentSaveStatus = true;
                StartCoroutine(LoadUsers());
            }
        }
    }
    private bool UsernameExists(string username){
        bool flag = false;
        foreach (User user in users)
        {
            if (user.username == username)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }

    public static string HashPassword(string password)
    {
        // Returns password hashed
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            Debug.Log(builder.ToString());
            return builder.ToString();
        }
    }

    public User ConnectUser(string username, string password){
        return users.Find(u => u.username == username && CheckPassword(password, u.password_hashed));
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

    public List<User> GetUsers(){
        return users;
    }

    public bool CheckPassword(string inputPassword, string hashedPassword) => HashPassword(inputPassword) == hashedPassword;

}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        StartCoroutine(UpdateUser());

    }
    IEnumerator UpdateUser(){
        UserDatabaseManager manager = UserDatabaseManager.Instance;
        User currentUser = manager.GetCurrentUser();
        yield return manager.UpdateUser(currentUser.username, currentUser.getStar_dust() , currentUser.friendsList , currentUser.starIDList);
    }
}

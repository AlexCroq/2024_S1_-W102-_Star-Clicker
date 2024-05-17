using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlasticPipe.PlasticProtocol.Messages;

public class Clicker : MonoBehaviour
{
    public TMP_Text powerText;
    public TMP_Text powerStar;

    private User currentUser;

    private bool clicked = false;
    private bool actionPerformed;

    void Start()
    {
        currentUser = UserDatabaseManager.Instance.GetCurrentUser();
        StartCoroutine(WaitForClick());
    }

    void Update()
    {
        powerText.text = "Total Star Dust: " + currentUser.getStar_dust();
        powerStar.text = "Star power: " + (int)currentUser.getStarPower();
    }

    public void MainButtonPress()
    {
        currentUser.AddScore();
    }

    IEnumerator WaitForClick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if (!clicked && !actionPerformed)
            {
                yield return UserDatabaseManager.Instance.UpdateUser(currentUser,starDust:currentUser.star_dust);
                actionPerformed = true;
            }
            else if (clicked)
            {
                actionPerformed = false;
                clicked = false;
            }
        }
    }
}

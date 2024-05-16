using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlasticPipe.PlasticProtocol.Messages;

public class Clicker : MonoBehaviour
{
    public int power = 0;
    public float powerPerSecond = 1.0f;
    public TMP_Text powerText;

    private bool clicked = false;
    private bool actionPerformed;
    private User user;

    void Start()
    {
        user = UserDatabaseManager.Instance.GetUsers()[1];
        Debug.Log(user.username);
        StartCoroutine(WaitForClick());
    }

    void Update()
    {
        powerText.text = "Total Star Dust: " + power;
    }

    public void MainButtonPress()
    {
        power++;
        clicked = true;
    }

    IEnumerator WaitForClick()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if (!clicked && !actionPerformed)
            {
                yield return UserDatabaseManager.Instance.UpdateUser(user.username,starDust:power);
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

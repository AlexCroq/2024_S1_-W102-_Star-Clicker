using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseScore : MonoBehaviour
{

public Text scoreText;
public float playerScore = 0;

public void AddScore(int amount){
 playerScore+=amount * Time.deltaTime;
 scoreText.text = Mathf.RoundToInt(playerScore).ToString();
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddScore(1);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        AddScore(100); 
    }
    }
}

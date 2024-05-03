using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonMovement : MonoBehaviour
{
    public float speed = 5; 
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform rotateAround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  float h = Input.GetAxis("Horizontal");
       // float v = Input.GetAxis("Vertical");

       // Vector2 pos = transform.position;  // get the position of the object
       // pos.x += h * Time.deltaTime;
       // pos.y += v * Time.deltaTime;
       // transform.position = pos;
       this.transform.RotateAround(rotateAround.position,Vector3.forward,rotationSpeed * Time.deltaTime);

    }
}
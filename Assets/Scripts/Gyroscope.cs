using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GyroControl : MonoBehaviour 
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private bool calibrationMode = true;
    public Button endCalibrationButton;
    private Quaternion rot;
    private Vector2 lastTouchPosition;
    public float rotationSpeed = 1f;
    
    private Quaternion calibratedCoordinates;


    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
        endCalibrationButton.onClick.AddListener(() => SetCalibration(false));
        gyroEnabled = EnableGyro();

    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log(gyro.attitude);
            // cameraContainer.transform.rotation = GetFixedPoint();
            // rot = new Quaternion(0, 0, 1, 0);
            return true;
        }
        return false;
    }

    private void SetCalibration(bool enabled){
        calibratedCoordinates = gyro.attitude;
        calibrationMode = enabled;
        endCalibrationButton.gameObject.SetActive(enabled);
    }

    private Quaternion GetFixedPoint(){
        (float, float) location = GetLocation();
        string hemisphere = GetHemisphere(location);

        Quaternion fixedStar = Quaternion.identity;

        // Set fixed point based on hemisphere
        if (hemisphere == "Northern")
        {
            // Example: fixedStar = Quaternion.Euler(90f, 0f, 0f); // Polaris
        }
        else
        {
            // Example: fixedStar = Quaternion.Euler(-45f, 180f, 0f); // Southern Cross
        }
        return fixedStar;
    }

    private string GetHemisphere((float, float) location){
        return location.Item1 >= 0 ? "Northern" : "Southern";
    }

    private (float, float) GetLocation(){
        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            maxWait--;
            if (maxWait <= 0)
                break;
            System.Threading.Thread.Sleep(1000);
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Failed to initialize location service.");
            return (0f, 0f); 
        }
        return (Input.location.lastData.latitude, Input.location.lastData.longitude);
    }
    private void Update(){
        if (calibrationMode){
            if (Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                switch (touch.phase){
                    case TouchPhase.Began:
                        lastTouchPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                        Vector2 delta = touch.position - lastTouchPosition;
                        float rotationX = -delta.y * rotationSpeed;
                        float rotationY = delta.x * rotationSpeed;

                        // Apply rotation to the camera
                        transform.Rotate(Vector3.right, rotationX, Space.World);
                        transform.Rotate(Vector3.up, rotationY, Space.World);
                        lastTouchPosition = touch.position;
                        break;
                }
            }       
        }
        else{
    if (gyroEnabled)
            {   
                Quaternion gyroscope = gyro.attitude;
                Quaternion currentRotation = CalibratedRotation(calibratedCoordinates, gyroscope);
                cameraContainer.transform.rotation = currentRotation;
            }
        }
    }

    public Quaternion CalibratedRotation(Quaternion coordinates, Quaternion gyroscope)
    {
        return Quaternion.Inverse(coordinates) * gyroscope;
    }
}

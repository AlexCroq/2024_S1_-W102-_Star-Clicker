using UnityEngine;
using NUnit.Framework;

public class GyroControlTests
{
    [Test]
    public void CalibratedRotation_ReturnsExpectedValue()
    {
        GyroControl gyroControl = new GameObject().AddComponent<GyroControl>();

        Vector3 mockGyroAttitude = new  Vector3(1.1f, 1.1f, 1.1f);
        Vector3 mockCoordinates = new  Vector3(1.1f, 1.1f, 1.1f);

        Vector3 result = gyroControl.CalibratedRotation(mockCoordinates,mockGyroAttitude);

        //At calibrated point test
        Vector3 expected = new Vector3(0f, 0f, 0f); 
        Assert.AreEqual(expected, result);


        //No calibration Test
        mockCoordinates = new  Vector3(0f, 0f, 0f);
        mockGyroAttitude = new  Vector3(1.1f, 1.1f, 1.1f);

        result = gyroControl.CalibratedRotation(mockCoordinates,mockGyroAttitude);
        expected = mockGyroAttitude; 
        Assert.AreEqual(expected, result);
    }
}

using UnityEngine;
using NUnit.Framework;

public class GyroControlTests
{
    [Test]
    public void CalibratedRotation_ReturnsExpectedValue()
    {
        GyroControl gyroControl = new GameObject().AddComponent<GyroControl>();

        Quaternion mockGyroAttitude = new  Quaternion(1.1f, 1.1f, 1.1f, 1.1f);
        Quaternion mockCoordinates = new  Quaternion(1.1f, 1.1f, 1.1f, 1.1f);

        Quaternion result = gyroControl.CalibratedRotation(mockCoordinates,mockGyroAttitude);

        //No calibration Test
        Quaternion expected = new Quaternion(0f, 0f, 0f, 0f); 
        Assert.AreEqual(expected, result);


    }
}

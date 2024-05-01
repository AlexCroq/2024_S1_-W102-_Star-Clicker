using NUnit.Framework;
using UnityEngine;

public class StarTests {

    [Test]
    public void TestBasePosition() {

        Star star = new Star(1, 1.0, 0.5, (byte)'G', 0x30, 50, 0.1f, 0.2f);
        Vector3 expectedPosition = new Vector3(Mathf.Cos(1.0f) * Mathf.Cos(0.5f), Mathf.Sin(0.5f), Mathf.Sin(1.0f) * Mathf.Cos(0.5f));
        Vector3 actualPosition = star.GetBasePosition();
        Assert.AreEqual(expectedPosition, actualPosition);
    }

    [Test]
    public void TestColour() {

        Star star = new Star(1, 1.0, 0.5, (byte)'G', 0x30, 50, 0.1f, 0.2f);
        Color expectedColor = new Color(1.0f, 0.93333333f, 0.909803921f); // G1 color
        Color actualColor = star.colour;
        Assert.AreEqual(expectedColor, actualColor);
    }

    [Test]
    public void TestSize() {

        Star star = new Star(1, 1.0, 0.5, (byte)'G', 0x30, 50, 0.1f, 0.2f);
        float expectedSize = 1 - Mathf.InverseLerp(-146, 796, 50);
        float actualSize = star.size;
        Assert.AreEqual(expectedSize, actualSize);
    }
}

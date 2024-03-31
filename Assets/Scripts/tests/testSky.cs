using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections.Generic;

public class SkyTests
{
    [Test]
    public void Sky_StarListNotNull()
    {
        Sky sky = new GameObject().AddComponent<Sky>(); 
        Assert.IsNotNull(sky.starList);
    }

    [Test]
    public void Sky_ShowStars()
    {
        Sky sky = new GameObject().AddComponent<Sky>();
        sky.starList = new List<Star>
        {
            new Star(),
            new Star(),
            new Star()
        };
        sky.ShowStars();

    }
}

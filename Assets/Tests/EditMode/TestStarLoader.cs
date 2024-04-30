using NUnit.Framework;
using UnityEngine;

public class StarDataLoaderTests {

    [Test]
    public void TestLoadData() {
        StarDataLoader dataLoader = new StarDataLoader();

        var stars = dataLoader.LoadData();

        Assert.NotNull(stars);
        Assert.Greater(stars.Count, 0); 
        foreach (var star in stars) {
            // Test individual star properties and check incoming data quality
            Assert.Greater(star.catalog_number, 0); 
            Assert.IsTrue(star.GetRightAscension() >= 0 && star.GetRightAscension() < 360);
            Assert.IsTrue(star.GetDeclination() >= -90 && star.GetDeclination() <= 90); 
            Assert.IsNotNull(star.colour); 
            Assert.GreaterOrEqual(star.size, 0); 
        }

            }
}

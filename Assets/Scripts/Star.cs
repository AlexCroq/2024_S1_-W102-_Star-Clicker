using UnityEngine;

public class Star : MonoBehaviour
{
    // Star properties
    public string starName;
    public float catalogNumber;
    public double rightAscension;
    public double declination;
    public byte spectralType;
    public byte spectralIndex;
    public short magnitude;
    public float raProperMotion;
    public float dexProperMotion;
    
    // Additional flags
    public bool isInInventory;
    public bool isSelected;

    public Star(string name, float catalogNumber, double rightAscension, double declination,
                byte spectralType, byte spectralIndex, short magnitude,
                float raProperMotion, float dexProperMotion)
    {
        this.starName = name;
        this.catalogNumber = catalogNumber;
        this.rightAscension = rightAscension;
        this.declination = declination;
        this.spectralType = spectralType;
        this.spectralIndex = spectralIndex;
        this.magnitude = magnitude;
        this.raProperMotion = raProperMotion;
        this.dexProperMotion = dexProperMotion;
        isInInventory = false;
        isSelected = false;
    }

    public void CollectStar()
    {
        // Add the collected star to the player's inventory (AR feature)
        isInInventory = true;
        Debug.Log("Collected star: " + starName);
    }
    
    public void DisplayStarInfo()
    {
        // Display star information in UI or console
        Debug.Log("Name: " + starName);
        Debug.Log("Catalog Number: " + catalogNumber);
        Debug.Log("Right Ascension: " + rightAscension);
        Debug.Log("Declination: " + declination);
        Debug.Log("Spectral Type: " + spectralType);
        Debug.Log("Spectral Index: " + spectralIndex);
        Debug.Log("Magnitude: " + magnitude);
        Debug.Log("Right Ascension Proper Motion: " + raProperMotion);
        Debug.Log("Declination Proper Motion: " + dexProperMotion);
        Debug.Log("Is in Inventory: " + isInInventory);
        Debug.Log("Is Selected: " + isSelected);
    }

    public void getStarInfo(){
        // Paul gets star info
    }
}

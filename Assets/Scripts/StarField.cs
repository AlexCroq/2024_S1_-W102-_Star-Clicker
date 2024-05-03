using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StarField : MonoBehaviour {
  [Range(0, 100)]
  [SerializeField] private float starSizeMin = 0f;
  [Range(0, 100)]
  [SerializeField] private float starSizeMax = 5f;
  private List<Star> stars;
  private List<GameObject> starObjects;
  private Dictionary<int, GameObject> constellationVisible = new();

  private readonly int starFieldScale = 400;

  void Start() {
    StarDataLoader starLoader = new();
    stars = starLoader.LoadData();
    starObjects = new();
    foreach (Star star in stars) {
      GameObject starObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
      starObject.transform.parent = transform;
      starObject.name = $"HR {star.catalog_number}";
      starObject.transform.localPosition = star.position * starFieldScale;
      starObject.transform.LookAt(transform.position);
      starObject.transform.Rotate(0, 180, 0);
      Material material = starObject.GetComponent<MeshRenderer>().material;
      material.shader = Shader.Find("Unlit/StarShader");
      material.SetFloat("_Size", Mathf.Lerp(starSizeMin, starSizeMax, star.size));
      material.color = star.colour;
      starObjects.Add(starObject);

      EventTrigger eventTrigger = starObject.AddComponent<EventTrigger>();

      // Add PointerClick event to trigger when the star is clicked
      EventTrigger.Entry entry = new EventTrigger.Entry();
      entry.eventID = EventTriggerType.PointerClick;
      entry.callback.AddListener((data) => { OnStarClicked(star); });
      eventTrigger.triggers.Add(entry);
    }
  }

  private void OnStarClicked(Star star){
    if(star.isToBeCollected()){
      //TODO in Sprint 2 logic to collect star once bought (with users database)
      Debug.Log($"Star HR {star.catalog_number} collected");
    }
  }
}
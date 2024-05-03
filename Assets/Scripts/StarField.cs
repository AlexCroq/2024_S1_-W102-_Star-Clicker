using System.Collections.Generic;
using UnityEngine;

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
    }
  }
}
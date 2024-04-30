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
    // Loading the skymap
    StarDataLoader sdl = new();
    stars = sdl.LoadData();
    starObjects = new();
    foreach (Star star in stars) {
      GameObject stargo = GameObject.CreatePrimitive(PrimitiveType.Quad);
      stargo.transform.parent = transform;
      stargo.name = $"HR {star.catalog_number}";
      stargo.transform.localPosition = star.position * starFieldScale;
      stargo.transform.LookAt(transform.position);
      stargo.transform.Rotate(0, 180, 0);
      Material material = stargo.GetComponent<MeshRenderer>().material;
      material.shader = Shader.Find("Unlit/StarShader");
      material.SetFloat("_Size", Mathf.Lerp(starSizeMin, starSizeMax, star.size));
      material.color = star.colour;
      starObjects.Add(stargo);
    }
  }

private void FixedUpdate() {
  // TODO remove when debugging is done
  if (Input.GetKey(KeyCode.Mouse1)) {
    Camera skycamera = GameObject.Find("SkyCamera").GetComponent<Camera>();
    skycamera.transform.RotateAround(skycamera.transform.position, skycamera.transform.right, Input.GetAxis("Mouse Y"));
    skycamera.transform.RotateAround(skycamera.transform.position, Vector3.up, -Input.GetAxis("Mouse X"));
  }
  return;
}
}
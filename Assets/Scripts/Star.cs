using System.Data;
using UnityEngine;

  public class Star {

    public float catalog_number;
    public Vector3 position;
    public Color colour;
    public float size;
    private readonly double right_ascension;
    private readonly double declination;
    private readonly float ra_proper_motion;
    private readonly float dec_proper_motion;
    private bool toBeCollected = false;

    public short magnitude;

    public Star(float catalog_number, double right_ascension, double declination, byte spectral_type,
                byte spectral_index, short magnitude, float ra_proper_motion, float dec_proper_motion) {
      this.catalog_number = catalog_number;
      this.right_ascension = right_ascension;
      this.declination = declination;
      this.ra_proper_motion = ra_proper_motion;
      this.dec_proper_motion = dec_proper_motion;
      this.magnitude = magnitude;

      position = GetBasePosition();
      colour = SetColour(spectral_type, spectral_index);
      size = SetSize(magnitude);
    }
    public Vector3 GetBasePosition() {
      // Place stars on a cylinder.
      double x = System.Math.Cos(right_ascension);
      double y = System.Math.Sin(declination);
      double z = System.Math.Sin(right_ascension);
      // Transforms into a sphere
      double y_cos = System.Math.Cos(declination);
      x *= y_cos;
      z *= y_cos;
      return new((float)x, (float)y, (float)z);
    }

    private Color SetColour(byte spectral_type, byte spectral_index) {
      Color IntColour(int r, int g, int b) {
        return new Color(r / 255f, g / 255f, b / 255f);
      }

      Color[] col = new Color[8];
      col[0] = IntColour(0x5c, 0x7c, 0xff); 
      col[1] = IntColour(0x5d, 0x7e, 0xff);
      col[2] = IntColour(0x79, 0x96, 0xff); 
      col[3] = IntColour(0xb8, 0xc5, 0xff); 
      col[4] = IntColour(0xff, 0xef, 0xed);
      col[5] = IntColour(0xff, 0xde, 0xc0); 
      col[6] = IntColour(0xff, 0xa2, 0x5a);
      col[7] = IntColour(0xff, 0x7d, 0x24);

      int col_idx = -1;
      if (spectral_type == 'O') {
        col_idx = 0;
      } else if (spectral_type == 'B') {
        col_idx = 1;
      } else if (spectral_type == 'A') {
        col_idx = 2;
      } else if (spectral_type == 'F') {
        col_idx = 3;
      } else if (spectral_type == 'G') {
        col_idx = 4;
      } else if (spectral_type == 'K') {
        col_idx = 5;
      } else if (spectral_type == 'M') {
        col_idx = 6;
      }

      // If unknown, make white.
      if (col_idx == -1) {
        return Color.white;
      }

      // Map second part 0 -> 0, 10 -> 100
      float percent = (spectral_index - 0x30) / 10.0f;
      return Color.Lerp(col[col_idx], col[col_idx + 1], percent);
    }

    private float SetSize(short magnitude) {
      return 1 - Mathf.InverseLerp(-146, 796, magnitude);
    }

    public double GetRightAscension() {
        return right_ascension;
    }

    public double GetDeclination() {
        return declination;
    }

    public void setToBeCollected(bool toBeCollected){
      this.toBeCollected = toBeCollected;
    }

    public bool isToBeCollected(){
      return toBeCollected;
    }

    public int getStarClass(){
      if(size<0.05){
        return 1;
      }
      else if(size<0.2 & size>0.05){
        return 2;
      }
      else if(size<0.5 & size>0.2){
        return 3;
      }
      else if(size>0.5){
        return 4;
      }
      else{
        return 0;
      }
    }
    
  }
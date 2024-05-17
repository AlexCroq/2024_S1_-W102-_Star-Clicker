using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StarDataLoader {
  public List<Star> LoadData() {
    List<Star> stars = new();
    // Open the binary file for reading.
    const string filename = "BSC5";
    TextAsset textAsset = Resources.Load(filename) as TextAsset;
    MemoryStream stream = new(textAsset.bytes);
    BinaryReader br = new(stream);
    // Read the header
    int sequence_offset = br.ReadInt32();
    int start_index = br.ReadInt32();
    int num_stars = -br.ReadInt32();
    int star_number_settings = br.ReadInt32();
    int proper_motion_included = br.ReadInt32();
    int num_magnitudes = br.ReadInt32();
    int star_data_size = br.ReadInt32();

    // Read one field at a time.
    for (int i = 0; i < num_stars; i++) {
      float catalog_number = br.ReadSingle();
      double right_ascension = br.ReadDouble();
      // Angular distance from celestial equator.
      double declination = br.ReadDouble();
      byte spectral_type = br.ReadByte();
      byte spectral_index = br.ReadByte();
      short magnitude = br.ReadInt16();
      float ra_proper_motion = br.ReadSingle();
      float dec_proper_motion = br.ReadSingle();
      Star star = new(catalog_number, right_ascension, declination, spectral_type, spectral_index, magnitude, ra_proper_motion, dec_proper_motion);
      stars.Add(star);
    }
    return stars;
  }

}

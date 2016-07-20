using System;
using ProtoRpg;

namespace ProtoRpg {
  /// <summary>
  /// This class loads configurtion from config file.
  /// </summary>
  public class Config {
    // Width of the game window
    public int Width;
    // Height of the game window
    public int Height;

    public int VirtualWidth;
    public int VirtualHeight;

    public static Config Load() {
      XmlManager<Config> xml = new XmlManager<Config>();
      return xml.Load("./config.xml");
    }
  }
}


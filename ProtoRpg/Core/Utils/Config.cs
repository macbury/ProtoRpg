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

    public int TileSize;

    /// <summary>
    /// Number of tiles that can fit horizontaly
    /// </summary>
    /// <value>The columns.</value>
    public int Columns {
      get {
        return (int)Math.Ceiling(Convert.ToDouble(VirtualWidth / TileSize)) + 1;
      }
    }

    /// <summary>
    /// Numbers of tiles that can fit veriticaly
    /// </summary>
    /// <value>The rows.</value>
    public int Rows {
      get {
        return (int)Math.Ceiling(Convert.ToDouble(VirtualHeight / TileSize));
      }
    }

    public static Config Load() {
      return XmlManager<Config>.Load("./config.xml");
    }
  }
}


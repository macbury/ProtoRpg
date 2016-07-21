using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace ProtoRpg {
  /// <summary>
  /// This class defines tileset texture atlas with all tiles
  /// </summary>
  public class Tileset : IDisposable {
    /// <summary>
    /// Tile count horizontal
    /// </summary>
    public int Width;
    /// <summary>
    /// Tile count veriticaly.
    /// </summary>
    public int Height;

    /// <summary>
    /// Name taken from file name without extension
    /// </summary>
    [XmlIgnoreAttribute]
    public string Name;

    public string TextureName;
    /// <summary>
    /// Texture for current tileset
    /// </summary>
    [XmlIgnoreAttribute]
    public Texture2D Texture;

    [XmlIgnoreAttribute]
    public int TileCount {
      get;
      private set;
    }

    public Tileset() {
      
    }

    /// <summary>
    /// Load information about tileset and calculate tileset gids.
    /// </summary>
    public void Load() {
      TileCount = (Width * Height);
    }

    #region IDisposable implementation

    public void Dispose() {
      Texture = null;
    }

    #endregion
  }
}


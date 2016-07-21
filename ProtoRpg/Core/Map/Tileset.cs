using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProtoRpg {

  public class TilesetNotFound : Exception {
    public TilesetNotFound(string tilesetName) : base("Could not find map: " + tilesetName) {}
  }

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
    /// <summary>
    /// The name of the texture.
    /// </summary>
    public string TextureName;

    public List<Tile> Tiles;

    /// <summary>
    /// Texture for current tileset
    /// </summary>
    [XmlIgnoreAttribute]
    public Texture2D Texture;

    [XmlIgnoreAttribute]
    public Point TileSize;

    [XmlIgnoreAttribute]
    public int TileCount {
      get { return Tiles.Count; }
    }

    public Tileset() {
      
    }

    /// <summary>
    /// Load information about tileset and calculate tileset gids.
    /// </summary>
    public void Load(int tileSize, int gidOffset) {
      TileSize  = new Point(tileSize, tileSize);

      if (Tiles == null || Tiles.Count == 0) {
        Tiles = new List<Tile>(); 

        int gid = gidOffset;
        for (int col = 0; col < Width; col++) {
          for (int row = 0; row < Height; row++) {
            var tile       = new Tile() { Id = gid++ };
            var tileOffset = new Point(col, row) * TileSize;
            tile.Rect      = new Rectangle(tileOffset, TileSize);
            Tiles.Add(tile);
          }
        }
      }

    }

    public Tile this[int i] {
      get {
        return Tiles[i];
      }
    }

    #region IDisposable implementation

    public void Dispose() {
      Texture = null;
    }

    #endregion
  }
}


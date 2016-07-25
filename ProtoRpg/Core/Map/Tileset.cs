using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MonoRPG {

  public class TilesetNotFound : Exception {
    public TilesetNotFound(string tilesetName) : base("Could not find map: " + tilesetName) {}
  }

  public class TileNotFound : Exception {
    public TileNotFound(int gid) : base("Could not find tile with gid: " + gid) {}
  }

  /// <summary>
  /// This class defines tileset texture atlas with all tiles
  /// </summary>
  public class Tileset : IDisposable {
    private const int TILES_PER_ID = 1000;

    /// <summary>
    /// Unique id of tileset
    /// </summary>
    public int Id;
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
    public string Name;
    /// <summary>
    /// The name of the texture.
    /// </summary>
    public string TextureName;
    /// <summary>
    /// The tiles in tileset
    /// </summary>
    public List<Tile> Tiles;

    /// <summary>
    /// Texture for current tileset
    /// </summary>
    [XmlIgnoreAttribute]
    public Texture2D Texture;

    [XmlIgnoreAttribute]
    public int StartGidOffset {
      get {
        return Id * TILES_PER_ID;
      }
    }

    public Vector2 TileSize;

    [XmlIgnoreAttribute]
    public int TileCount {
      get { return Tiles.Count; }
    }

    public Tileset() {
      
    }

    /// <summary>
    /// Using information from texture generates new tiles
    /// </summary>
    /// <param name="texture">Texture.</param>
    /// <param name="tileSize">Tile size.</param>
    public void SetupUsingTexture(Texture2D texture, int tileSize) {
      TileSize  = new Vector2(tileSize, tileSize);

      Width = texture.Width / tileSize;
      Height = texture.Height / tileSize;
      Tiles = new List<Tile>();

      int gid = StartGidOffset;
      for (int col = 0; col < Width; col++) {
        for (int row = 0; row < Height; row++) {
          var tile       = new Tile() { Id = gid++ };
          var size       = new Point(tileSize, tileSize);
          var tileOffset = new Point(col, row) * size;
          tile.Rect      = new Rectangle(tileOffset, size);
          tile.Tileset   = this;
          Tiles.Add(tile);
        }
      }
    }

    /// <summary>
    /// Gets the <see cref="ProtoRpg.Tileset"/> with the specified gid.
    /// </summary>
    /// <param name="gid">Gid.</param>
    public Tile this[int gid] {
      get {
        int index = gid - StartGidOffset;
        if (index < 0 || index > Tiles.Count) {
          throw new TileNotFound(gid);
        }
        return Tiles[gid - StartGidOffset];
      }
    }

    #region IDisposable implementation

    public void Dispose() {
      Texture = null;
    }

    #endregion
  }
}


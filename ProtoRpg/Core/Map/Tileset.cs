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
     
    /// <summary>
    /// Width of texture
    /// </summary>
    /// <value>The width in pixels.</value>
    public int WidthInPixels {
      get { return (int)(Width * TileSize.X); }
    }

    /// <summary>
    /// Height of texture
    /// </summary>
    /// <value>The height in pixels.</value>
    public int HeightInPixels {
      get { return (int)(Height * TileSize.Y); }
    }

    [XmlIgnoreAttribute]
    public int TileCount {
      get { return Tiles.Count; }
    }

    public Tileset() {
      
    }

    #region Gid calculation
    /// <summary>
    /// Calculates gid from passed coordinates
    /// </summary>
    /// <returns>The gid.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public int PointToGid(int x, int y) {
      return (x + y * Width) + StartGidOffset;
    }

    /// <summary>
    /// Calculates point on tilemap for passed gid
    /// </summary>
    /// <returns>The to point.</returns>
    /// <param name="gid">Gid.</param>
    public Point GidToPoint(int gid) {
      int localGid = gid - StartGidOffset;
      Point point = new Point();
      point.Y = (int)(localGid / Width);
      point.X = localGid - (point.Y * Width);
      return point;
    }

    /// <summary>
    /// Calculates gid from passed coordinates of tileset
    /// </summary>
    /// <returns>The to gid.</returns>
    /// <param name="point">Point.</param>
    public int PointToGid(Point point) {
      return PointToGid(point.X, point.Y);
    }
    #endregion

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
      for (int row = 0; row < Height; row++) {
        for (int col = 0; col < Width; col++) {
          var tile       = new Tile() { Id = PointToGid(col, row) };
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
        return Tiles[index];
      }
    }

    #region IDisposable implementation

    public void Dispose() {
      Texture = null;
    }

    #endregion
  }
}


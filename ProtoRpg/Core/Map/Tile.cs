using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProtoRpg {
  public class Tile {
    /// <summary>
    /// Unique id of tile in all tilesets
    /// </summary>
    public int Id;
    /// <summary>
    /// The rect with information what part of tileset texture should be rendered
    /// </summary>
    public Rectangle Rect;

    public Tileset Tileset {
      get;
      set;
    }

    public Tile() {
    }

    /// <summary>
    /// Render current tile into spritebatch at tile position
    /// </summary>
    /// <param name="spriteBatch">Sprite batch.</param>
    /// <param name="tilePos">Tile position.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 tilePos) {
      spriteBatch.Draw(
        Tileset.Texture,
        tilePos * Tileset.TileSize,
        Rect,
        Color.White
      );
    }
  }
}


using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace MonoRPG {
  /// <summary>
  /// This class contains list of all maps, and tilesets
  /// </summary>
  public class MapManager : IDisposable {
    private const string TILESET_DIR = "Tileset";
    private const string TILESETS_FILE = "Tilesets.xml";
    private ContentManager contentManager;
    public Tilesets Tilesets;

    const string TAG = "MapManager";

    int TileSize;

    public MapManager(ContentManager contentManager, int TileSize) {
      this.contentManager = new ContentManager(contentManager.ServiceProvider, contentManager.RootDirectory);
      this.TileSize = TileSize;

      this.ReloadTilesets();
    }

    #region Tileset


    /// <summary>
    /// Loads the tileset information from tileset xmls
    /// </summary>
    public void ReloadTilesets() {
      Log.Info(TAG, "Reloading tilesets");
      if (this.Tilesets != null) {
        Tilesets.Dispose();
      }
      this.Tilesets = Tilesets.LoadOrBootstrap(Path.Combine(contentManager.RootDirectory, TILESET_DIR, TILESETS_FILE));
    }

    #endregion

    #region Map managment
    /// <summary>
    /// Load map and its assets into memory
    /// </summary>
    /// <param name="mapName">Map name.</param>
    public void LoadMap(string mapName) {
      Log.Info(TAG, "Loading map: " + mapName);

      UnloadMap();
      //CurrentTileset           = GetTileset("000_Floor");
      //Texture2D tilesetTexture = contentManager.Load<Texture2D>(Path.Combine(TILESET_DIR, CurrentTileset.TextureName));
      //CurrentTileset.Texture   = tilesetTexture;
    }

    /// <summary>
    /// Unloads current map and its resources.
    /// </summary>
    public void UnloadMap() {
      contentManager.Unload();

      //if (CurrentTileset != null) {
      //  CurrentTileset.Texture = null;
      //}
    }
    #endregion

    #region IDisposable implementation

    /// <summary>
    /// Releases all resource used by the <see cref="ProtoRpg.MapManager"/> object.
    /// </summary>
    /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="ProtoRpg.MapManager"/>. The
    /// <see cref="Dispose"/> method leaves the <see cref="ProtoRpg.MapManager"/> in an unusable state. After calling
    /// <see cref="Dispose"/>, you must release all references to the <see cref="ProtoRpg.MapManager"/> so the garbage
    /// collector can reclaim the memory that the <see cref="ProtoRpg.MapManager"/> was occupying.</remarks>
    public void Dispose() {
      Tilesets.Dispose();
      contentManager.Dispose();
    }

    #endregion
  }
}


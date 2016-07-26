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
    AssetsManager assetsManager;

    public Tilesets Tilesets;

    const string TAG = "MapManager";

    int TileSize;

    public MapManager(AssetsManager assetsManager, int TileSize) {
      this.assetsManager = assetsManager;
      this.TileSize = TileSize;

      this.ReloadTilesets();
    }

    #region Tileset

    private String TilesetsPath {
      get {
        return Path.Combine(TILESET_DIR, TILESETS_FILE);
      }
    }

    /// <summary>
    /// Loads the tileset information from tileset xmls
    /// </summary>
    public void ReloadTilesets() {
      Log.Info(TAG, "Reloading tilesets");
      assetsManager.UnloadNow(TilesetsPath);
      this.Tilesets = assetsManager.LoadNow<Tilesets>(TilesetsPath);
    }

    /// <summary>
    /// Saves the tilesets on the disk
    /// </summary>
    public void SaveTilesets() {
      Log.Info(TAG, "Saving tileset as: " + TilesetsPath);
      if (!Directory.Exists(TILESET_DIR))
        Directory.CreateDirectory(TILESET_DIR);
      XmlManager<Tilesets>.Save(TilesetsPath, this.Tilesets);
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
      Tilesets = null;
      assetsManager.UnloadNow(TilesetsPath);
      assetsManager = null;
    }

    #endregion
  }
}


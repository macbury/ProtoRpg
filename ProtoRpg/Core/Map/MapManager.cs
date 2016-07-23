using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace ProtoRpg {
  /// <summary>
  /// This class contains list of all maps, and tilesets
  /// </summary>
  public class MapManager : IDisposable {
    private const string TILESET_DIR = "Tileset"; 
    private Dictionary<string, Tileset> tilesets;
    private List<Tile> tiles;
    private ContentManager contentManager;
    public Tileset CurrentTileset { get; private set; }

    const string TAG = "MapManager";

    int TileSize;

    public MapManager(ContentManager contentManager, int TileSize) {
      this.contentManager = new ContentManager(contentManager.ServiceProvider, contentManager.RootDirectory);
      this.TileSize = TileSize;
      this.tiles    = new List<Tile>();
      tilesets = new Dictionary<string, Tileset>();

      this.loadTilesetInformation();
    }

    #region Tileset

    /// <summary>
    /// Find tile by its id
    /// </summary>
    /// <returns>The tile.</returns>
    /// <param name="gid">Gid.</param>
    public Tile GetTile(int gid) {
      if (gid < 0 || gid > this.tiles.Count) {
        throw new TileNotFound(gid);
      }
      return this.tiles[gid];
    }

    /// <summary>
    /// Finds tileset by name
    /// </summary>
    /// <returns>The tileset.</returns>
    /// <param name="name">name of tileset.</param>
    public Tileset GetTileset(string name) {
      if (tilesets.ContainsKey(name)) {
        return tilesets[name];
      } else {
        throw new TilesetNotFound(name);
      }
    }

    /// <summary>
    /// Loads the tileset information from tileset xmls
    /// </summary>
    private void loadTilesetInformation() {
      tilesets.Clear();
      tiles.Clear();

      var tilesetsXmlPaths = Directory.GetFiles(Path.Combine(contentManager.RootDirectory, TILESET_DIR), "*.xml");
      int gidOffset = 0;
      foreach (var tilesetXmlPath in tilesetsXmlPaths) {
        Log.Info(TAG, "Found: " + tilesetXmlPath);
        string tilesetName = Path.GetFileNameWithoutExtension(tilesetXmlPath);
        Tileset tileset    = XmlManager<Tileset>.Load(tilesetXmlPath);
        tileset.Name = tilesetName;
        tileset.Load(TileSize, gidOffset);
        gidOffset += tileset.TileCount;
        tilesets.Add(tilesetName, tileset);
        tiles.AddRange(tileset.Tiles);
      }
    }

    /// <summary>
    /// Number of tilesets in database
    /// </summary>
    /// <value>The tileset count.</value>
    public int TilesetCount {
      get {
        return tilesets.Count;
      }
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
      CurrentTileset           = GetTileset("000_Floor");
      Texture2D tilesetTexture = contentManager.Load<Texture2D>(Path.Combine(TILESET_DIR, CurrentTileset.TextureName));
      CurrentTileset.Texture   = tilesetTexture;
    }

    /// <summary>
    /// Unloads current map and its resources.
    /// </summary>
    public void UnloadMap() {
      contentManager.Unload();

      if (CurrentTileset != null) {
        CurrentTileset.Texture = null;
      }
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
      foreach (var tileset in tilesets) {
        tileset.Value.Dispose();
      }
      tilesets.Clear();
      contentManager.Dispose();
    }

    #endregion
  }
}


using System;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace ProtoRpg {
  /// <summary>
  /// This class contains list of all maps, and tilesets
  /// </summary>
  public class MapManager : IDisposable {
    private const string TILESET_DIR = "Tileset"; 
    private string RootDirectory;
    private Dictionary<string, Tileset> tilesets;

    const string TAG = "MapManager";

    public MapManager(string RootDirectory) {
      this.RootDirectory = Path.GetFullPath(RootDirectory);
      tilesets = new Dictionary<string, Tileset>();

      this.loadTilesetInformation();
    }

    #region Tileset

    /// <summary>
    /// Finds tileset by name
    /// </summary>
    /// <returns>The tileset.</returns>
    /// <param name="name">name of tileset.</param>
    public Tileset GetTileset(string name) {
      return tilesets[name];
    }

    /// <summary>
    /// Loads the tileset information from tileset xmls
    /// </summary>
    private void loadTilesetInformation() {
      tilesets.Clear();

      var tilesetsXmlPaths = Directory.GetFiles(Path.Combine(RootDirectory, TILESET_DIR), "*.xml");
      foreach (var tilesetXmlPath in tilesetsXmlPaths) {
        Log.Info(TAG, "Found: " + tilesetXmlPath);
        string tilesetName = Path.GetFileNameWithoutExtension(tilesetXmlPath);
        Tileset tileset    = XmlManager<Tileset>.Load(tilesetXmlPath);
        tileset.Name = tilesetName;
        tilesets.Add(tilesetName, tileset);
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
    }

    #endregion
  }
}


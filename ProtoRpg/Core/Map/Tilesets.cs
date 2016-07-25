using System;
using System.Collections.Generic;
using System.IO;

namespace MonoRPG {
  /// <summary>
  /// Holds information about all tilesets in system.
  /// </summary>
  public class Tilesets : List<Tileset>, IDisposable {
    public const int DEFAULT_NUMBER_OF_TILESETS = 10;

    /// <summary>
    /// Loads tileset or bootstrap a new one
    /// </summary>
    /// <returns>Tileset loaded or new.</returns>
    /// <param name="path">Path to tileset</param>
    public static Tilesets LoadOrBootstrap(string path) {
      if (File.Exists(path)) {
        return XmlManager<Tilesets>.Load(path);
      } else {
        return Tilesets.Bootstrap();
      }
    }

    /// <summary>
    /// Bootstrap tilesets with DEFAULT_NUMBER_OF_TILESETS as null.
    /// </summary>
    public static Tilesets Bootstrap() {
      Tilesets tilesets = new Tilesets();
      for (int i = 0; i < DEFAULT_NUMBER_OF_TILESETS; i++) {
        var tileset = new Tileset() { Id = i, Name = "", Width = 1, Height = 1 };
        tilesets.Add(tileset);
      }
      return tilesets;
    }

    public Tilesets() : base(DEFAULT_NUMBER_OF_TILESETS) {
    }

    /// <summary>
    /// Save tilesets information at the specified path.
    /// </summary>
    /// <param name="path">Path.</param>
    public void Save(string path) {
      XmlManager<Tilesets>.Save(path, this);
    }

    #region IDisposable implementation

    public void Dispose() {
      foreach(var tileset in this) {
        if (tileset != null)
          tileset.Dispose();
      }
      Clear();
    }

    #endregion
  }
}


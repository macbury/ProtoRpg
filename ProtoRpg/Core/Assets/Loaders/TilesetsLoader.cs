using System;

namespace MonoRPG {

  /// <summary>
  /// Load only tilesets xml
  /// </summary>
  public class TilesetsLoader : AssetLoader<Tilesets> {
    public TilesetsLoader() {
    }

    #region implemented abstract members of AssetLoader

    public override Tilesets Load(AssetsManager assetManager, string fileName) {
      return Tilesets.LoadOrBootstrap(fileName);
    }

    public override void Dispose() {
      
    }

    #endregion
  }
}


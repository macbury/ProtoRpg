using System;

namespace MonoRPG {
  /// <summary>
  /// Abstract base interface for asset loaders.
  /// </summary>
  public abstract class AssetLoader<T> : IDisposable {
    abstract public T Load(AssetsManager assetManager, String fileName);

    #region IDisposable implementation

    abstract public void Dispose();

    #endregion
  }
}


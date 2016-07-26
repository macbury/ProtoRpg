using System;
using System.Collections.Generic;

namespace MonoRPG {
  /// <summary>
  /// Abstract base interface for asset loaders.
  /// </summary>
  public abstract class AssetLoader<T> : IDisposable {
    /// <summary>
    /// Load the main asset from file.
    /// </summary>
    /// <param name="assetManager">Asset manager.</param>
    /// <param name="fileName">File name.</param>
    abstract public T Load(AssetsManager assetManager, String fileName);

    #region IDisposable implementation

    abstract public void Dispose();

    #endregion
  }
}


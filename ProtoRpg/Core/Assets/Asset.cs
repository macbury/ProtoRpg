using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace MonoRPG {

  /// <summary>
  /// Managed asset by asset manager
  /// </summary>
  public class Asset : IDisposable {
    /// <summary>
    /// How many times asset was added to load. If this value equals 0 then it will unload asset
    /// </summary>
    public int RefCount;
    /// <summary>
    /// Path to asset
    /// </summary>
    public string Path;
    /// <summary>
    /// The type of the content.
    /// </summary>
    public Type ContentType;

    /// <summary>
    /// Loaded content
    /// </summary>
    public object Content;

    public List<Asset> Dependencies;

    public bool Loaded {
      get { return Content != null; }
    }

    public Asset() {
      Dependencies = new List<Asset>();
    }

    /// <summary>
    /// Decrease RefCount, and if it hits 0 then unload asset
    /// </summary>
    public void Unload() {
      if (RefCount > 0) {
        RefCount--;
      }
    }

    #region IDisposable implementation

    public void Dispose() {
      RefCount = 0;
      if (Content is IDisposable)
        ((IDisposable)Content).Dispose();

      foreach (var dependency in Dependencies) {
        dependency.Unload();
      }

      Dependencies.Clear();
      Content = null;
    }

    #endregion
  }
}


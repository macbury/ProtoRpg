using System;

namespace ProtoRpg {
  /// <summary>
  /// This class defines tileset texture atlas with all tiles
  /// </summary>
  public class Tileset : IDisposable {

    public string Name;

    public Tileset() {
      
    }

    #region IDisposable implementation

    public void Dispose() {
      throw new NotImplementedException();
    }

    #endregion
  }
}


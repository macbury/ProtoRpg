using System;
using Microsoft.Xna.Framework.Graphics;

namespace ProtoRpg {
  public class Map : IDisposable {
    /// <summary>
    /// This layer contains all floor stuff like gras, dirt, sand or water. This is always drawed first
    /// </summary>
    /// <value>The floor.</value>
    public Layer<short> Floor { get; private set; }
    /// <summary>
    /// All decoration drawn after floor. This can be sorted by Z
    /// </summary>
    /// <value>The decoration.</value>
    public Layer<short> Decoration { get; private set; }

    public Map() {
      
    }

    public void Draw(Camera camera, SpriteBatch spriteBatch) {
      
    }

    #region IDisposable implementation

    public void Dispose() {
      throw new NotImplementedException();
    }

    #endregion
  }
}


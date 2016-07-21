using System;
using Microsoft.Xna.Framework.Graphics;

namespace ProtoRpg {
  public class Map : IDisposable {
    public Layer<byte> Background { get; private set; }
    public Layer<byte> Obstalces { get; private set; }

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


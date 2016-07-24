using System;
using System.Threading.Tasks;

namespace MonoRPG {
  public class Layer<T> : IDisposable {
    private T[,] tiles;

    public Layer(int Width, int Height) {
      tiles = new T[Width, Height];
    }

    #region IDisposable implementation

    public void Dispose() {
      tiles = null;
    }

    #endregion
  }
}


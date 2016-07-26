using System;
using NUnit.Framework;
using MonoRPG;

namespace Test {

  internal class DummyAssetObject : IDisposable {
    public bool Disposed;

    #region IDisposable implementation
    public void Dispose() {
      this.Disposed = true;
    }
    #endregion
    
  }

  [TestFixture()]
  public class TestAsset {

    [Test()]
    public void ItShouldDisposeObject() {
      DummyAssetObject dummyAssetObject = new DummyAssetObject();
      Asset asset = new Asset() { RefCount = 1, Content = dummyAssetObject };

      asset.Dispose();

      Assert.IsTrue(dummyAssetObject.Disposed);
      Assert.IsNull(asset.Content);
    }
  }
}


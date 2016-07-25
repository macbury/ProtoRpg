using System;
using NUnit.Framework;
using MonoRPG;


namespace Test {
  [TestFixture()]
  public class TestTileset {

    [Test()]
    public void ItShouldGenerateProperGidOffsetByTilesetId() {
      Tileset firstTileset = new Tileset() { Id = 0 };
      Assert.AreEqual(firstTileset.StartGidOffset, 0);

      Tileset secondTileset = new Tileset() { Id = 1 };
      Assert.AreEqual(secondTileset.StartGidOffset, 1000);
    }
  }
}


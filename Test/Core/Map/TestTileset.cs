using System;
using NUnit.Framework;
using MonoRPG;
using Microsoft.Xna.Framework;


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

    [Test()]
    public void ItShouldCalculateProperGidUsingCoords() {
      Tileset tileset = new Tileset() { Id = 1, Width = 8, Height = 20 };

      Assert.AreEqual(1106, tileset.PointToGid(2, 13));
      Assert.AreEqual(1066, tileset.PointToGid(2, 8));
      Assert.AreEqual(1167, tileset.PointToGid(7, 20));
      Assert.AreEqual(1000, tileset.PointToGid(new Point(0,0)));
    }

    [Test()]
    public void ItShouldCalculatePointToGid() {
      Tileset tileset = new Tileset() { Id = 1, Width = 8, Height = 20 };

      Assert.AreEqual(new Point(0,0) , tileset.GidToPoint(1000));
      Assert.AreEqual(new Point(2,13) , tileset.GidToPoint(1106));
      Assert.AreEqual(new Point(7,20) , tileset.GidToPoint(1167));
      Assert.AreEqual(new Point(2,8) , tileset.GidToPoint(1066));
    }

    [Test()]
    public void ItShouldConvertBetweenGidAndPoint() {
      Tileset tileset = new Tileset() { Id = 4, Width = 32, Height = 32 };
      Point tilesetPos = tileset.GidToPoint(4500);
      Assert.AreEqual(4500 , tileset.PointToGid(tilesetPos));
    }
  }
}


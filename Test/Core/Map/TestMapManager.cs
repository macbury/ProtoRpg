using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using NUnit.Framework;
using ProtoRpg;

namespace Test {
  [TestFixture()]
  public class TestMapManager {
    MapManager mapManager;

    [SetUp()]
    public void SetUp() {
      mapManager = new MapManager(new ContentManager(new GameServiceContainer()));
    }

    [Test()]
    public void ItShouldFindAllTilesetsInDirectory() {
      Assert.AreEqual(mapManager.TilesetCount, 2);
    }

    [Test()]
    public void ItShouldAllowMeToFindTileset() {
      Tileset tileset = mapManager.GetTileset("000_forest");
      Assert.IsNotNull(tileset);
      Assert.AreEqual(tileset.Name, "000_forest");

      tileset = mapManager.GetTileset("001_town");
      Assert.AreEqual(tileset.Name, "001_town");
      Assert.IsNotNull(tileset);
    }
  }
}


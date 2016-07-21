using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using NUnit.Framework;
using ProtoRpg;
using Microsoft.Xna.Framework;

namespace Test {
  [TestFixture()]
  public class TestMapManager {
    MapManager mapManager;

    [SetUp()]
    public void SetUp() {
      var contentManager = new ContentManager(new GameServiceContainer());
      contentManager.RootDirectory = "./Fixtures";
      mapManager = new MapManager(contentManager);
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


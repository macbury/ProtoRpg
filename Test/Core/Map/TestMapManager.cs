using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using NUnit.Framework;
using MonoRPG;
using Microsoft.Xna.Framework;

namespace Test {
  [TestFixture()]
  public class TestMapManager {
    MapManager mapManager;

    [SetUp()]
    public void SetUp() {
      var contentManager = new ContentManager(new GameServiceContainer());
      contentManager.RootDirectory = "./Fixtures";
      mapManager = new MapManager(contentManager, 16);
    }

    [Test()]
    public void ItShouldFindAllTilesetsInDirectory() {
      Assert.AreEqual(mapManager.Tilesets.Count, 10);
    }

    [Test()]
    public void ItShouldAllowMeToFindTileset() {
      Tileset tileset = mapManager.Tilesets[0];
      Assert.IsNotNull(tileset);
      Assert.AreEqual("Forest", tileset.Name);

      tileset = mapManager.Tilesets[1];
      Assert.AreEqual("Town", tileset.Name);
      Assert.IsNotNull(tileset);
    }
  }
}


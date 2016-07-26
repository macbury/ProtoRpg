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

    RPGGame game;

    [SetUp()]
    public void SetUp() {
      this.game = new RPGGame(Config.Load());
      this.game.RunOneFrame();
      mapManager = this.game.MapManager;
    }

    [TearDown()]
    public void TearDown() {
      this.game.Dispose();
    }

    [Test()]
    public void ItShouldFindAllTilesetsInDirectory() {
      Assert.AreEqual(mapManager.Tilesets.Count, 10);
    }

    [Test()]
    public void ItShouldAllowMeToFindTileset() {
      Tileset tileset = mapManager.Tilesets[0];
      Assert.IsNotNull(tileset);
      Assert.AreEqual(0, tileset.Id);

      tileset = mapManager.Tilesets[1];
      Assert.AreEqual(1, tileset.Id);
      Assert.IsNotNull(tileset);
    }
  }
}


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
      var tilesetA = mapManager.Tilesets[0];
      Assert.IsNotNull(tilesetA);
      Assert.AreEqual(0, tilesetA.Id);

      var tilesetB = mapManager.Tilesets[1];
      Assert.AreEqual(1, tilesetB.Id);
      Assert.IsNotNull(tilesetB);
    }
  }
}


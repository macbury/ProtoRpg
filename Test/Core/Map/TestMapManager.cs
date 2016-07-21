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
      mapManager = new MapManager(contentManager, 16);
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

    [Test()]
    public void ItShouldGenerateTilesOnStart() {
      Tileset tileset = mapManager.GetTileset("000_forest");
      Assert.AreEqual(480, tileset.TileCount);

      Tile firstTile = tileset[0];

      Assert.IsNotNull(firstTile);
      Assert.AreEqual(0, firstTile.Id);

      Tile secondTile = tileset[1];
      Assert.AreEqual(1, secondTile.Id);

      Tileset secondTileset = mapManager.GetTileset("001_town");

      firstTile = secondTileset[0];
      Assert.IsNotNull(firstTile);
      Assert.AreEqual(480, firstTile.Id);
    }

    [Test()]
    [ExpectedException(typeof(TilesetNotFound))]
    public void ItShouldDropMoreNiceExceptionIfPassedNonExistingTilesetName() {
      mapManager.GetTileset("234324_thisnotexiststt");
    }
  }
}


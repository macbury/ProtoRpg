using System;
using NUnit.Framework;
using MonoRPG;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test {
  [TestFixture()]
  public class TestTilesets {
    const int DEFAULT_TILE_SIZE   = 16;
    const string DEFAULT_TILESETS = "Fixtures/Tilesets/Tilesets.xml";
    RPGGame game;

    [SetUp()]
    public void SetUp() {
      this.game = new RPGGame(Config.Load());
      this.game.RunOneFrame();
    }

    [TearDown()]
    public void TearDown() {
      this.game.Dispose();
    }

    [Test()]
    public void TestBootstrap() {
      Tilesets tilesets = Tilesets.Bootstrap();
      Assert.IsNotNull(tilesets);
      Assert.AreEqual(tilesets.Count, Tilesets.DEFAULT_NUMBER_OF_TILESETS);
      Assert.IsNotNull(tilesets[0]);
    }

    [Test()]
    public void TestLoadOrBootstrap() {
      const string NON_EXISTING_TILESET_PATH = "Fixtures/test_load_or_bootstrap_tilesets.xml";

      if (File.Exists(NON_EXISTING_TILESET_PATH))
        File.Delete(NON_EXISTING_TILESET_PATH);
      
      Tilesets bootstrapedTilesets = Tilesets.LoadOrBootstrap(NON_EXISTING_TILESET_PATH);
      Assert.IsNotNull(bootstrapedTilesets);
      Assert.AreEqual(bootstrapedTilesets.Count, Tilesets.DEFAULT_NUMBER_OF_TILESETS);
      Assert.IsNotNull(bootstrapedTilesets[0]);

      Tilesets existingTilesets = Tilesets.LoadOrBootstrap(DEFAULT_TILESETS);
      Assert.IsNotNull(existingTilesets);
      Assert.IsNotNull(existingTilesets[0]);
    }

    [Test()]
    public void ItShouldCreateFileOnSave() {
      const string TEST_TILESETS_SAVE = "/tmp/test_tilesets.xml";
      if (File.Exists(TEST_TILESETS_SAVE))
        File.Delete(TEST_TILESETS_SAVE);
      Tilesets tilesets = Tilesets.Bootstrap();
      tilesets.Save(TEST_TILESETS_SAVE);
      Assert.IsTrue(File.Exists(TEST_TILESETS_SAVE));
    }

    [Test()]
    public void ItShouldGenerateTilesetFromGraphicsFile() {
      Tilesets tilesets = Tilesets.Bootstrap();
      Tileset floorTileset = tilesets[0];
      using (var stream = File.OpenRead("Fixtures/Tileset/Floor.png")) {
        using(var texture = Texture2D.FromStream(game.GraphicsDevice, stream)) {
          floorTileset.SetupUsingTexture(texture, DEFAULT_TILE_SIZE);
          floorTileset.Name = "Floor";
          floorTileset.TextureName = "Floor.png";
        }

        Assert.AreEqual(819, tilesets[0].TileCount);

        Tile firstTile = floorTileset[0];

        Assert.IsNotNull(firstTile);
        Assert.AreEqual(0, firstTile.Id);
        Assert.AreEqual(new Rectangle(0,0 ,DEFAULT_TILE_SIZE,DEFAULT_TILE_SIZE), firstTile.Rect);

        Tile secondTile = floorTileset[1];
        Assert.AreEqual(1, secondTile.Id);
        Assert.AreEqual(new Rectangle(0,DEFAULT_TILE_SIZE ,DEFAULT_TILE_SIZE,DEFAULT_TILE_SIZE), secondTile.Rect);
      }
    }

    /*[Test()]
    public void ItShouldGenerateTilesOnStart() {
      Tilesets tilesets = Tilesets.LoadOrBootstrap(DEFAULT_TILESETS);
      Tileset tileset   = tilesets[0];
      Assert.AreEqual(480, tileset.TileCount);

      Tile firstTile = tileset[0];

      Assert.IsNotNull(firstTile);
      Assert.AreEqual(0, firstTile.Id);
      Assert.AreEqual(new Rectangle(0,0 ,16,16), firstTile.Rect);

      Tile secondTile = tileset[1];
      Assert.AreEqual(1, secondTile.Id);
      Assert.AreEqual(new Rectangle(0,16 ,16,16), secondTile.Rect);

      Tileset secondTileset = tilesets[1];

      firstTile = secondTileset[500];
      Assert.IsNotNull(firstTile);
      Assert.AreEqual(500, firstTile.Id);
    }*/

    [Test()]
    public void ItShouldLoadTilesetsFromOneXml() {
      Tilesets tilesets = Tilesets.Bootstrap();

      XmlManager<Tilesets>.Save("/tmp/test.xml", tilesets);
    }
  }
}


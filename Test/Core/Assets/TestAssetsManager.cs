using System;
using NUnit;
using NUnit.Framework;
using MonoRPG;
using Microsoft.Xna.Framework.Graphics;

namespace Test {
  [TestFixture()]
  public class TestAssetsManager {
    const string EXAMPLE_TILESET_TEXTURE = "Fixtures/Tileset/Floor.png";
    const string EXAMPLE_TILESET2_TEXTURE = "Fixtures/Tileset/Map0.png";
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
    public void ItShouldAddPendingAssetLoadingRequest() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      Asset textureAsset = assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      Assert.IsFalse(textureAsset.Loaded);
      Assert.AreEqual(1, textureAsset.RefCount);
      Assert.AreEqual(EXAMPLE_TILESET_TEXTURE, textureAsset.Path);
      Assert.AreEqual(typeof(Texture2D), textureAsset.ContentType);

      assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      Assert.AreEqual(2, textureAsset.RefCount);

      assetsManager.UpdateAll();
      Assert.IsTrue(textureAsset.Loaded);

      assetsManager.Dispose();

      Assert.AreEqual(0, textureAsset.RefCount);
    }

    [Test()]
    [ExpectedException( typeof( AssetNotFound ) )]
    public void ItShouldThrowExceptionIfAssetIsNotLoaded() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      Assert.IsNull(assetsManager.Get<Texture2D>(EXAMPLE_TILESET_TEXTURE));
    }

    [Test()]
    [ExpectedException( typeof( AssetNotLoaded ) )]
    public void ItShouldThrowExceptionIfAssetIsAddedToLoadButNotLoaded() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      Assert.IsNull(assetsManager.Get<Texture2D>(EXAMPLE_TILESET_TEXTURE));
    }

    [Test()]
    public void ItShouldReturnLoadedTexture() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      assetsManager.UpdateAll();
      Assert.IsNotNull(assetsManager.Get<Texture2D>(EXAMPLE_TILESET_TEXTURE));
    }

    [Test()]
    [ExpectedException( typeof( AssetNotFound ) )]
    public void ItShouldLoadAndUnloadAssets() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      Asset asset = assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      assetsManager.UpdateAll();
      assetsManager.Unload(EXAMPLE_TILESET_TEXTURE);
      assetsManager.UpdateAll();

      Assert.IsFalse(asset.Loaded);
      Assert.AreEqual(0, asset.RefCount);

      Assert.IsNotNull(assetsManager.Get<Texture2D>(EXAMPLE_TILESET_TEXTURE));
    }

    [Test()]
    public void ItShouldLoadAndUnloadOtherAsset() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      Asset assetA = assetsManager.Load<Texture2D>(EXAMPLE_TILESET_TEXTURE);
      assetsManager.UpdateAll();
      Asset assetB = assetsManager.Load<Texture2D>(EXAMPLE_TILESET2_TEXTURE);
      assetsManager.UpdateAll();

      Assert.IsTrue(assetA.Loaded);
      Assert.IsTrue(assetB.Loaded);

      assetsManager.Unload(EXAMPLE_TILESET_TEXTURE);
      assetsManager.UpdateAll();

      Assert.IsFalse(assetA.Loaded);
      Assert.IsTrue(assetB.Loaded);
    }

    [Test()]
    public void ItShouldLoadNowTileset() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      var tilesetsFirst = assetsManager.LoadNow<Tilesets>(DEFAULT_TILESETS);
      var asset = assetsManager.GetAsset(DEFAULT_TILESETS);
      Assert.IsNotNull(tilesetsFirst);
      Assert.AreEqual(1, asset.RefCount);
      assetsManager.UnloadNow(DEFAULT_TILESETS);
      Assert.AreEqual(0, asset.RefCount);
      var tilesetsSecond = assetsManager.LoadNow<Tilesets>(DEFAULT_TILESETS);

      Assert.AreNotSame(tilesetsFirst, tilesetsSecond);
    }

    [Test()]
    public void ItShouldUpdateRefCountForMultipleLoadNow() {
      AssetsManager assetsManager   = new AssetsManager(game.GraphicsDevice);
      assetsManager.LoadNow<Tilesets>(DEFAULT_TILESETS);
      assetsManager.LoadNow<Tilesets>(DEFAULT_TILESETS);
      assetsManager.LoadNow<Tilesets>(DEFAULT_TILESETS);
      var asset = assetsManager.GetAsset(DEFAULT_TILESETS);
      Assert.AreEqual(3, asset.RefCount);
      Assert.IsTrue(asset.Loaded);

      assetsManager.Unload(DEFAULT_TILESETS);
      Assert.AreEqual(2, asset.RefCount);

      assetsManager.Unload(DEFAULT_TILESETS);
      Assert.AreEqual(1, asset.RefCount);

      assetsManager.Unload(DEFAULT_TILESETS);
      Assert.AreEqual(0, asset.RefCount);
      Assert.IsTrue(asset.Loaded);

      assetsManager.UpdateAll();

      Assert.IsFalse(asset.Loaded);
    }
  }
}


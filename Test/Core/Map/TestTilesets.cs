using System;
using NUnit.Framework;
using MonoRPG;
using System.IO;

namespace Test {
  [TestFixture()]
  public class TestTilesets {

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

      Tilesets existingTilesets = Tilesets.LoadOrBootstrap("Fixtures/Tilesets.xml");
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

    /*[Test()]
    public void ItShouldLoadTilesetsFromOneXml() {
      Tilesets tilesets = Tilesets.Bootstrap();

      XmlManager<Tilesets>.Save("/tmp/test.xml", tilesets);
    }*/
  }
}


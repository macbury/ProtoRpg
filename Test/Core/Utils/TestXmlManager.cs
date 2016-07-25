using System;
using NUnit.Framework;
using MonoRPG;

namespace Test {

  public class DummyXmlObject {
    public int ValueToLoad;
  }

  [TestFixture()]
  public class TestXmlManager {

    [Test()]
    public void ItShouldLoadDummyObjectFromXml() {
      DummyXmlObject dummy = XmlManager<DummyXmlObject>.Load("Fixtures/dummy_xml_object.xml");
      Assert.IsNotNull(dummy);
      Assert.AreEqual(dummy.ValueToLoad, 10);
    }

  }
}


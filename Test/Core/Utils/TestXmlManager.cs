using System;
using NUnit.Framework;
using ProtoRpg;

namespace Test {

  public class DummyXmlObject {
    public int ValueToLoad;
  }

  [TestFixture()]
  public class TestXmlManager {

    [Test()]
    public void ItShouldLoadDummyObjectFromXml() {
      XmlManager<DummyXmlObject> xml = new XmlManager<DummyXmlObject>();
      DummyXmlObject dummy = xml.Load("Fixtures/dummy_xml_object.xml");
      Assert.IsNotNull(dummy);
      Assert.AreEqual(dummy.ValueToLoad, 10);
    }
  }
}


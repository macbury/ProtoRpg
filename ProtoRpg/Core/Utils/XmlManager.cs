using System;
using System.IO;
using System.Xml.Serialization;

namespace ProtoRpg {
  // This class helps load xml file and deserialize it to object
  public class XmlManager<T> {

    // Load object from file
    public static T Load(string path) {
      T instance;
      using(TextReader textReader = new StreamReader(path)) {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        instance = (T)xml.Deserialize(textReader);
      }

      return instance;
    }
  }
}


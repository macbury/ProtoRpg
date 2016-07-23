using System;
using System.IO;
using System.Xml.Serialization;

namespace ProtoRpg {
  /// <summary>
  /// This class helps load xml file and deserialize it to object
  /// </summary>
  public class XmlManager<T> {

    /// <summary>
    /// Load object from the specified path.
    /// </summary>
    /// <param name="path">Path.</param>
    public static T Load(string path) {
      T instance;
      using(TextReader textReader = new StreamReader(path)) {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        instance = (T)xml.Deserialize(textReader);
      }

      return instance;
    }

    /// <summary>
    /// Save instance at the specified path
    /// </summary>
    /// <param name="path">Path.</param>
    /// <param name="instance">Instance.</param>
    public static void Save(string path, T instance) {
      using(TextWriter textWriter = new StreamWriter(path)) {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        xml.Serialize(textWriter, instance);
      }
    }
  }
}


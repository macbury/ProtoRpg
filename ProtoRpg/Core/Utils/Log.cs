using System;
using System.Diagnostics;

namespace ProtoRpg {
  public class Log {

    public static void Info(string tag, string message) {
      Debug.WriteLine("[" + tag + "] " + message);
    }
  }
}


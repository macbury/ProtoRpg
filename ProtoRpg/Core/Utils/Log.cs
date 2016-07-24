using System;
using System.Diagnostics;

namespace MonoRPG {
  public class Log {

    public static void Info(string tag, string message) {
      Debug.WriteLine("[" + tag + "] " + message);
    }
  }
}


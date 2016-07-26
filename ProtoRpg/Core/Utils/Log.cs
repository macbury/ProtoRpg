using System;
using System.Diagnostics;

namespace MonoRPG {
  public class Log {

    public static void Info(string tag, string message) {
      Console.WriteLine("[" + tag + "] " + message);
    }
  }
}


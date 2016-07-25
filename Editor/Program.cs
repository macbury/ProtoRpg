using System;
using Gtk;
using Microsoft.Xna.Framework;
using MonoRPG;


namespace Editor {
  class MainClass {
    public static void Main(string[] args) {
      Config config = Config.Load();
      RPGGame core     = new RPGGame(config);

      Application.Init();
      MainWindow win = new MainWindow(core);

      win.Show();
      Application.Run();
    }
  }
}

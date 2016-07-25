using System;
using Gtk;
using Microsoft.Xna.Framework;
using MonoRPG;


namespace Editor {
  class MainClass {
    public static void Main(string[] args) {
      Config config = Config.Load();
      RPGGame core  = new RPGGame(config);
      core.RunOneFrame();
      Application.Init();
      //TODO ask for config file chere
      MainWindow win = new MainWindow(core);
      win.Show();
      Application.Run();
    }
  }
}

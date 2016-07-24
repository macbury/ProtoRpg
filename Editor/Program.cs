using System;
using Gtk;
using Microsoft.Xna.Framework;
namespace Editor {
  class MainClass {
    public static void Main(string[] args) {
      Application.Init();
      MainWindow win = new MainWindow();

      win.Show();
      Application.Run();
    }
  }
}

using System;
using Gtk;

namespace Editor {
  class MainClass {
    public static void Main(string[] args) {
      Application.Init();
      MainWindow win = new MainWindow();
      win.DefaultWidth = 1280;
      win.DefaultHeight = 760;
      win.Show();
      Application.Run();
    }
  }
}

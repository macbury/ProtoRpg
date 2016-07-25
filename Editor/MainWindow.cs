using System;
using Gtk;
using Editor;
using MonoRPG;

public partial class MainWindow: Gtk.Window {
  RPGGame game;

  public MainWindow(RPGGame core) : base(Gtk.WindowType.Toplevel) {
    Build();
    this.game = core;

  }

  protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
    game.Dispose();
    Application.Quit();
    a.RetVal = true;
  }

  protected void OnConfigureTilesetsAction(object sender, EventArgs e) {
    TilesetsManagerDialog dialog = new TilesetsManagerDialog(game.MapManager);
    dialog.ShowAll();
  }

}

using System;
using Gtk;
using Editor;

public partial class MainWindow: Gtk.Window {
  public MainWindow() : base(Gtk.WindowType.Toplevel) {
    Build();
  }

  protected void OnDeleteEvent(object sender, DeleteEventArgs a) {
    Application.Quit();
    a.RetVal = true;
  }

  protected void onDbClick(object sender, EventArgs e) {
    DatabaseManagerWindow databaseManagerWindow = new DatabaseManagerWindow();
    databaseManagerWindow.Show();
  }
}

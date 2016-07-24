using System;
using Gtk;
namespace Editor {

  /// <summary>
  /// Renders tilesets and selections
  /// </summary>
  internal class TilesetDrawningArea : DrawingArea {
    public TilesetDrawningArea() {
      
    }

    protected override bool OnExposeEvent(Gdk.EventExpose evnt) {
      Cairo.Context cr = Gdk.CairoHelper.Create(evnt.Window);

      cr.LineWidth = 9;
      cr.SetSourceRGB(0.7, 0.2, 0.0);

      int width, height;
      width = Allocation.Width;
      height = Allocation.Height;

      cr.Translate(width/2, height/2);
      cr.Arc(0, 0, (width < height ? width : height) / 2 - 10, 0, 2*Math.PI);
      cr.StrokePreserve();

      cr.SetSourceRGB(0.3, 0.4, 0.6);
      cr.Fill();

      ((IDisposable) cr.Target).Dispose();                                      
      ((IDisposable) cr).Dispose();
      return true;
    }
  }

  [System.ComponentModel.ToolboxItem(true)]
  /// <summary>
  /// This widget display list of tiles that can be edited
  /// </summary>
  public partial class TilesetEditorWidget : Bin {
    TilesetDrawningArea drawingArea;

    public TilesetEditorWidget() {
      this.drawingArea = new TilesetDrawningArea();
      SetSizeRequest(700, 700);
      this.Build();
      this.Add(drawingArea);
    }
  }
}


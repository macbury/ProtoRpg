using System;
using Gtk;
using Cairo;
using MonoRPG;


namespace Editor {

  /// <summary>
  /// Renders tilesets and selections
  /// </summary>
  internal class TilesetDrawningArea : DrawingArea {
    public Tileset Tileset {
      get;
      set;
    }

    public TilesetDrawningArea() {
      
    }

    /// <summary>
    /// Refresh this drawning area.
    /// </summary>
    public void Refresh() {
      this.QueueDraw();
    }

    protected override bool OnExposeEvent(Gdk.EventExpose evnt) {
      Cairo.Context cr = Gdk.CairoHelper.Create(evnt.Window);

      using(cr.GetTarget()) {
        using(cr) {
          cr.SetSourceRGB(0.0, 0.0, 0.0);
          cr.Rectangle(new Rectangle(new Point(), Allocation.Width, Allocation.Height));
          cr.Fill();
        }//((IDisposable) cr).Dispose();
      }//((IDisposable) cr.GetTarget()).Dispose();            
                               
      return true;
    }
  }

  [System.ComponentModel.ToolboxItem(true)]
  /// <summary>
  /// This widget display list of tiles that can be edited
  /// </summary>
  public partial class TilesetEditorWidget : Bin {
    private TilesetDrawningArea drawingArea;
    /// <summary>
    /// Gets or sets the current tileset that you can edit;
    /// </summary>
    /// <value>The current tileset.</value>
    public Tileset CurrentTileset {
      set {
        drawingArea.Tileset = value;
        drawingArea.Refresh();
      }

      get { return drawingArea.Tileset; }
    }

    public TilesetEditorWidget() {
      this.drawingArea = new TilesetDrawningArea();
      SetSizeRequest(700, 700);
      this.Build();
      this.Add(drawingArea);
    }
  }
}


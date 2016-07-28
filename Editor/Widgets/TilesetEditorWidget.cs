using System;
using Gtk;
using Cairo;
using MonoRPG;
using System.IO;
using GLib;
using System.Collections.Generic;


namespace Editor {

  /*/// <summary>
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
      if (Tileset == null) {
        Parent.SetSizeRequest(0, 0);
      } else {
        Parent.SetSizeRequest(700, 700);
      }

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
  }*/

  [System.ComponentModel.ToolboxItem(true)]
  /// <summary>
  /// This widget display list of tiles that can be edited
  /// </summary>
  public partial class TilesetEditorWidget : Bin {
    private Tileset _tileset;
    /// <summary>
    /// Gets or sets the current tileset that you can edit;
    /// </summary>
    /// <value>The current tileset.</value>
    public Tileset CurrentTileset {
      set {
        if (_tileset != value) {
          _tileset = value;
          Reload();
        }

        if (_tileset == null)
          UnloadTexture();
      }

      get { return _tileset; }
    }
    public MapManager MapManager;
    public AssetsManager AssetsManager;

    ImageSurface tilesetSurface;

    List<ImageSurface> tileSurfaces;


    public TilesetEditorWidget() {
      this.Build();
      this.tileSurfaces = new List<ImageSurface>();
      this.mapDrawingArea.ExposeEvent += OnExpose;
    }

    ~TilesetEditorWidget() {
      UnloadTexture();
      CurrentTileset = null;
      MapManager = null;
      AssetsManager = null;
    }

    /// <summary>
    /// Draw tileset here
    /// </summary>
    /// <param name="o">O.</param>
    /// <param name="args">Arguments.</param>
    private void OnExpose(object o, ExposeEventArgs args) {
      Cairo.Context cr = Gdk.CairoHelper.Create(args.Event.Window);

      using(cr.GetTarget()) {
        using(cr) {
          cr.Antialias = Antialias.None;
          cr.SetSourceRGB(0.0, 0.0, 0.0);

          cr.Rectangle(new Rectangle(new Point(), Allocation.Width, Allocation.Height));
          cr.Fill();


          if (HaveTexture) {
            LoadTexture();
            for (int x = 0; x < CurrentTileset.Width; x++) {
              for (int y = 0; y < CurrentTileset.Height; y++) {
                cr.NewPath();
                cr.SetSourceSurface(tileSurfaces[0], 16*x,16 * y);
                cr.Rectangle(16*x,16 *y, 16, 16);
                cr.Fill();
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Check if current tileset is set and have texture that exists on disk
    /// </summary>
    /// <returns><c>true</c>, if texture was had, <c>false</c> otherwise.</returns>
    public bool HaveTexture {
      get { return CurrentTileset != null && CurrentTileset.TextureName != null && File.Exists(TextureAbsolutePath); }
    }

    /// <summary>
    /// Return absolute path to texture
    /// </summary>
    /// <returns>The absolute path.</returns>
    public string TextureAbsolutePath {
      get {
        return MapManager.TilesetTextureAbsolutePath(CurrentTileset);
      }
    }

    /// <summary>
    /// Unloads the texture.
    /// </summary>
    private void UnloadTexture() {
      if (tilesetSurface != null)
        tilesetSurface.Dispose();

      tilesetSurface = null;

      foreach (var tileSurface in tileSurfaces) {
        tileSurface.Dispose();
      }
      tileSurfaces.Clear();
    }

    /// <summary>
    /// Loads new texture if old one is unloaded
    /// </summary>
    private void LoadTexture() {

      if (HaveTexture && tilesetSurface == null) {
        tilesetSurface = new ImageSurface(TextureAbsolutePath);

        foreach (var tile in CurrentTileset.Tiles) {
          var tileSurface    = new ImageSurface (Format.Argb32, tile.Rect.Width, tile.Rect.Height);
          using (Context g = new Context(tileSurface)) {
            g.SetSourceSurface(tilesetSurface, -tile.Rect.X, tile.Rect.Y);
            g.Paint ();
          }
          tileSurfaces.Add(tileSurface);
        }
      }
    }

    /// <summary>
    /// Resize tileset and redraw everything
    /// </summary>
    public void Reload() {
      UnloadTexture();
      if (HaveTexture) {
        LoadTexture();
        SetSizeRequest(tilesetSurface.Width, tilesetSurface.Height);
      } else {
        SetSizeRequest(0, 0);
      }
      mapDrawingArea.QueueDraw();
    }
  }
}


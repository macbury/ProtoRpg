using System;
using MonoRPG;
using Gtk;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace Editor {

  [TreeNode (ListOnly=true)]
  internal class TilesetTreeModel : TreeNode {
    public Tileset tileset;

    [TreeNodeValue (Column=0)]
    public string Id {
      get {
        return (tileset.Id+1).ToString("D4");
      }
    }

    [TreeNodeValue (Column=1)]
    public string Name {
      get {
        return tileset.Name;
      }
    }
  }

  /// <summary>
  /// Tilesets manager dialog. Here user can edit each tileset in system.
  /// </summary>
  public partial class TilesetsManagerDialog : Gtk.Dialog {
    MapManager mapManager;
    AssetsManager assets;
    GraphicsDevice graphicsDevice;

    const string TAG = "TilesetsManagerDialog";

    public TilesetsManagerDialog(MapManager mapManager, AssetsManager assets, GraphicsDevice graphicsDevice) {
      this.mapManager = mapManager;
      this.assets = assets;
      this.graphicsDevice = graphicsDevice;

      Destroyed += OnClose;
      this.Build();

      tilesetEditor.MapManager = mapManager;
      tilesetEditor.AssetsManager = assets;

      tilesetsNodeView.AppendColumn("Id", new CellRendererText(), "text", 0);
      tilesetsNodeView.AppendColumn("Name", new CellRendererText(), "text", 1);
      tilesetsNodeView.ShowAll();
      tilesetsNodeView.NodeSelection.Changed += new System.EventHandler(OnTilesetSelected);
      Reload();

      if (mapManager.Tilesets.Count > 0) {
        //tilesetsNodeView.NodeStore.GetEnumerator()[0];
        //tilesetsNodeView.NodeSelection.SelectNode(tilesetsNodeView.NodeStore.GetNode(new TreePath(0)));
      }

      RefreshUI();
    }

    /// <summary>
    /// Reference to tileset in tilesetEditor
    /// </summary>
    /// <value>The current tileset.</value>
    private Tileset CurrentTileset { get { return tilesetEditor.CurrentTileset; } }

    /// <summary>
    /// Set tileset to edit
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    public void OnTilesetSelected(object sender, EventArgs e) {
      if (sender.GetType() == typeof(NodeSelection)) {
        NodeSelection selection = (NodeSelection) sender;
        if (selection.SelectedNode != null) {
          var selectedTilesetNode = (TilesetTreeModel)selection.SelectedNode;
          tilesetEditor.CurrentTileset = selectedTilesetNode.tileset;

          tilesetNameEntry.Text = CurrentTileset.Name;
          if (CurrentTileset.TextureName == null) {
            tilesetGraphicsEntry.Text = "<empty>";
          } else {
            tilesetGraphicsEntry.Text = CurrentTileset.TextureName;
          }
        } else {
          tilesetNameEntry.Text = tilesetGraphicsEntry.Text = "";

          tilesetEditor.CurrentTileset = null;
        }
      }
      RefreshUI();
    }

    /// <summary>
    /// Hides or show tilesetEditorContainer if tilesetEditor have any tileset selected
    /// </summary>
    void RefreshUI() {
      tilesetEditModeToolbar.Sensitive = tilesetNameEntry.Sensitive = selectTilesetGraphicsButton.Sensitive = tilesetGraphicsEntry.Sensitive = tilesetEditor.CurrentTileset != null;
    }

    /// <summary>
    /// Adds loaded tilesets in mapManager to node view
    /// </summary>
    private void Reload() {
      var nodeStore = new NodeStore(typeof(TilesetTreeModel));
      tilesetsNodeView.NodeStore = nodeStore;

      foreach (var tileset in mapManager.Tilesets) {
        nodeStore.AddNode(new TilesetTreeModel() { tileset = tileset });
      }

      RefreshUI();
    }

    /// <summary>
    /// Reload tilesets on window close
    /// </summary>
    /// <param name="o">O.</param>
    /// <param name="args">Arguments.</param>
    private void OnClose(object o, EventArgs args) {
      tilesetEditor.Dispose();
      mapManager.ReloadTilesets();
      mapManager = null;
    }

    /// <summary>
    /// Close window and save tileset
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnOkButtonClicked(object sender, EventArgs e) {
      mapManager.SaveTilesets();
      Destroy();
    }

    /// <summary>
    /// Close window
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnCancelButtonClicked(object sender, EventArgs e) {
      Destroy();
    }

    /// <summary>
    /// Change tileset name on input text change
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnNameEntryChange(object sender, EventArgs e) {
      if (CurrentTileset != null) {
        CurrentTileset.Name = tilesetNameEntry.Text;
        tilesetsNodeView.QueueDraw();
      }
    }

    /// <summary>
    /// Show select file dialog. If user select diffrent tileset, then move it to tileset directory and clear and create new tileset data
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    protected void OnSelectTilesetGraphicsButtonClicked(object sender, EventArgs e) {
      DirectoryInfo tilesetsAbsolutePath = new DirectoryInfo(assets.PathResolver.Absolute(MapManager.TILESET_DIR));
      using(FileChooserDialog fileChooser = new FileChooserDialog ("Import Tileset", this, FileChooserAction.Open)) {
        fileChooser.AddButton(Stock.Cancel, ResponseType.Cancel);
        fileChooser.AddButton(Stock.Open, ResponseType.Ok);

        fileChooser.Filter = new FileFilter();
        fileChooser.Filter.AddPattern("*.png");
        fileChooser.SetCurrentFolder(tilesetsAbsolutePath.FullName);
        ResponseType RetVal = (ResponseType)fileChooser.Run();

        // handle the dialog's exit value
        // Read the file name from Fcd.Filename
        if (RetVal == ResponseType.Ok) {
          // do something
          // If file is not in tilesets directory then copy it there
          // if there is another file with the same name then show alert
          //Dire

          DirectoryInfo selectedFile = new DirectoryInfo(fileChooser.Filename);
          DirectoryInfo tilesetFile = selectedFile;
          string tilesetFileName = System.IO.Path.GetFileName(selectedFile.FullName);
          if (!FileHelper.IsInside(selectedFile, tilesetsAbsolutePath)) {
            tilesetFile = new DirectoryInfo(
              System.IO.Path.Combine(
                tilesetsAbsolutePath.FullName, 
                tilesetFileName
              )
            );
            Log.Info(TAG, "Importing tileset into project from " + selectedFile.FullName + " to " + tilesetFile.FullName);

            File.Copy(
              selectedFile.FullName,
              tilesetFile.FullName
            );

          } else {
            Log.Info(TAG, "We dont need to move this file" + fileChooser.Filename);
          }

          if (CurrentTileset.TextureName != tilesetFileName) {
            Log.Info(TAG, "New tileset detected! " + fileChooser.Filename);
            tilesetGraphicsEntry.Text = CurrentTileset.TextureName = tilesetFileName;

            //TODO check if graphic did change then reload settings
            using(var file = File.OpenRead(tilesetFile.FullName)) {
              using (var texture = Texture2D.FromStream(graphicsDevice, file)) {
                CurrentTileset.SetupUsingTexture(texture, mapManager.TileSize);
              }
            }

            tilesetEditor.Reload();
          } else {
            Log.Info(TAG, "Selected tileset with the same name. Ignorign it: " + fileChooser.Filename);
          }
        }

        fileChooser.Hide();
      }
    }
  }
}


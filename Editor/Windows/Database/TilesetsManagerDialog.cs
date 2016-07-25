using System;
using MonoRPG;
using Gtk;

namespace Editor {

  [TreeNode (ListOnly=true)]
  internal class TilesetTreeModel : TreeNode {
    public Tileset tileset;

    [TreeNodeValue (Column=0)]
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

    public TilesetsManagerDialog(MapManager mapManager) {
      this.mapManager = mapManager;

      Destroyed += OnClose;
      this.Build();

      tilesetsNodeView.AppendColumn("Name", new CellRendererText(), "text", 0);
      tilesetsNodeView.ShowAll();
      tilesetsNodeView.NodeSelection.Changed += new System.EventHandler(OnTilesetSelected);
      Reload();

      if (mapManager.Tilesets.Count > 0) {
        //tilesetsNodeView.NodeStore.GetEnumerator()[0];
        //tilesetsNodeView.NodeSelection.SelectNode(tilesetsNodeView.NodeStore.GetNode(new TreePath(0)));
      }

      RefreshUI();
    }

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
        } else {
          tilesetNameEntry.Text = "";
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
      mapManager.ReloadTilesets();
      mapManager = null;
    }

    protected void OnOkButtonClicked(object sender, EventArgs e) {
      Destroy();
    }

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
  }
}


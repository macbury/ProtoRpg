
// This file has been generated by the GUI designer. Do not modify.
namespace Editor
{
	public partial class TilesetsManagerDialog
	{
		private global::Gtk.UIManager UIManager;
		
		private global::Gtk.Action newAction;
		
		private global::Gtk.Action removeAction;
		
		private global::Gtk.RadioAction PassableAction;
		
		private global::Gtk.RadioAction LayerAction;
		
		private global::Gtk.HPaned hpaned1;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.NodeView tilesetsNodeView;
		
		private global::Gtk.Toolbar tilesetsManagerToolbar;
		
		private global::Gtk.HBox tilesetsEditorContainer;
		
		private global::Gtk.Frame frame2;
		
		private global::Gtk.Alignment GtkAlignment2;
		
		private global::Gtk.Table table1;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Button selectTilesetGraphicsButton;
		
		private global::Gtk.Entry tilesetGraphicsEntry;
		
		private global::Gtk.Entry tilesetNameEntry;
		
		private global::Gtk.Label GtkLabel4;
		
		private global::Gtk.ScrolledWindow scrolledwindow1;
		
		private global::Editor.TilesetEditorWidget tilesetEditor;
		
		private global::Gtk.Toolbar tilesetEditModeToolbar;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Editor.TilesetsManagerDialog
			this.UIManager = new global::Gtk.UIManager ();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
			this.newAction = new global::Gtk.Action ("newAction", null, null, "gtk-new");
			this.newAction.HideIfEmpty = false;
			w1.Add (this.newAction, null);
			this.removeAction = new global::Gtk.Action ("removeAction", null, null, "gtk-remove");
			this.removeAction.HideIfEmpty = false;
			w1.Add (this.removeAction, null);
			this.UIManager.InsertActionGroup (w1, 0);
			global::Gtk.ActionGroup w2 = new global::Gtk.ActionGroup ("EditTilesetGroup");
			this.PassableAction = new global::Gtk.RadioAction ("PassableAction", global::Mono.Unix.Catalog.GetString ("Passable"), null, null, 0);
			this.PassableAction.Group = new global::GLib.SList (global::System.IntPtr.Zero);
			this.PassableAction.HideIfEmpty = false;
			this.PassableAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Passable");
			w2.Add (this.PassableAction, null);
			this.LayerAction = new global::Gtk.RadioAction ("LayerAction", global::Mono.Unix.Catalog.GetString ("Layer"), null, null, 0);
			this.LayerAction.Group = this.PassableAction.Group;
			this.LayerAction.HideIfEmpty = false;
			this.LayerAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Layer");
			w2.Add (this.LayerAction, null);
			this.UIManager.InsertActionGroup (w2, 1);
			this.AddAccelGroup (this.UIManager.AccelGroup);
			this.WidthRequest = 800;
			this.HeightRequest = 600;
			this.Name = "Editor.TilesetsManagerDialog";
			this.Title = global::Mono.Unix.Catalog.GetString ("Configure tilesets");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Modal = true;
			this.Resizable = false;
			this.AllowGrow = false;
			this.DestroyWithParent = true;
			// Internal child Editor.TilesetsManagerDialog.VBox
			global::Gtk.VBox w3 = this.VBox;
			w3.Name = "dialog1_VBox";
			w3.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hpaned1 = new global::Gtk.HPaned ();
			this.hpaned1.CanFocus = true;
			this.hpaned1.Name = "hpaned1";
			this.hpaned1.Position = 223;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.tilesetsNodeView = new global::Gtk.NodeView ();
			this.tilesetsNodeView.CanFocus = true;
			this.tilesetsNodeView.Name = "tilesetsNodeView";
			this.GtkScrolledWindow.Add (this.tilesetsNodeView);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.GtkScrolledWindow]));
			w5.Position = 1;
			// Container child vbox2.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='tilesetsManagerToolbar'><toolitem/><toolitem/><toolitem name='newAction' action='newAction'/><toolitem name='removeAction' action='removeAction'/></toolbar></ui>");
			this.tilesetsManagerToolbar = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/tilesetsManagerToolbar")));
			this.tilesetsManagerToolbar.Name = "tilesetsManagerToolbar";
			this.tilesetsManagerToolbar.ShowArrow = false;
			this.tilesetsManagerToolbar.ToolbarStyle = ((global::Gtk.ToolbarStyle)(0));
			this.tilesetsManagerToolbar.IconSize = ((global::Gtk.IconSize)(1));
			this.vbox2.Add (this.tilesetsManagerToolbar);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.tilesetsManagerToolbar]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			this.hpaned1.Add (this.vbox2);
			global::Gtk.Paned.PanedChild w7 = ((global::Gtk.Paned.PanedChild)(this.hpaned1 [this.vbox2]));
			w7.Resize = false;
			// Container child hpaned1.Gtk.Paned+PanedChild
			this.tilesetsEditorContainer = new global::Gtk.HBox ();
			this.tilesetsEditorContainer.Name = "tilesetsEditorContainer";
			this.tilesetsEditorContainer.Spacing = 6;
			// Container child tilesetsEditorContainer.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table (((uint)(3)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Name");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Graphics");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.selectTilesetGraphicsButton = new global::Gtk.Button ();
			this.selectTilesetGraphicsButton.CanFocus = true;
			this.selectTilesetGraphicsButton.Name = "selectTilesetGraphicsButton";
			this.selectTilesetGraphicsButton.UseUnderline = true;
			this.selectTilesetGraphicsButton.Label = global::Mono.Unix.Catalog.GetString ("...");
			this.table1.Add (this.selectTilesetGraphicsButton);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.selectTilesetGraphicsButton]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.LeftAttach = ((uint)(2));
			w10.RightAttach = ((uint)(3));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.tilesetGraphicsEntry = new global::Gtk.Entry ();
			this.tilesetGraphicsEntry.CanFocus = true;
			this.tilesetGraphicsEntry.Name = "tilesetGraphicsEntry";
			this.tilesetGraphicsEntry.IsEditable = false;
			this.tilesetGraphicsEntry.InvisibleChar = '●';
			this.table1.Add (this.tilesetGraphicsEntry);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.tilesetGraphicsEntry]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.tilesetNameEntry = new global::Gtk.Entry ();
			this.tilesetNameEntry.CanFocus = true;
			this.tilesetNameEntry.Name = "tilesetNameEntry";
			this.tilesetNameEntry.IsEditable = true;
			this.tilesetNameEntry.InvisibleChar = '●';
			this.table1.Add (this.tilesetNameEntry);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.tilesetNameEntry]));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.GtkAlignment2.Add (this.table1);
			this.frame2.Add (this.GtkAlignment2);
			this.GtkLabel4 = new global::Gtk.Label ();
			this.GtkLabel4.Name = "GtkLabel4";
			this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Tileset settings</b>");
			this.GtkLabel4.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel4;
			this.tilesetsEditorContainer.Add (this.frame2);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.tilesetsEditorContainer [this.frame2]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child tilesetsEditorContainer.Gtk.Box+BoxChild
			this.scrolledwindow1 = new global::Gtk.ScrolledWindow ();
			this.scrolledwindow1.CanFocus = true;
			this.scrolledwindow1.Name = "scrolledwindow1";
			this.scrolledwindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledwindow1.Gtk.Container+ContainerChild
			global::Gtk.Viewport w16 = new global::Gtk.Viewport ();
			w16.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.tilesetEditor = new global::Editor.TilesetEditorWidget ();
			this.tilesetEditor.Events = ((global::Gdk.EventMask)(256));
			this.tilesetEditor.Name = "tilesetEditor";
			w16.Add (this.tilesetEditor);
			this.scrolledwindow1.Add (w16);
			this.tilesetsEditorContainer.Add (this.scrolledwindow1);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.tilesetsEditorContainer [this.scrolledwindow1]));
			w19.Position = 1;
			// Container child tilesetsEditorContainer.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString ("<ui><toolbar name='tilesetEditModeToolbar'><toolitem/><toolitem/></toolbar></ui>");
			this.tilesetEditModeToolbar = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/tilesetEditModeToolbar")));
			this.tilesetEditModeToolbar.Name = "tilesetEditModeToolbar";
			this.tilesetEditModeToolbar.Orientation = ((global::Gtk.Orientation)(1));
			this.tilesetEditModeToolbar.ShowArrow = false;
			this.tilesetEditModeToolbar.ToolbarStyle = ((global::Gtk.ToolbarStyle)(1));
			this.tilesetsEditorContainer.Add (this.tilesetEditModeToolbar);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.tilesetsEditorContainer [this.tilesetEditModeToolbar]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			this.hpaned1.Add (this.tilesetsEditorContainer);
			w3.Add (this.hpaned1);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(w3 [this.hpaned1]));
			w22.Position = 0;
			// Internal child Editor.TilesetsManagerDialog.ActionArea
			global::Gtk.HButtonBox w23 = this.ActionArea;
			w23.Name = "dialog1_ActionArea";
			w23.Spacing = 10;
			w23.BorderWidth = ((uint)(5));
			w23.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w24 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonCancel]));
			w24.Expand = false;
			w24.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w25 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonOk]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 800;
			this.DefaultHeight = 600;
			this.Show ();
			this.tilesetNameEntry.Changed += new global::System.EventHandler (this.OnNameEntryChange);
			this.selectTilesetGraphicsButton.Clicked += new global::System.EventHandler (this.OnSelectTilesetGraphicsButtonClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnCancelButtonClicked);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnOkButtonClicked);
		}
	}
}

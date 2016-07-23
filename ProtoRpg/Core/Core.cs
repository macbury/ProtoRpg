using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;

namespace ProtoRpg {
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Core : Game {
    const string TAG = "Core";

    MapManager mapManager;

    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Config config;
    Camera camera;

    public Core(Config config) {
      Log.Info(TAG, "Initializing...");
      this.config = config;
      graphics = new GraphicsDeviceManager(this);

      // Configure resolution
      graphics.PreferredBackBufferWidth  = this.config.Width;
      graphics.PreferredBackBufferHeight = this.config.Height;
      graphics.SynchronizeWithVerticalRetrace = true;
      graphics.IsFullScreen = false;

      Content.RootDirectory = "Content";
      mapManager = new MapManager(Content, config.TileSize); 
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize() {
      base.Initialize();
      camera = new Camera(GraphicsDevice, this.config.VirtualWidth, this.config.VirtualHeight);
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent() {
      Log.Info(TAG, "Loading content...");
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      mapManager.LoadMap("hello_world");

      //TODO: use this.Content to load your game content here 
    }

    protected override void UnloadContent() {
      mapManager.Dispose();
      Content.Unload();
      base.UnloadContent();
      camera.Dispose();
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      // For Mobile devices, this logic will close the Game when the Back button is pressed
      // Exit() is obsolete on iOS
      #if !__IOS__ &&  !__TVOS__
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();
      #endif
            
      // TODO: Add your update logic here
            
      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      graphics.GraphicsDevice.Clear(Color.Black);
      //camera.Position = new Vector2(-16, -16);

      camera.Update();

      int Rows = this.config.Rows;
      int Cols = this.config.Columns;
      int i = 0;

      //TODO Draw map and events in here!
      spriteBatch.Begin(transformMatrix: camera.View, samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.BackToFront); {
        for (int x = 0; x < Cols; x++) {
          for (int y = 0; y < Rows; y++) {
            Vector2 tilePosition = new Vector2(x, y);
            mapManager.GetTile(i++).Draw(spriteBatch, tilePosition);
          }
        }

      } spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}


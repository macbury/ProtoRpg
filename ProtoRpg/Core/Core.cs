using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace ProtoRpg {
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Core : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    Config config;
    Camera camera;
    Texture2D player;

    public Core(Config config) {
      this.config = config;
      graphics = new GraphicsDeviceManager(this);

      // Configure resolution
      graphics.PreferredBackBufferWidth  = this.config.Width;
      graphics.PreferredBackBufferHeight = this.config.Height;

      Content.RootDirectory = "Content";
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
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      player = Content.Load<Texture2D>("debug_player.png");
      //TODO: use this.Content to load your game content here 
    }

    protected override void UnloadContent() {
      Content.Unload();
      base.UnloadContent();
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
      camera.Position += new Vector2(-1, -1);
      camera.Update();
      //TODO: Add your drawing code here
      spriteBatch.Begin(transformMatrix: camera.View, samplerState: SamplerState.PointClamp); {
        spriteBatch.Draw(player, Vector2.Zero);
      } spriteBatch.End();
      base.Draw(gameTime);
    }
  }
}


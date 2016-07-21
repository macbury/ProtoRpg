using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProtoRpg {
  /// <summary>
  /// A camera with orthographic projection.
  /// </summary>
  public class Camera : IDisposable {
    private GraphicsDevice graphicsDevice;
    public Vector2 Position { get; set; }
    public Vector2 Origin { get; set; }
    public Matrix View { get; private set; }

    public Viewport Viewport { get { return this.graphicsDevice.Viewport; } } 

    private int VirtualWidth;
    private int VirtualHeight;

    public Camera(GraphicsDevice graphicsDevice, int VirtualWidth, int VirtualHeight) {
      this.graphicsDevice = graphicsDevice;
      this.VirtualWidth = VirtualWidth;
      this.VirtualHeight = VirtualHeight;
      Origin = Vector2.Zero;
      Position = Vector2.Zero;
      Update();
    }

    /// <summary>
    /// Resize virtual size of the screen
    /// </summary>
    /// <param name="Width">Width.</param>
    /// <param name="Height">Height.</param>
    public void Resize(int Width, int Height) {
      VirtualWidth = Width;
      VirtualHeight = Height;
    }

    /// <summary>
    /// Calculate view and projection matrix
    /// </summary>
    public void Update() {
      var scaleX = (float)Viewport.Width / VirtualWidth;
      var scaleY = (float)Viewport.Height / VirtualHeight;
      View = Matrix.CreateTranslation(new Vector3(-Position, 0.0f)) *
        Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
        Matrix.CreateRotationZ(0) * //TODO Set by var?
        Matrix.CreateScale(scaleX, scaleY, 1.0f) *
        Matrix.CreateTranslation(new Vector3(Origin, 0.0f)); 
    }

    #region IDisposable implementation

    public void Dispose() {
      graphicsDevice = null;
    }

    #endregion
  }
}


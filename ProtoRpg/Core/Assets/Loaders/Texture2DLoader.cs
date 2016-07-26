using System;
using MonoRPG;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonoRPG {

  /// <summary>
  /// Loads image file as texture
  /// </summary>
  public class Texture2DLoader : AssetLoader<Texture2D> {
    public Texture2DLoader() {
    }

    #region implemented abstract members of AssetLoader

    public override Texture2D Load(AssetsManager assetManager, string fileName) {
      Texture2D texture = null;
      using(var textureStream = TitleContainer.OpenStream(fileName)) {
        texture = Texture2D.FromStream(assetManager.GraphicsDevice, textureStream);
      }

      return texture;
    }

    #endregion

    #region implemented abstract members of AssetLoader

    public override void Dispose() {
      
    }

    #endregion


  }
}


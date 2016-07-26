using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Collections;

namespace MonoRPG {
  public class AssetLoaderNotFound : Exception {
    public AssetLoaderNotFound(Type type) : base("Could not find asset loader for type: " + type.ToString()) {}
  }

  public class AssetNotFound : Exception {
    public AssetNotFound(string path) : base("You need to add asset to load: " + path) {}
  }

  public class AssetNotLoaded : Exception {
    public AssetNotLoaded(string path) : base("You need to load asset first: " + path + " call Update() ") {}
  }

  /// <summary>
  /// Loads and stores assets like textures, bitmapfonts, tile maps, sounds, music and so on.
  /// </summary>
  public class AssetsManager : IDisposable {
    const string TAG = "AssetsManager";

    private Dictionary<string, Asset> assets;
    private Dictionary<Type, object> loaders;
    private Stack<Asset> pendingAssetsToLoad;
    private Stack<Asset> pendingAssetsToUnload;
    public GraphicsDevice GraphicsDevice;

    public AssetsManager(GraphicsDevice graphicsDevice) {
      this.GraphicsDevice = graphicsDevice;
      assets              = new Dictionary<string, Asset>();
      loaders             = new Dictionary<Type, object>();
      pendingAssetsToLoad = new Stack<Asset>();
      pendingAssetsToUnload = new Stack<Asset>();

      loaders.Add(typeof(Texture2D), new Texture2DLoader());
    }

    /// <summary>
    /// Load one asset at once. Returns true if loaded all assets
    /// </summary>
    public bool Update() {
      if (pendingAssetsToLoad.Count > 0) {
        Asset pendingAsset = pendingAssetsToLoad.Pop();
        Log.Info(TAG, "Loading: " + pendingAsset.Path);
        dynamic assetLoader = loaders[pendingAsset.ContentType];
        pendingAsset.Content = assetLoader.Load(this, pendingAsset.Path);
      }

      while(pendingAssetsToUnload.Count > 0) {
        Asset pendingAsset = pendingAssetsToUnload.Pop();
        if (pendingAsset.RefCount == 0) {
          Log.Info(TAG, "Disposing: " + pendingAsset.Path + " nothing references it");
          pendingAsset.Dispose();
          assets.Remove(pendingAsset.Path);
        }

      }
        
      return pendingAssetsToLoad.Count == 0;
    }

    /// <summary>
    /// Loads all pending assets at once
    /// </summary>
    public void LoadAll() {
      while(!Update()) {
        
      }
    }

    /// <summary>
    /// Adds the given asset to the loading queue of the AssetManager.
    /// </summary>
    public Asset Load<T>(string path) {
      if (assets.ContainsKey(path)) {
        Log.Info(TAG, "Referencing existing asset to load: " + path);
        Asset asset = assets[path];
        asset.RefCount++;
        return asset;
      } else {
        Type assetType = typeof(T);
        if (!loaders.ContainsKey(assetType)) {
          throw new AssetLoaderNotFound(assetType);
        }
        Log.Info(TAG, "New asset to load: " + path);
        Asset asset = new Asset() { RefCount = 1, Path = path, ContentType = assetType };
        assets.Add(path, asset);
        pendingAssetsToLoad.Push(asset);
        return asset;
      }
    }

    /// <summary>
    /// Gets the asset.
    /// </summary>
    /// <returns>The asset.</returns>
    /// <param name="path">Path.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public T GetAsset<T>(string path) {
      if (assets.ContainsKey(path)) {
        Asset asset = assets[path];

        if (asset.Loaded) {
          return (T)asset.Content;
        } else {
          throw new AssetNotLoaded(path);
        }
      } else {
        throw new AssetNotFound(path);
      }
    }

    /// <summary>
    /// Marks the given asset to be removed if all references are not used by other. Asset will be unloaded after LoadAll or Update will be trigered
    /// </summary>
    /// <param name="path">Path.</param>
    public void Unload(string path) {
      if (assets.ContainsKey(path)) {
        Asset asset = assets[path];

        if (asset.Loaded) {
          asset.Unload();
          if (!pendingAssetsToUnload.Contains(asset))
            pendingAssetsToUnload.Push(asset);
        } else {
          throw new AssetNotLoaded(path);
        }
      } else {
        throw new AssetNotFound(path);
      }
    }

    #region IDisposable implementation

    public void Dispose() {
      foreach (var asset in assets) {
        asset.Value.Dispose();
      }

      foreach (var asset in pendingAssetsToLoad) {
        asset.Dispose();
      }

      foreach (var loader in loaders) {
        ((IDisposable) loader.Value).Dispose();
      }
      loaders.Clear();
      assets.Clear();
      pendingAssetsToLoad.Clear();
    }

    #endregion
  }
}


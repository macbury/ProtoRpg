using System;

namespace MonoRPG {
  /// <summary>
  /// Helps to resolve path to file
  /// </summary>
  public interface IPathResolver {
    /// <summary>
    /// Return relative path to content
    /// </summary>
    /// <param name="path">Path.</param>
    string Relative(string path);

    /// <summary>
    /// Return absolute path
    /// </summary>
    /// <param name="path">Path.</param>
    string Absolute(string path);
  }
}


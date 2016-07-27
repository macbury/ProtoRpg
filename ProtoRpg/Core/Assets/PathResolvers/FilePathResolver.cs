using System;
using System.IO;

namespace MonoRPG {
  public class FilePathResolver : IPathResolver {
    const string CONTENT_DIRECTORY = "Content";

    public FilePathResolver() {
    }


    public string Relative(string path) {
      return Path.Combine(CONTENT_DIRECTORY, path);
    }

    public string Absolute(string path) {
      return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Relative(path));
    }

  }
}


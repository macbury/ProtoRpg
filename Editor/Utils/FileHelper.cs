using System;
using System.IO;

namespace Editor {
  public class FileHelper {
    public static bool IsInside(DirectoryInfo path, DirectoryInfo folder) {
      if (path.Parent == null) {
        return false;
      }

      if (String.Equals(path.Parent.FullName, folder.FullName, StringComparison.InvariantCultureIgnoreCase)) {
        return true;
      }

      return IsInside(path.Parent, folder);
    }
  }
}


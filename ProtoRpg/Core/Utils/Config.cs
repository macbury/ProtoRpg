﻿using System;
using ProtoRpg;

namespace ProtoRpg {
  // This class loads configurtion from config file.
  public class Config {
    // Width of the game window
    public int Width;
    // Height of the game window
    public int Height;

    public static Config Load() {
      XmlManager<Config> xml = new XmlManager<Config>();
      return xml.Load("./config.xml");
    }
  }
}


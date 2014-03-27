using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Merge
{
    public class TileDisplayDetail
    {
        public Color Background;
        public Color Foreground;
        public string Display;

        public TileDisplayDetail(string display, Color background, Color foreground)
        {
            Display = display;
            Background = background;
            Foreground = foreground;
        }
    }

    public static class MergeTileDisplayDetail
    {
        public static Dictionary<int, TileDisplayDetail> Style2048 = new Dictionary<int, TileDisplayDetail>()
        {
            {0,    new TileDisplayDetail ("",    Color.FromArgb(0xBB,0xAD,0xA0), Color.FromArgb(0x77,0x6E,0x65))},
            //{1,    new TileDisplayDetail ("1",    Color.FromArgb(0xEE,0xE4,0xDA), Color.FromArgb(0x77,0x6E,0x65))},
            {2,    new TileDisplayDetail ("2",    Color.FromArgb(0xEE,0xE4,0xDA), Color.FromArgb(0x77,0x6E,0x65))},
            {4,    new TileDisplayDetail ("4",    Color.FromArgb(0xED,0xE0,0xC8), Color.FromArgb(0x77,0x6E,0x65))},
            {8,    new TileDisplayDetail ("8",    Color.FromArgb(0xF2,0xB1,0x79), Color.FromArgb(0xF9,0xF6,0xF2))},
            {16,   new TileDisplayDetail ("16",   Color.FromArgb(0xF5,0x95,0x63), Color.FromArgb(0xF9,0xF6,0xF2))},
            {32,   new TileDisplayDetail ("32",   Color.FromArgb(0xF6,0x7C,0x5F), Color.FromArgb(0xF9,0xF6,0xF2))},
            {64,   new TileDisplayDetail ("64",   Color.FromArgb(0xF6,0x5E,0x3B), Color.FromArgb(0xF9,0xF6,0xF2))},
            {128,  new TileDisplayDetail ("128",  Color.FromArgb(0xED,0xCF,0x72), Color.FromArgb(0xF9,0xF6,0xF2))},
            {256,  new TileDisplayDetail ("256",  Color.FromArgb(0xED,0xCC,0x61), Color.FromArgb(0xF9,0xF6,0xF2))},
            {512,  new TileDisplayDetail ("512",  Color.FromArgb(0xED,0xC8,0x50), Color.FromArgb(0xF9,0xF6,0xF2))},
            {1024, new TileDisplayDetail ("1024", Color.FromArgb(0xED,0xC5,0x3F), Color.FromArgb(0xF9,0xF6,0xF2))},
            {2048, new TileDisplayDetail ("2048", Color.FromArgb(0xED,0xC2,0x2E), Color.FromArgb(0xF9,0xF6,0xF2))},
        };
    }
}

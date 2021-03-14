using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace taskTrayApp
{
    public static class MyIconUtil
    {
        public static readonly string[] iconDot = new string[]{
            "................",
            ".###...##...##..",
            "..#...#..#.#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#....#..#.",
            "..#...#..#.#..#.",
            ".###...##...##..",
            "................",
        };

        public static class NativeMethods
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public extern static bool DestroyIcon(IntPtr handle);
        }

        public static Icon Create16x16Icon(string[] iconDot)
        {
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (iconDot[y][x] == '#')
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }

            IntPtr Hicon = bmp.GetHicon();
            return Icon.FromHandle(Hicon);
        }

        public static void DestroyIcon(Icon icon)
        {
            NativeMethods.DestroyIcon(icon.Handle);
        }
    }
}

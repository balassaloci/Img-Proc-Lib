using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ImgFilterCs
{
    class TrackLib
    {

        public void LoadSequence(string formattedNames, string directory, ref List<Bitmap> imageSequence)
        {            
            string[] paths = Directory.GetFiles(directory);

            int i=1;    //start index, may need modification

            string fileName = directory + formattedNames.Replace("%d", i.ToString());
            while (File.Exists(fileName))
            {
                Bitmap newImg = new Bitmap(fileName);
                imageSequence.Add(newImg);

                i++;
                fileName = directory + formattedNames.Replace("%d", i.ToString());
            }
        }

        public Point trackFrame(Bitmap frame, Point trackerLocation, ref Color[,] match, int treshold)
        {


            return new Point();
        }

        bool isTrackerMatch(ref Bitmap frame, Point trackerLocation, ref Color[,] match, int treshold)
        {
            for (int i = 0; i < match.GetLength(0); i++)
            {
                for (int j = 0; j < match.GetLength(1); j++)
                {
                    Color pixel = frame.GetPixel(trackerLocation.X - i / 2 - i % 2, trackerLocation.Y - j / 2 - j % 2);

                    if (Math.Abs(pixel.R - match[i, j].R) > treshold) return false;
                    if (Math.Abs(pixel.G - match[i, j].G) > treshold) return false;
                    if (Math.Abs(pixel.B - match[i, j].B) > treshold) return false;

                }
            }
            return true;
        }


    }
}

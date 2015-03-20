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
        public List<Bitmap> sequence;
        int currentImage;

        public Point lastTrackPoint;
        public Color[,] lastMatch;


        public TrackLib()
        {
            sequence = new List<Bitmap>();
            currentImage = 0;

        }

        public Bitmap trackForward(ref Point trkPoint, ref Color[,] msk)
        {
            currentImage++;
            if (currentImage >= sequence.Count()) currentImage = sequence.Count() - 1;
            //img = sequence[currentImage];
            //return currentImage + 1;
            return null;
        }

        public int getPrevious(ref Bitmap img)
        {
            currentImage--;
            if (currentImage < 0) currentImage = 0;
            img = sequence[currentImage];
            return currentImage + 1;
        }

        public int currentIndex()
        {
            return currentImage + 1;
        }

        public int SequenceSize()
        {
            return sequence.Count();
        }
        
        /// <summary>
        /// Getting formatted name and directory from preexisting path
        /// </summary>
        /// <param name="fileName">Original full path</param>
        /// <param name="dir">Directory</param>
        /// <param name="formattedName">Formatted name</param>
        public void getFormattedName(string fileName, ref string dir, ref string formattedName)
        {
            string[] nameParts = fileName.Split('\\');
            
            dir = fileName.Substring(0, fileName.Length - nameParts.Last().Length);

            string originalFileName = nameParts.Last();
            string nameBeginning = originalFileName.Split('_').First() + "_";
            string nameEnding = "." + originalFileName.Split('.').Last();

            formattedName = nameBeginning + "%d" + nameEnding;
        }
        
        /// <summary>
        /// Load an image sequence into a list of Bitmaps
        /// </summary>
        /// <param name="formattedName">Formatted filename eg: 123_%d.jpg (%d is where the index comes)</param>
        /// <param name="directory">Directory in which the files are in</param>
        /// <param name="imageSequence">List to which loader outputs images</param>
        public void LoadSequence(string formattedName, string directory)
        {            
            //string[] paths = Directory.GetFiles(directory);

            int i = 1;    //start index, may need modification
            sequence.Clear();

            string fileName = directory + formattedName.Replace("%d", i.ToString());
            while (File.Exists(fileName))
            {
                Bitmap newImg = new Bitmap(fileName);
                sequence.Add(newImg);

                i++;

                fileName = directory + formattedName.Replace("%d", i.ToString());
            }
        }

        /// <summary>
        /// Track a frame, adjust match and everything accordingly
        /// </summary>
        /// <param name="frame">Frame to track</param>
        /// <param name="trackerLocation">Previous tracker location</param>
        /// <param name="match">Match mask</param>
        /// <param name="treshold">Treshold to check for</param>
        /// <param name="radius">Radius to check in</param>
        /// <returns>New tracker location</returns>
        public Point trackFrame(Bitmap frame, Point trackerLocation, ref Color[,] match, int treshold, int radius)
        {

            for (int i = 1; i < radius; i++)
            {
                if (checkAroundSquare(ref frame, ref trackerLocation, ref match, treshold / i, i)) {
                    
                    return trackerLocation;
                }
            }
            
            return new Point();
        }

        /// <summary>
        /// Check frame around point for specific radius if matching shape found
        /// </summary>
        /// <param name="frame">A frame to be tracked</param>
        /// <param name="trackerLocation">Previous tracker location, around which new tracker to be looked for</param>
        /// <param name="match">Texture considered to be match</param>
        /// <param name="treshold">Maximum deviation from tracked values (per pixel)</param>
        /// <param name="radius">Max radius to look for new match</param>
        /// <returns>Match was found or not</returns>
        bool checkAroundSquare(ref Bitmap frame, ref Point trackerLocation, ref Color[,] match, int treshold, int radius)
        {
            for (int i = trackerLocation.X - radius / 2; i < trackerLocation.X + radius / 2; i++)
            {
                for (int j = trackerLocation.Y - radius / 2; j < trackerLocation.Y + radius / 2; j++)
                {
                    Point newTracker = new Point(i, j);

                    if (isTrackerMatch(ref frame, newTracker, ref match, treshold))
                    {
                        trackerLocation = newTracker;
                        return true;
                    }
                }
            }

            return false;
        }

        //If tracker matches specific pixel (within treshold)
        //Also updates Color[,] match to match new finding, so gradually adopting to
        //Gradient changes
        bool isTrackerMatch(ref Bitmap frame, Point trackerLocation, ref Color[,] match, int treshold)
        {
            int matchW = trackerLocation.X - (int)Math.Floor((double)match.GetLength(0) / 2);
            int matchH = trackerLocation.Y - (int)Math.Floor((double)match.GetLength(1) / 2);

            for (int i = 0; i < match.GetLength(0); i++)
            {
                for (int j = 0; j < match.GetLength(1); j++)
                {
                    Color pixel = frame.GetPixel(i + matchW, j + matchH);

                    if (Math.Abs(pixel.R - match[i, j].R) > treshold) return false;
                    if (Math.Abs(pixel.G - match[i, j].G) > treshold) return false;
                    if (Math.Abs(pixel.B - match[i, j].B) > treshold) return false;

                }
            }

            
            return true;
        }

        void SamplePixels(ref Color[,] match, ref Bitmap frame) {
            //Copy new pixel to match to update. (Continuously keep updating shit)
            for (int i = 0; i < match.GetLength(0); i++)
            {
                for (int j = 0; j < match.GetLength(1); j++)
                {
                   // match[i, j] = frame.GetPixel(matchW + i, matchH + j);
                }
            }

        }

        /// <summary>
        /// Draw the marker around a tracker location
        /// </summary>
        /// <param name="frame">Frame to draw the marker on</param>
        /// <param name="tracker">Tracker Location</param>
        /// <param name="radius">Tracker radius</param>
        /// <returns>New frame</returns>
        public Bitmap drawMarker(Bitmap frame, Point tracker, int radius)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                Color colr = Color.FromArgb(255, Color.Gray);
                SolidBrush brush = new SolidBrush(colr);
                Pen pen = new Pen(colr, 1);

                Rectangle aroundMarker = new Rectangle(tracker.X - radius / 2, tracker.Y - radius / 2, radius, radius);
                g.DrawRectangle(pen, aroundMarker);

                g.DrawLine(pen, tracker, tracker);
            }

            lastTrackPoint = tracker;

            return frame;
        }

    }
}

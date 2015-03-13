using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ImgFilterCs
{
    class FilterLib
    {
        public Bitmap orig;
        public int finishedThreads;
        
        //to be set using setMask(msk);
        public float[,] mask;
        //public int maskSum;
        public int maskOffsetW;
        public int maskOffsetH;


        public FilterLib (Bitmap img) {
            orig = img;
        }

        //init mask
        public void setMask(float[,] newMask)
        {
            //maskSum = new int();
            //maskSum = 0;
            mask = newMask;

            //for (int i = 0; i < mask.GetLength(0); i++)
            //{
            //    for (int j = 0; j < mask.GetLength(1); j++)
            //    {
            //        maskSum += mask[i, j];
            //    }
            //}

            maskOffsetW = (int)Math.Floor((decimal)newMask.GetLength(0) / 2); // - (newMask.GetLength(0) % 2); //round down
            maskOffsetH = (int)Math.Floor((decimal)newMask.GetLength(1) / 2); // - (newMask.GetLength(1) % 2); //round down
            
        }

        public Bitmap applyMask(float[,] newMask)
        {

            setMask(newMask);

            Bitmap newImg = new Bitmap(orig.Width, orig.Height);

            for (int i = 0 + maskOffsetW; i < orig.Width - maskOffsetW; i++)
            {
                for (int j = 0 + maskOffsetH; j < orig.Height - maskOffsetH; j++)
                {
                    Color newPx = getMaskedPixel(i, j);
                    newImg.SetPixel(i, j, newPx);
                }
            }

            return newImg;
        }

        Color getMaskedPixel(int x, int y)
        {
            float red = 0;
            float green = 0;
            float blue = 0;
            
            int coordX = 0;
            int coordY = 0;

            for (int kx = 0; kx <= maskOffsetW * 2; kx++)
            {
                for (int ky = 0; ky <= maskOffsetH * 2; ky++)
                {
                    coordX = kx + x - maskOffsetW;
                    coordY = ky + y - maskOffsetH;

                    red += orig.GetPixel(coordX, coordY).R * mask[kx, ky];
                    green += orig.GetPixel(coordX, coordY).G * mask[kx, ky];
                    blue += orig.GetPixel(coordX, coordY).B * mask[kx, ky];

                }
            }


            int red_ = BurnoutLimit(red);
            int green_ = BurnoutLimit(green);
            int blue_ = BurnoutLimit(blue);

            return Color.FromArgb(red_, green_, blue_);
        }

        int BurnoutLimit(float color)
        {
            if (color > 255) color = 255;
            if (color < 0) color = 0;

            return (int)color;
        }

        public Bitmap GetBlurred(int fromX, int toX, int fromY, int toY)
        {
            Bitmap newImg = new Bitmap(orig.Width, orig.Height);

            for (int i = fromX; i < toX; i++)
            {
                for (int j = fromY; j < toY; j++)
                {
                    Color newCol = getBlurredColVal(i, j);
                    newImg.SetPixel(i, j, newCol);
                }
            }
            finishedThreads++;

            return newImg;
        }

        Color getBlurredColVal(int x, int y)
        {
            int red = 0;
            int green = 0;
            int blue = 0;
           

            int coordX = 0;
            int coordY = 0;

            for (int kx = -1; kx <= 1; kx++)
            {
                for (int ky = -1; ky <= 1; ky++)
                {
                    coordX = kx + x;
                    coordY = ky + y;

                    red += orig.GetPixel(coordX, coordY).R;
                    green += orig.GetPixel(coordX, coordY).G;
                    blue += orig.GetPixel(coordX, coordY).B;

                    //float g = (float)(orig.GetPixel(coordX, coordY).G);
                    //float b = (float)(orig.GetPixel(coordX, coordY).B);

                    //red += r * filterVal;
                    //green += g * filterVal;
                    //blue += b * filterVal;
                }
            }

            

            return Color.FromArgb(red / 9, green / 9, blue / 9);
        }


        /// <summary>
        /// Checking for pixels with given color at a specific point in the picture
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Bitmap FindWColor(Color match, int treshold)
        {
            Bitmap ret = (Bitmap)orig.Clone();

            int minX = orig.Width, maxX = 0, minY = orig.Height, maxY = 0;

            for (int i = 0; i < ret.Width; i++)
            {
                for (int j = 0; j < ret.Height; j++)
                {
                    if (isMatch(match, orig.GetPixel(i, j), treshold))
                    {
                        if (i < minX) minX = i;
                        if (j < minY) minY = j;

                        if (i > maxX) maxX = i;
                        if (j > maxY) maxY = j;
                    }
                }
            }

            using (Graphics g = Graphics.FromImage(ret))
            {
                Color colr = Color.FromArgb(255, Color.Red);
                SolidBrush brush = new SolidBrush(colr);

                Pen pen = new Pen(colr,5);
                
                g.DrawRectangle(pen, minX, minY, maxX - minX, maxY - minY);
            }


            return ret;
        }

        public Bitmap blobDetect(Color match, int treshold, int maxDistance)
        {
            Bitmap ret = (Bitmap)orig.Clone();
            List<Point> colorPoints = new List<Point>();
            List<Blob> blobs = new List<Blob>();

            //finds points
            findMatchPoints(match, treshold, ref colorPoints);

            //detects blobs (groups)
            pointsToBlobs(ref colorPoints, ref blobs, maxDistance);
            
            //draws rectangles around blobs
            drawBlobs(ref blobs, ref ret);

            return ret;
        }

        void drawBlobs(ref List<Blob> blobs, ref Bitmap img)
        {
            foreach (Blob blob in blobs)
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    Color colr = Color.FromArgb(255, Color.Red);
                    SolidBrush brush = new SolidBrush(colr);

                    Pen pen = new Pen(colr, 3);

                    g.DrawRectangle(pen, blob.getRect());
                }
            }
        }

        class Blob
        {

            public Blob()
            {
                points = new List<Point>();
            }

            public List<Point> points;

            public bool contains(Point point)
            {
                foreach (Point pt in points)
                {
                    if (pt == point) return true;
                }
                return false;
            }

            public bool neighbourTo(Point point, int treshold)
            {
                foreach (Point pt in points)
                {
                    if (Math.Abs(pt.X - point.X) < treshold && Math.Abs(pt.Y - point.Y) < treshold)
                    {
                        return true;
                    }
                }

                return false;
            }

            public Rectangle getRect() {
                if (points.Count == 0)
                {
                    return  new Rectangle(0, 0, 0, 0);
                }
                
                int minX = points[0].X, maxX = points[0].X, minY = points[0].Y, maxY = points[0].Y;

                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X < minX) minX = points[i].X;
                    if (points[i].Y < minY) minY = points[i].Y;

                    if (points[i].X > maxX) maxX = points[i].X;
                    if (points[i].Y > maxY) maxY = points[i].Y;
                }

                return new Rectangle(minX, minY, maxX - minX, maxY - minY);
            }

        }

        void pointsToBlobs(ref List<Point> colorPoints, ref List<Blob> blobs, int maxDist)
        {

            while (colorPoints.Count > 0)
            {
                /* V1 of code
                int neighbour = findNeighbour(ref blobs, colorPoints[0], maxDist);

                //Add new blob
               

                colorPoints.Remove(colorPoints[0]);
                 */

                int underArea = IsUnderArea(ref blobs, colorPoints[0]);

                if (underArea < 0)
                {
                    int neighbour = findNeighbour(ref blobs, colorPoints[0], maxDist);

                    if (neighbour < 0)
                    {
                        Blob newBlob = new Blob();
                        newBlob.points.Add(colorPoints[0]);
                        blobs.Add(newBlob);
                    }
                    else
                    {
                        blobs[neighbour].points.Add(colorPoints[0]);
                    }
                }
                else
                {
                    blobs[underArea].points.Add(colorPoints[0]);
                }

                colorPoints.Remove(colorPoints[0]);              

                //New line added
            }
        }

        int IsUnderArea(ref List<Blob> blobs, Point point)
        {
            for (int i = 0; i < blobs.Count; i++)
            {

                Rectangle blobArea = blobs[i].getRect();

                if (blobArea.Contains(point))
                {
                    return i;
                }
                
            }

            return -1;
        }

        int findNeighbour(ref List<Blob> blobs, Point point, int maxDist)
        {
            for (int i=0;i< blobs.Count;i++) {
                if (blobs[i].neighbourTo(point, maxDist))
                {
                    return i;
                }
            }

            return -1;
        }

        public void findMatchPoints(Color match, int treshold, ref List<Point> colorPts)
        {
            colorPts.Clear();

            for (int i = 0; i < orig.Width; i++)
            {
                for (int j = 0; j < orig.Height; j++)
                {
                    if (isMatch(match, orig.GetPixel(i, j), treshold))
                    {
                        Point loc = new Point(i, j);
                        colorPts.Add(loc);
                    }
                }
            }

        }
        
        bool isMatch(Color match, Color px, int treshold)
        {
            if (Math.Abs(px.R - match.R) > treshold) return false;
            if (Math.Abs(px.G - match.G) > treshold) return false;
            if (Math.Abs(px.B - match.B) > treshold) return false;
            return true;
        }

        public void ToBW()
        {
            for (int i = 0; i < orig.Width; i++)
            {
                for (int j = 0; j < orig.Height; j++)
                {
                    int luma = orig.GetPixel(i, j).R + orig.GetPixel(i, j).G + orig.GetPixel(i, j).B;
                    luma /=3;

                    Color newPx = new Color();
                    newPx = Color.FromArgb(luma, luma, luma);

                    orig.SetPixel(i, j, newPx);
                }
            }
        }

    }
}

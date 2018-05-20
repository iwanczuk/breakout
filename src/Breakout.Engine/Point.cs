namespace Breakout.Engine
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int W { get { return X; } set { X = value; } }
        public int H { get { return Y; } set { Y = value; } }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

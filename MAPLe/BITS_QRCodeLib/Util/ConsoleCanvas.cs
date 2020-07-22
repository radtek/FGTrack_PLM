using System;
using Line = HTN.BITS.QRCodeLib.Geom.Line;
using Point = HTN.BITS.QRCodeLib.Geom.Point;

namespace HTN.BITS.QRCodeLib.Util
{
    public class ConsoleCanvas : DebugCanvas
    {

        public void println(String str)
        {
            Console.WriteLine(str);
        }

        public void drawPoint(Point point, int color)
        {
        }

        public void drawCross(Point point, int color)
        {

        }

        public void drawPoints(Point[] points, int color)
        {
        }

        public void drawLine(Line line, int color)
        {
        }

        public void drawLines(Line[] lines, int color)
        {
        }

        public void drawPolygon(Point[] points, int color)
        {
        }

        public void drawMatrix(bool[][] matrix)
        {

        }

    }
}

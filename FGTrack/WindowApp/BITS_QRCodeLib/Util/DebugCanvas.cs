﻿using System;
using Line = HTN.BITS.QRCodeLib.Geom.Line;
using Point = HTN.BITS.QRCodeLib.Geom.Point;

namespace HTN.BITS.QRCodeLib.Util
{
    public interface DebugCanvas
    {
        void println(String str);
        void drawPoint(Point point, int color);
        void drawCross(Point point, int color);
        void drawPoints(Point[] points, int color);
        void drawLine(Line line, int color);
        void drawLines(Line[] lines, int color);
        void drawPolygon(Point[] points, int color);
        void drawMatrix(bool[][] matrix);
    }
}

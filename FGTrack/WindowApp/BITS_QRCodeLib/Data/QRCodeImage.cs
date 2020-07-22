using System;

namespace HTN.BITS.QRCodeLib.Data
{
    public interface QRCodeImage
    {
        int Width
        {
            get;

        }
        int Height
        {
            get;

        }
        int getPixel(int x, int y);
    }
}

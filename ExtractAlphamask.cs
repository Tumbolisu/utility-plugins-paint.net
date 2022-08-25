// Name: Extract Alphamask
// Submenu: Color
// Author: Tumby#5171
// Title: Extract Alphamask
// Version: 1.0
// Desc:
// Keywords: alpha|alphamask|mask
// URL:
// Help:
#region UICode
#endregion

void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra pix;

    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;
        for (int x = rect.Left; x < rect.Right; x++)
        {
            pix = src[x,y];

            pix.R = pix.A;
            pix.G = pix.A;
            pix.B = pix.A;
            pix.A = (byte)255;

            dst[x,y] = pix;
        }
    }
}

// Name: Extract Alphamask
// Submenu: Alpha
// Author: Tumbolisu aka Tumby
// Title: Extract Alphamask
// Version: 1.1
// Desc: Convert alpha channel into greyscale image.
// Keywords: alpha|alphamask|mask
// URL: https://github.com/Tumbolisu/utility-plugins-paint.net
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

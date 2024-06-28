// Name: Alpha To: Fully Transparent
// Submenu: Alpha
// Author: Tumbolisu aka Tumby
// Title: Alpha To: Fully Transparent
// Version: 1.0
// Desc: Force selected pixels to be fully transparent.
// Keywords: alpha|transparent
// URL: https://github.com/Tumbolisu/utility-plugins-paint.net
// Help:
#region UICode
#endregion


unsafe void Render(Surface dst, Surface src, Rectangle rect)
{
    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        ColorBgra* srcPtr = src.GetPointPointerUnchecked(rect.Left, y);
        ColorBgra* dstPtr = dst.GetPointPointerUnchecked(rect.Left, y);

        for (int x = rect.Left; x < rect.Right; x++)
        {
            ColorBgra Pixel = *srcPtr;

            Pixel.A = 0;

            *dstPtr = Pixel;
            srcPtr++;
            dstPtr++;
        }
    }
}

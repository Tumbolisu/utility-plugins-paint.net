// Name: Alpha To: Fully Opaque
// Submenu: Alpha
// Author: Tumbolisu aka Tumby
// Title: Alpha To: Fully Opaque
// Version: 1.0
// Desc: Force selected pixels to be fully opaque.
// Keywords: alpha|opaque
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

            Pixel.A = 255;

            *dstPtr = Pixel;
            srcPtr++;
            dstPtr++;
        }
    }
}

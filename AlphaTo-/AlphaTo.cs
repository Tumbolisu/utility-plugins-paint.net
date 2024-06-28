// Name: Alpha To:
// Submenu: Alpha
// Author: Tumbolisu aka Tumby
// Title: Alpha To:
// Version: 1.0
// Desc: Force selected pixels to have a uniform, user-chosen transparency.
// Keywords: alpha|transparent|transparency
// URL: https://github.com/Tumbolisu/utility-plugins-paint.net
// Help:
#region UICode
IntSliderControl user_amount = 128; // [0,255] Amount
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

            Pixel.A = (byte)(user_amount);

            *dstPtr = Pixel;
            srcPtr++;
            dstPtr++;
        }
    }
}

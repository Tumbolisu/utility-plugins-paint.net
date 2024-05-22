// Name: Apply Alphamask
// Submenu: Alpha
// Author: Tumbolisu aka Tumby
// Title: Apply Alphamask
// Version: 1.0
// Desc: Change the alpha channel based on an image in your clipboard.
// Keywords: alpha|alphamask|mask
// URL: https://github.com/Tumbolisu/utility-plugins-paint.net
// Help: This plugin requires you to have an image in your clipboard. The pixels from the clipboard image are then applied as an alpha mask to the selected area. Use the different read and write modes to change how the clipboard image is used, and how the alpha of the selected area is affected. Lastly, you can also invert the alpha mask. Without inversion: white = opaque, black = transparent. With inversion: black = opaque, white = transparent.
#region UICode
CheckboxControl user_invert = false; // Invert Alpha (default: off)
ListBoxControl user_read = 3; // Read Mode|Red Only|Green Only|Blue Only|RGB Average (default)|RGB Luminance|Alpha Only
ListBoxControl user_write = 0; // Write Mode|Overwrite (default)|Multiply|Add
#endregion


// grayscale wrapped clipboard
byte[,] clip = null;

private Surface clipboardSurface = null;
private bool readClipboard = false; // this is the past form of read, not present.


protected override void OnDispose(bool disposing)
{
    if (disposing)
    {
        // Release any surfaces or effects you've created
        clipboardSurface?.Dispose(); clipboardSurface = null;
    }

    base.OnDispose(disposing);
}


void PreRender(Surface dst, Surface src)
{
    if (clip == null)
    {
        clip = new byte[src.Width,src.Height];
    }

    if (!readClipboard)
    {
        readClipboard = true;
        clipboardSurface = Services.GetService<IClipboardService>().TryGetSurface();
    }

    // Copy from the Clipboard to the clip surface
    ColorBgra color;
    byte intensity;
    for (int y = 0; y < clip.GetLength(1); y++)
    {
        if (IsCancelRequested) return;
        for (int x = 0; x < clip.GetLength(0); x++)
        {
            if (clipboardSurface != null)
            {
                color = clipboardSurface.GetBilinearSampleWrapped(x, y);
                switch (user_read)
                {
                case 0:  // Red Only
                    intensity = color.R;
                    break;

                case 1:  // Green Only
                    intensity = color.G;
                    break;

                case 2:  // Blue Only
                    intensity = color.B;
                    break;

                case 3:  // RGB Average
                    intensity = (byte)((color.R + color.G + color.B) / 3);
                    break;

                case 4:  // RGB Luminance
                    // luminance integer approximation from libpng
                    intensity = (byte)((6968 * color.R + 23434 * color.G + 2366 * color.B) / 32768);
                    break;

                case 5:  // Alpha Only
                    intensity = color.A;
                    break;

                default:  // UNKNOWN
                    intensity = (byte)(((x+y)%4) * 255/3);
                    break;
                }

                if (user_invert)
                {
                    intensity = (byte)(255 - intensity);
                }
                clip[x,y] = intensity;
            }
            else  // no image in clipboard
            {
                clip[x,y] = (byte)(((x+y)%2) * 255/1);
            }
        }
    }
}


void Render(Surface dst, Surface src, Rectangle rect)
{
    ColorBgra pixel;
    byte mask;
    int alpha;

    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            pixel = src[x,y];
            mask = clip[x,y];

            switch (user_write)
            {
            case 0:  // Overwrite
                alpha = (int)mask;
                break;

            case 1:  // Multiply
                alpha = (int)(pixel.A) * (int)mask;
                alpha /= 255;
                break;

            case 2:  // Add
                alpha = (int)(pixel.A) + (int)mask;
                alpha = (alpha > 255 ? 255 : alpha);
                break;

            default:  // UNKNOWN
                alpha = ((x+y)%6) * 255/5;
                break;
            }

            pixel.A = (byte)alpha;

            dst[x,y] = pixel;
        }
    }
}

// Name: Scroll
// Submenu: Utility
// Author: Tumby#5171
// Title: Scroll
// Version: 1.0
// Desc:
// Keywords: scroll|shift|move|offset|tile|repeat
// URL:
// Help:
#region UICode
ListBoxControl user_offset_mode = 0; // Offset Type|Relative|Absolute
DoubleSliderControl user_x_offset_relative = 50; // [0,100] {user_offset_mode} Relative X Offset
DoubleSliderControl user_y_offset_relative = 50; // [0,100] {user_offset_mode} Relative Y Offset
IntSliderControl user_x_offset_absolute = 0; // [-200,200] {!user_offset_mode} Absolute X Offset
IntSliderControl user_y_offset_absolute = 0; // [-200,200] {!user_offset_mode} Absolute Y Offset
#endregion

int ModPositive(int dividend, int divisor)
{
    int result = dividend % divisor;
    if (result < 0)
        result += divisor;
    return result;
}

void Render(Surface dst, Surface src, Rectangle rect)
{
    int iw = src.Width;  // full image width
    int ih = src.Height;  // full image height

    Rectangle selection = EnvironmentParameters.SelectionBounds;
    int sl = selection.Left;
    int st = selection.Top;
    int sw = selection.Width;
    int sh = selection.Height;

    ColorBgra pix = ColorBgra.Black;  // Work Pixel

    int x_offset = 0;
    int y_offset = 0;

    switch (user_offset_mode)
    {
    case 0:  // Relative
        x_offset = (int)Math.Round(sw * user_x_offset_relative / 100.0);
        y_offset = (int)Math.Round(sh * user_y_offset_relative / 100.0);
        break;

    case 1:  // Absolute
        x_offset = user_x_offset_absolute;
        y_offset = user_y_offset_absolute;
        break;
    }

    for (int y = rect.Top; y < rect.Bottom; y++)
    {
        if (IsCancelRequested) return;

        for (int x = rect.Left; x < rect.Right; x++)
        {
            pix = src[ModPositive(x - sl - x_offset, sw) + sl, ModPositive(y - st - y_offset, sh) + st];
            dst[x,y] = pix;
        }
    }  // end of pixel loops
}

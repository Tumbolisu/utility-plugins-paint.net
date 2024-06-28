# Utility Plugins for Paint.NET

A collection of extremely simple utility plugins for Paint.NET.

For now, all plugins edit the alpha channel. Some other plugins will be added in
the future, whenever I feel like it.


# List of Plugins


## ![Icon](AlphaTo-/AlphaTo.png) Alpha To:...

Force selected pixels to have a uniform, user-chosen transparency.


## ![Icon](AlphaTo-/AlphaToFullyOpaque.png) Alpha To: Fully Opaque

Force selected pixels to be fully opaque.


## ![Icon](AlphaTo-/AlphaToFullyTransparent.png) Alpha To: Fully Transparent

Force selected pixels to be fully transparent.


## ![Icon](ApplyAlphamask/ApplyAlphamask.png) Apply Alphamask

Uses the image in your clipboard as an alphamask and applies it to the canvas
image.

Here is a little sample output image:

![Sample Image](ApplyAlphamask/ApplyAlphamask.sample.png)

This is the image in the clipboard:

![Clipboard Image](ApplyAlphamask/ApplyAlphamask.sample-clipboard.png)


## ![Icon](ExtractAlphamask/ExtractAlphamask.png) Extract Alphamask

Extracts the alphamask from the canvas image. The end result is an opaque
gray-scale image, where black means transparent, and white means opaque.

Here is a little sample output image:

![Sample Image](ExtractAlphamask/ExtractAlphamask.sample.png)

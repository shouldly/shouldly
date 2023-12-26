using System.Drawing;
using System.Drawing.Imaging;

namespace Shouldly.MessageGenerators;

class ImageShouldBeEqualGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context is
        {
            ShouldMethod: "ShouldBe" or "ShouldNotBe",
            Expected: byte[],
            Actual: byte[],
        };

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        Debug.Assert(context.Expected is byte[]);
        Debug.Assert(context.Actual is byte[]);
        byte[] expectedBytes = (byte[])context.Expected;
        byte[] actualBytes = (byte[])context.Actual;
        Bitmap? expectedBitmap = ConvertToBitmap(expectedBytes);
        Bitmap? actualBitmap = ConvertToBitmap(actualBytes);

        if (expectedBitmap is null || actualBitmap is null)
        {
            return new ShouldBeMessageGenerator().GenerateErrorMessage(context);
        }

        if (!expectedBitmap.RawFormat.Equals(actualBitmap.RawFormat))
        {

            return $@"{context.CodePart} image format {context.ShouldMethod.PascalToSpaced()} {GetImageName(expectedBitmap.RawFormat)} but was {GetImageName(actualBitmap.RawFormat)}";
        }

        if (!expectedBitmap.Width.Equals(actualBitmap.Width) || !expectedBitmap.Height.Equals(actualBitmap.Height))
        {
            return $@"{context.CodePart} dimensions [Width x Height] {context.ShouldMethod.PascalToSpaced()} [{expectedBitmap.Width} x {expectedBitmap.Height}] but were [{actualBitmap.Width} x {actualBitmap.Height}]";
        }

        context.Expected = ExtractPixels(expectedBitmap);
        context.Actual = ExtractPixels(actualBitmap);
        return new ShouldBeMessageGenerator().GenerateErrorMessage(context);
    }

    private static Bitmap? ConvertToBitmap(byte[] imageBytes)
    {
        try
        {
            using MemoryStream stream = new MemoryStream(imageBytes);
            Bitmap bitmap = new Bitmap(stream);
            return bitmap;

        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private static byte[][] ExtractPixels(Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        byte[][] pixelData = new byte[height * width][];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = bitmap.GetPixel(x, y);
                int argbValue = pixel.ToArgb();
                byte[] convertedPixel = BitConverter.GetBytes(argbValue);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(convertedPixel);
                }

                pixelData[y * width + x] = convertedPixel;
            }
        }
        return pixelData;
    }

    private static string GetImageName(ImageFormat format)
    {
        if (format == ImageFormat.Jpeg) return "JPEG";
        if (format == ImageFormat.Bmp) return "BMP";
        if (format == ImageFormat.Png) return "PNG";
        if (format == ImageFormat.Emf) return "EMF";
        if (format == ImageFormat.Exif) return "EXIF";
        if (format == ImageFormat.Gif) return "GIF";
        if (format == ImageFormat.Icon) return "ICON";
        if (format == ImageFormat.MemoryBmp) return "MEMORY_BMP";
        if (format == ImageFormat.Tiff) return "TIFF";
        return "Wmf";
    }
}


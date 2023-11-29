using System.Drawing;

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
        try
        {
            var codePart = context.CodePart;
            byte[] expectedBytes = (byte[])context.Expected;
            byte[] actualBytes = (byte[])context.Actual;
            context.Expected = ExtractPixels(expectedBytes);
            context.Actual = ExtractPixels(actualBytes);
            return new ShouldBeMessageGenerator().GenerateErrorMessage(context);
        }
        catch (Exception)
        {
            return new ShouldBeMessageGenerator().GenerateErrorMessage(context);
        }
    }

    private static byte[][] ExtractPixels(byte[] imageBytes)
    {
        using MemoryStream stream = new MemoryStream(imageBytes);
        using Bitmap bitmap = new Bitmap(stream);
        int width = bitmap.Width;
        int height = bitmap.Height;

        byte[][] pixelData = new byte[height*width][];
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
                pixelData[x*width+y] = convertedPixel;
            }
        }
        return pixelData;
    }
}


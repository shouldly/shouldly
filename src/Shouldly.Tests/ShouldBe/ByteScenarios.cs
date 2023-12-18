
using System.Drawing;
using System.Drawing.Imaging;

namespace Shouldly.Tests.ShouldBe;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1416:...", Justification = "Explanation for suppression.")]
public class ByteScenarios
{
   [Fact]
    public void ImageBytes2x2ShouldBeEqual()
    {
        byte[] expectedImage = GenerateImage(2, 2, new Color[]{ Color.Red, Color.Green, Color.Blue, Color.Blue}, ImageFormat.Jpeg);
        byte[] actualImage = (byte[])expectedImage.Clone();
        expectedImage.ShouldBe(actualImage);
    }

    [Fact]
    public void ImageBytes2x2ShouldNotBeEqual()
    {
        byte[] expectedImage = GenerateImage(2, 2, new Color[] { Color.Red, Color.Red, Color.Blue, Color.White }, ImageFormat.Png);
        byte[] actualImage = GenerateImage(2,2, new Color[] { Color.White, Color.White, Color.White, Color.White }, ImageFormat.Png);
        Should.Throw<ShouldAssertException>(
            () =>expectedImage.ShouldBe(actualImage))
            .Message.ShouldBe(
             """
expectedImage
    should be
[[255, 255, 255, 255], [255, 255, 255, 255], [255, 255, 255, 255], [255, 255, 255, 255]]
    but was
[[255, 255, 0, 0], [255, 255, 0, 0], [255, 0, 0, 255], [255, 255, 255, 255]]
    difference
[*[255, 255, 0, 0]*, *[255, 255, 0, 0]*, *[255, 0, 0, 255]*, [255, 255, 255, 255]]
"""
);
    }

    [Fact]
    public void ImageBytesDifferentSizeShouldNotBeEqual()
    {
        byte[] expectedImage = GenerateImage(2, 3, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.Black, Color.Black }, ImageFormat.Png);
        byte[] actualImage = GenerateImage(3, 2, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.Black, Color.Black }, ImageFormat.Png);
        Should.Throw<ShouldAssertException>(
            () => expectedImage.ShouldBe(actualImage))
            .Message.ShouldBe(
             """
expectedImage dimensions [Width x Height] should be [3 x 2] but were [2 x 3]
"""
);
    }

    [Fact]
    public void ImageBytesDifferentFormatShouldNotBeEqual()
    {
        byte[] expectedImage = GenerateImage(2, 3, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.Black, Color.Black }, ImageFormat.Png);
        byte[] actualImage = GenerateImage(2, 3, new Color[] { Color.White, Color.White, Color.White, Color.White, Color.Black, Color.Black }, ImageFormat.Jpeg);
        Should.Throw<ShouldAssertException>(
            () => expectedImage.ShouldBe(actualImage))
            .Message.ShouldBe(
             """
expectedImage image format should be Jpeg but was Png
"""
);
    }

    [Fact]
    public void RegularBytesThrowMessageTest()
    {
        byte[] expected = { 1, 1, 0, 1, 0, 0, 0, 0 };
        byte[] actual = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Should.Throw<ShouldAssertException>(
            ()=>expected.ShouldBe(actual))
            .Message.ShouldBe(
            """
expected
    should be
[0, 0, 0, 0, 0, 0, 0, 0, 0]
    but was
[1, 1, 0, 1, 0, 0, 0, 0]
    difference
[*1*, *1*, 0, *1*, 0, 0, 0, 0, *]
"""
);
    }




    private static byte[] GenerateImage(int width, int height, Color[] pixels, ImageFormat format)
    {
        using Bitmap bitmap = new Bitmap(width, height);

        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bitmap.SetPixel(x, y, pixels[index]);
                index++;
            }
        }

        using MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, format);
        return stream.ToArray();
    }

}

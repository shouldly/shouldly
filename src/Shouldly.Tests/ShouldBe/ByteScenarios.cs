
using System.Drawing;
using System.Drawing.Imaging;

namespace Shouldly.Tests.ShouldBe;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1416:...", Justification = "Explanation for suppression.")]
public class ByteScenarios
{
   [Fact]
    public void ImageBytesShouldBeEqual()
    {
        byte[] expectedImage = Generate2x2Image(Color.Red, Color.Green, Color.Blue, Color.Blue, ImageFormat.Jpeg);
        byte[] actualImage = (byte[])expectedImage.Clone();
        expectedImage.ShouldBe(actualImage);
    }

    [Fact]
    public void ImageBytesShouldNotBeEqual()
    {
        byte[] expectedImage = Generate2x2Image(Color.Red, Color.Green, Color.Blue, Color.Blue, ImageFormat.Png);
        byte[] actualImage = Generate2x2Image(Color.White, Color.White, Color.White, Color.White, ImageFormat.Png);
        Should.Throw<ShouldAssertException>(
            () =>expectedImage.ShouldBe(actualImage),
            customMessage:
             """
Shouldly.ShouldAssertException : expectedImage
    should be
[[255, 255, 255, 255], [255, 255, 255, 255], [255, 255, 255, 255], [255, 255, 255, 255]]
    but was
[[255, 255, 0, 0], [255, 0, 0, 255], [255, 0, 128, 0], [255, 0, 0, 255]]
    difference
[*[255, 255, 0, 0] *, *[255, 0, 0, 255] *, *[255, 0, 128, 0] *, *[255, 0, 0, 255] *]
"""
);
    }

[Fact]
    public void RegularBytesThrowMessageTest()
    {
        byte[] expected = { 1, 1, 0, 1, 0, 0, 0, 0 };
        byte[] actual = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Should.Throw<ShouldAssertException>(()=>expected.ShouldBe(actual),
            customMessage:
            """
Shouldly.ShouldAssertException : expected
    should be
[0, 0, 0, 0, 0, 0, 0, 0, 0]
    but was
[1, 1, 0, 1, 0, 0, 0, 0]
    difference
[*1*, *1*, 0, *1*, 0, 0, 0, 0, *]
"""
);
    }




    private static byte[] Generate2x2Image(Color pixel1, Color pixel2, Color pixel3, Color pixel4, ImageFormat format)
    {
        using Bitmap bitmap = new Bitmap(2, 2);
        bitmap.SetPixel(0, 0, pixel1);
        bitmap.SetPixel(1, 0, pixel2);
        bitmap.SetPixel(0, 1, pixel3);
        bitmap.SetPixel(1, 1, pixel4);
        using MemoryStream stream = new MemoryStream();
        bitmap.Save(stream, format);
        byte[] byteArray = stream.ToArray();
        return byteArray;
    }
    
}

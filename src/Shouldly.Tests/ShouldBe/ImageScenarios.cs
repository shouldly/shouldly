namespace Shouldly.Tests.ShouldBe;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1416:...", Justification = "Explanation for suppression.")]
public class ImageScenarios
{
    [Fact]
    public void ImageBytes2x2ShouldBeEqual()
    {
        byte[] expectedImage = LoadImage("PNG_2x2_RRBW.png");
        byte[] actualImage = (byte[])expectedImage.Clone();
        expectedImage.ShouldBe(actualImage);
    }

    [Fact]
    public void ImageBytes2x2ShouldNotBeEqual()
    {
        byte[] expectedImage = LoadImage("PNG_2x2_RRBW.png");
        byte[] actualImage = LoadImage("PNG_2x2_WWWW.png");
        Should.Throw<ShouldAssertException>(
            () => expectedImage.ShouldBe(actualImage))
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

        byte[] expectedImage = LoadImage("PNG_2x3_WWWWBB.png");
        byte[] actualImage = LoadImage("PNG_3x2_WWWWBB.png");
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
        byte[] expectedImage = LoadImage("PNG_2x3_WWWWBB.png");
        byte[] actualImage = LoadImage("JPEG_2x3_WWWWBB.jpeg");
        Should.Throw<ShouldAssertException>(
            () => expectedImage.ShouldBe(actualImage))
            .Message.ShouldBe(
             """
expectedImage image format should be JPEG but was PNG
"""
);
    }

    [Fact]
    public void RegularBytesThrowMessageTest()
    {
        byte[] expected = { 1, 1, 0, 1, 0, 0, 0, 0 };
        byte[] actual = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        Should.Throw<ShouldAssertException>(
            () => expected.ShouldBe(actual))
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




    private static byte[] LoadImage(string fileName)
    {

        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", fileName);
        try
        {
            byte[] content = File.ReadAllBytes(path);
            return content;
        }
        catch (IOException ex)
        {
            Assert.Fail(ex.Message);
            return null;
        }
    }

}

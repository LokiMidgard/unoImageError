using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

public static class ImageHelper
{
    public static async Task<ImageSource> GenerateSource()
    {
        var picker = new Windows.Storage.Pickers.FileOpenPicker();
        picker.FileTypeFilter.Add(".png");
        var pickerResult = await picker.PickSingleFileAsync("tileset");
        if (pickerResult is null)
            return null;
        using var stream = await pickerResult.OpenReadAsync();

        string imagePath = Path.GetFullPath(Path.Combine("cache", "image.png"));
        Directory.CreateDirectory(Path.GetDirectoryName(imagePath));

        using (var bitmapImageStream = File.Open(imagePath,
                                      FileMode.Create,
                                      FileAccess.Write,
                                      FileShare.None))
        {

            await stream.AsStreamForRead().CopyToAsync(bitmapImageStream);
            bitmapImageStream.Flush(true);
        }

        var imgSrc = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
        imgSrc.UriSource = new Uri($"file://{imagePath}");
        var width = imgSrc.PixelHeight;
        return imgSrc;

    }

}
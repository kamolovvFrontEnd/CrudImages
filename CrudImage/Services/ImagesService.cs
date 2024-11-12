using System.Net.Mime;
using CrudImage.Core;
using CrudImage.Database;
using Microsoft.EntityFrameworkCore;

namespace CrudImage.Services;

public class ImagesService(Data data, IWebHostEnvironment hostingEnvironment)
{
    public async Task<List<ImageData>> GetImages()
    {
        List<ImageData> images = await data.Images.ToListAsync();
        return images;
    }

    public async Task<string> ImageUpload(IFormFile file)
    {
        string currentFolder = hostingEnvironment.WebRootPath;
        string fileName = Path.Combine(currentFolder, "Images", file.FileName);
        await using (FileStream stream = File.Create(fileName))
        {
            file.CopyTo(stream);
            ImageData imageData = new()
            {
                ImageString = fileName,
            };
            data.Add(imageData);
            await data.SaveChangesAsync();
        }

        return fileName;
    }

    public async Task<string> UpdateImage(int id, IFormFile file)
    {
        var image = await data.Images.FindAsync(id);
        if (image == null) throw new Exception("Image not found");

        File.Delete(image.ImageString);

        string currentFolder = hostingEnvironment.WebRootPath;
        string imagePath = Path.Combine(currentFolder, "Images", file.FileName);
        await using (FileStream stream = File.Create(imagePath))
        {
            file.CopyTo(stream);
        }

        image.ImageString = imagePath;
        data.Update(image);
        await data.SaveChangesAsync();

        return "Success";
    }

    public async Task<string> DeleteImage(int id)
    {
        var image = await data.Images.FindAsync(id);
        if (image is null) throw new Exception("Image not found");

        File.Delete(image.ImageString);
        data.Remove(image);
        await data.SaveChangesAsync();

        return "Deleted";
    }
}
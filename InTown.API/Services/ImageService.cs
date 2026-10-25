namespace InTown.API.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _env;
    
    //the only file types we accept
    private readonly string [] allowed = {".jpg",  ".jpeg", ".png", ".webp"};
    private const long maxSize = 5 * 1024 * 1024; //5MB
    
    // IWebHostEnvironment knows where wwwroot is on disk
    public ImageService(IWebHostEnvironment env)
    { 
        _env = env;
    }
    
    // returns the path to save in the database, or null if the file is not ok

    public async Task<string?> Save(IFormFile file)
    {
        //check the size
        if (file.Length == 0 || file.Length > maxSize)
            return null;
        
        //check the type
        string ext = Path.GetExtension(file.FileName).ToLower();
        if(!allowed.Contains(ext))
            return null;
        
        //give it a random name so two files never clash
        string fileName = Guid.NewGuid().ToString() + ext;
        string folder = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(folder);
        string fullPath = Path.Combine(folder, fileName);
        
        //cpåy the uploaded bytes into the new file
        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        return "/uploads/" + fileName;

    }
    
}
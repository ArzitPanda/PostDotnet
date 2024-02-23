namespace SlackApi.Utils
{
    public class ImageUploadUtils
    {

        public string _baseURL;


        public ImageUploadUtils(string baseURL)
        {
            _baseURL = baseURL;
            
        }


      

        public  string UploadImage(IFormFile photo)
        {
           
                // Check if photo is provided
                if (photo != null && photo.Length > 0)
                {
                    // Generate unique filename for the photo
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                 

                // Save the file to the server
                var directoryPath = Path.Combine("wwwroot", "profile");
                var filePath = Path.Combine(directoryPath, fileName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }

                // Return the file path
                var imageUrl = $"{_baseURL}/profile/{fileName}";

                // Return the image URL
                return imageUrl;
            }
                else
                {
                    // If photo is not provided, return null or throw an exception
                    throw new ArgumentException("No photo provided.");
                }
            }
        
        }
    

}

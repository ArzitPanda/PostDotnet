using System.ComponentModel.DataAnnotations;

namespace SlackApi.Data.Dto.RequestDto
{
    public class AddPostDto
    {

    
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }


        public IFormFile  Photo { get; set; }
        public string[] Visibility { get; set; }
        public long AuthorId { get; set; }
    }
}


public class ValidVisibilityAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var validValues = new List<string> { "family", "friends", "Office", "public" };
        if (!validValues.Contains(value.ToString().ToLower()))
        {
            return new ValidationResult($"The {validationContext.DisplayName} field must be one of the following values: Family, Friends, Office, Public.");
        }

        return ValidationResult.Success;
    }
}

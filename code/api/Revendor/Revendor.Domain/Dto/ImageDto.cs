using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Dto
{
    public class ImageDto
    {
        public ImageDto()
        {
            
        }

        public ImageDto(Image image)
        {
            Name = image.Name;
            Path = image.Path;
        }

        public string Path { get; set; }
        public string Name { get; set; }
    }
}
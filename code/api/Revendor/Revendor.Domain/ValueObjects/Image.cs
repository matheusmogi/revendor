using System.Collections.Generic;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;

namespace Revendor.Domain.ValueObjects
{
    public class Image : ValueObject
    {
        protected Image()
        {
            
        }
        public Image(ImageDto imageDto)
        {
            Name = imageDto.Name;
            Path = imageDto.Path;
        }
        public string Name { get; set; }
        public string Path { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return $"{Name} {Path}";
        }
    }
}
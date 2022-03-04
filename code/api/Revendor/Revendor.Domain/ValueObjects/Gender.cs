using System.Collections.Generic;
using Revendor.Domain.Common;

namespace Revendor.Domain.ValueObjects
{
    public class Gender : ValueObject
    {
        static Gender(){}
        private Gender(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
 
        public static Gender Male => new Gender("Male");
        public static Gender Female => new Gender("Female");
        public static Gender Other => new Gender("Other");
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
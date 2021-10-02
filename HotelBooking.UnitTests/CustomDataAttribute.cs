using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace HotelBooking.UnitTests
{
    public class CustomDataAttribute : DataAttribute
    {
        private string _path;
        public CustomDataAttribute(string path)
        {
            _path = path;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var fileData = File.ReadAllText(_path);
            return JsonConvert.DeserializeObject<List<object[]>>(fileData);
        }
    }
}

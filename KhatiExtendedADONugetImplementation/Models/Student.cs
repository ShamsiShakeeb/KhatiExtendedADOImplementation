using Newtonsoft.Json;

namespace KhatiExtendedADONugetImplementation.Models
{
    public class Student
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string? Name { get; set; }
    }
}

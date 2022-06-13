using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MockingU.Data
{
    public class ApiTemplate
    {
        public int Id { get; set; }

        [Required]
        public string UrlPattern { get; set; } = "^/api/v1/resource/?$";

        public List<string> Methods { get; set; } = new List<string> { "GET", "POST" };

        public ResponseTemplate Response { get; set; } = new ResponseTemplate();

        [JsonIgnore]
        public IdentityUser User { get; set; }

        [JsonIgnore]
        [Required]
        public string UserId { get; set; }

    }

    [Owned]
    public class ResponseTemplate
    {
        public List<string> Contents { get; set; } = new List<string>();

        [Required]
        public string ContentType { get; set; } = "application/json";

        public int StatusCode { get; set; } = 200;
    }
}

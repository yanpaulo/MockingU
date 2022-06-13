using MockingU.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MockingU.Services
{
    public class MockingService
    {
        private readonly Random _rng = new();
        private readonly ApplicationDbContext _db;

        public MockingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task RouteAsync(HttpContext context)
        {
            
            var path = context.Request.Path.ToString().Remove(0, 1);
            if (!path.EndsWith('/'))
            {
                path += "/";
            }

            var userName = path.Substring(0, path.IndexOf('/'));

            if (string.IsNullOrEmpty(userName))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            path = path.Remove(0, userName.Length);


            var template = _db.ApiTemplates
                .Select(e => new { e.Id, e.UrlPattern, e.Methods, e.User.UserName })
                .ToList()
                .Where(e => 
                    e.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase) && 
                    Regex.IsMatch(path, e.UrlPattern) && 
                    e.Methods.Contains(context.Request.Method))
                .Select(e => _db.ApiTemplates.Find(e.Id))
                .SingleOrDefault();

            if (template == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }



            context.Response.StatusCode = template.Response.StatusCode;
            context.Response.ContentType = template.Response.ContentType;
            if (template.Response.Contents.Any())
            {
                var content = template.Response.Contents[_rng.Next(template.Response.Contents.Count)];
                await context.Response.WriteAsync(content); 
            }
        }
    }
}

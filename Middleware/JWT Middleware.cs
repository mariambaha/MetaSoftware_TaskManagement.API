using System.IdentityModel.Tokens.Jwt;

namespace MetaSoftware_TaskManagement.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"]
                               .FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == "userId");

                if (userIdClaim != null)
                    context.Items["UserId"] = int.Parse(userIdClaim.Value);
            }

            await _next(context);
        }
    }
}

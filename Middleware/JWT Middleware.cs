using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                context.Items["UserId"] = jwt.Claims
                    .First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            }

            await _next(context);
        }
    }

}

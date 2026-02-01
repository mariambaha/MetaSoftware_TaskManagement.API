namespace MetaSoftware_TaskManagement.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private static Dictionary<string, (int Count, DateTime Time)> _requests = new();
        private readonly RequestDelegate _next;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = context.Connection.RemoteIpAddress.ToString();

            if (_requests.ContainsKey(key))
            {
                var data = _requests[key];
                if ((DateTime.UtcNow - data.Time).TotalMinutes < 1)
                {
                    if (data.Count >= 5)
                    {
                        context.Response.StatusCode = 429;
                        await context.Response.WriteAsync("Too many requests");
                        return;
                    }
                    _requests[key] = (data.Count + 1, data.Time);
                }
                else
                    _requests[key] = (1, DateTime.UtcNow);
            }
            else
                _requests[key] = (1, DateTime.UtcNow);

            await _next(context);
        }
    }

}

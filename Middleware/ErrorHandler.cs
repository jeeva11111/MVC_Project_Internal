namespace BackEnd.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
       public ErrorHandler(RequestDelegate next)
       {
        _next = next;   
       } 

       public async Task Invoke (HttpContext context){
        
       }
    }
}
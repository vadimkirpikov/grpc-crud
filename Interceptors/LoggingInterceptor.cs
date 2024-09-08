using Grpc.Core;
using Grpc.Core.Interceptors;

namespace CrudAppGrpc.Interceptors;

public class LoggingInterceptor(ILogger<LoggingInterceptor> logger): Interceptor
{

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        logger.LogInformation("Handling gRPC call: {Method}", context.Method);
        logger.LogInformation("Request: {Request}", request);

        var response = await continuation(request, context);

        logger.LogInformation("Response: {Response}", response);

        return response;
    }
}
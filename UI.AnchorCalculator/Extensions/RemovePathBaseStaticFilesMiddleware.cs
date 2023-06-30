using Microsoft.AspNetCore.StaticFiles;

namespace UI.AnchorCalculator.Extensions
{
    public class RemovePathBaseStaticFilesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly StaticFileMiddleware _staticFileMiddleware;

        public RemovePathBaseStaticFilesMiddleware(RequestDelegate next, StaticFileMiddleware staticFileMiddleware)
        {
            _next = next;
            _staticFileMiddleware = staticFileMiddleware;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalPathBase = context.Request.PathBase;

            if (!string.IsNullOrEmpty(originalPathBase))
            {
                context.Request.PathBase = PathString.Empty;
            }

            // Проверяем, является ли текущий запрос ссылкой на статический файл
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            var isStaticFile = contentTypeProvider.TryGetContentType(context.Request.Path, out var _);

            if (isStaticFile)
            {
                // Удаляем базовый путь из сгенерированной ссылки на статический файл
                var originalPath = context.Request.Path;
                context.Request.Path = originalPath.Value.Replace(originalPathBase, PathString.Empty);
                // Восстанавливаем оригинальные значения базового пути и пути запроса
                context.Request.PathBase = originalPathBase;
                context.Request.Path = originalPath;
            }
            await _staticFileMiddleware.Invoke(context);
            await _next(context);
        }
    }
}

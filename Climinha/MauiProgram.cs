using Microsoft.Extensions.Logging;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
using Microsoft.Maui.Handlers;


namespace Climinha
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
                    // Remover sublinhado do Entry
                    EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                    {
#if ANDROID
                        handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
                        handler.PlatformView.Background = null; // Remove qualquer fundo adicional
#elif IOS
                        handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
                        handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
                    });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

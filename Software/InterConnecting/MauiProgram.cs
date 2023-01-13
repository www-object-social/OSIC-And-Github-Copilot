﻿using InterConnecting.Data;
using Microsoft.Extensions.Logging;

namespace InterConnecting
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>();
            builder.Services.AddMauiBlazorWebView();
            #if DEBUG
		        builder.Services.AddBlazorWebViewDeveloperTools();
		        builder.Logging.AddDebug();
            #endif
            return builder.Build();
        }
    }
}
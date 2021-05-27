using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using MyBestApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MyBestApp.Middleware
{
    public static class LocalizationMiddleware
    {
        public static IServiceCollection AddCustomlLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddRazorPages()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            return services;
        }

        public static IApplicationBuilder UseCutomLocalization(this IApplicationBuilder app)
        {
            SupportedCultures.CultureList = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };
            var localizatonOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US", "en-US"),
                SupportedCultures = SupportedCultures.CultureList,
                SupportedUICultures = SupportedCultures.CultureList
            };
            app.UseRequestLocalization(localizatonOptions);
            return app;
        }
    }
}

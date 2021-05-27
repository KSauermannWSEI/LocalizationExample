using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MyBestApp.Data;
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
        public static IServiceCollection AddDbLocalization(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IStringLocalizer<SharedResource>, LocalizerFromDb<SharedResource>>();
            services.AddScoped<IHtmlLocalizer<SharedResource>, LocalizerFromDb<SharedResource>>();
            return services;
        }

        public static IApplicationBuilder UseCutomLocalization(this IApplicationBuilder app)
        {
            SupportedCultures.CultureList = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pl-PL")
                };
            app.useLocalizationOptions();
            return app;
        }
        public static IApplicationBuilder UseDbLocalization(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            var localizationContext = serviceProvider.GetService<LocalizationContext>();
            var cultures = localizationContext.Translations.Select(a => a.Culture).Distinct().ToList();
            SupportedCultures.CultureList = cultures.Select(a => new CultureInfo(a)).ToList();
            app.useLocalizationOptions();
            return app;
        }

        private static void useLocalizationOptions(this IApplicationBuilder app)
        {
            var localizatonOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US", "en-US"),
                SupportedCultures = SupportedCultures.CultureList,
                SupportedUICultures = SupportedCultures.CultureList
            };
            app.UseRequestLocalization(localizatonOptions);
        }
    }
}

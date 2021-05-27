using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using MyBestApp.Data;
using MyBestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MyBestApp.Helpers
{
    public class LocalizerFromDb<T> : IStringLocalizer<T>, IHtmlLocalizer<T>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LocalizationContext localizationContext;
        private string culture;
        public LocalizerFromDb(IHttpContextAccessor httpContextAccessor, LocalizationContext localizationContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.localizationContext = localizationContext;
            var cookie = httpContextAccessor.HttpContext.Request.Cookies[".AspNetCore.Culture"] ?? "default";
            culture = cookie.Split("|", StringSplitOptions.RemoveEmptyEntries).First().Replace("c=", "");
        }

        private List<Translation> list
        {
            get
            {
                if (httpContextAccessor.HttpContext.Session.GetString(culture) == null)
                {
                    var cultures = localizationContext.Translations.Select(a => a.Culture).Distinct().ToList();
                    SupportedCultures.CultureList = cultures.Select(a => new CultureInfo(a)).ToList();
                    var query = localizationContext.Translations.Where(a => a.Culture == culture).AsNoTracking();
                    httpContextAccessor.HttpContext.Session.SetString(culture, JsonConvert.SerializeObject(query.ToList()));
                }
                return JsonConvert.DeserializeObject<List<Translation>>(httpContextAccessor.HttpContext.Session.GetString(culture));
            }
        }
        private string getValue(string name)
        {
            var item = list.Find(a => a.Name == name);
            return item != null ? item.Value : name;
        }
        private string getValue(string name, params object[] arguments)
        {
            var item = list.Find(a => a.Name == name);
            return item != null ? string.Format(item.Value, arguments) : string.Format(name, arguments);
        }
        public LocalizedString this[string name] => new(name, getValue(name));

        public LocalizedString this[string name, params object[] arguments] => new(name, getValue(name, arguments));

        LocalizedHtmlString IHtmlLocalizer.this[string name] => new(name, getValue(name));

        LocalizedHtmlString IHtmlLocalizer.this[string name, params object[] arguments] => new(name, getValue(name, arguments));



        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return list.Select(a => new LocalizedString(a.Name, a.Value));
        }

        public LocalizedString GetString(string name) => new(name, getValue(name));


        public LocalizedString GetString(string name, params object[] arguments) => new(name, getValue(name, arguments));
    }
}

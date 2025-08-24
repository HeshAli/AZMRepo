using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AZMAdmin.CMS.Localization
{
    public static class CMSLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(CMSConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(CMSLocalizationConfigurer).GetAssembly(),
                        "AZMAdmin.CMS.Localization.SourceFiles"
                    )
                )
            );
            localizationConfiguration.Languages.Add(new LanguageInfo("ar", "Arabic", isDefault: true));
        }
    }
}

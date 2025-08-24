using AZMAdmin.CMS.Debugging;

namespace AZMAdmin.CMS
{
    public class CMSConsts
    {
        public const string LocalizationSourceName = "CMS";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8e804fa2f79f44b381597ecda0088c8b";
    }
}

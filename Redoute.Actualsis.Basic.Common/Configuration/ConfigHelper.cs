
using System;
using System.Collections.Generic;
using System.Text;

namespace Redoute.Actualsis.Basic.Common
{
   public class ConfigHelper
    {
        private static ConnectionStrings _ConnectionStrings;
        public static ConnectionStrings connectionStrings
        {
            get
            {
                if (_ConnectionStrings == null)
                {
                    _ConnectionStrings = ConfigurationManager.GetAppSettings<ConnectionStrings>("ConnectionStrings");
                }
                return _ConnectionStrings;
            }
        }

        //private static DownloadConfig _DownloadConfig;
        //public static DownloadConfig downloadConfig
        //{
        //    get
        //    {
        //        if (_DownloadConfig == null)
        //        {
        //            _DownloadConfig = ConfigurationManager.GetAppSettings<DownloadConfig>("DownloadConfig");
        //        }
        //        return _DownloadConfig;
        //    }
        //}

    }
}

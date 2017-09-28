using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mDiagnostics.Common
{
    public class SaveStateTest
    {
        public void savestate(bool d, string nametest)
        {
            Windows.Storage.ApplicationDataContainer roamingSettings =
                Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (d)
            {
                roamingSettings.Values[nametest] = "тест пройден";
            }
            else
            {
                roamingSettings.Values[nametest] = "тест не пройден";
            }
        }
    }
}

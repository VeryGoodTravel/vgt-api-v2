using System.Security.Cryptography;
using System.Text;

namespace vgt_api.Models.Common;

public class Maintenance
{
    private static readonly string[] Maintenances =
    {
        "All Inclusive",
        "Breakfast",
        "No maintenance"
    };

    public static double GetMaintenanceModifier(string maintenance)
    {
        if (Maintenances[0].Equals(maintenance))
        {
            return 1.0;
        }
        else if (Maintenances[1].Equals(maintenance))
        {
            return 0.9;
        }
        else if (Maintenances[2].Equals(maintenance))
        {
            return 0.8;
        }
        else
        {
            return 1.0;
        }
    }
    
    public static List<string> RandomizeMaintenances(string name)
    {
        using (MD5 md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            var rnd = BitConverter.ToInt32(hash, 0) % 7 + 1;
            List<string> ret = new List<string>();
            if ((rnd & 1) > 0)
            {
                ret.Add(Maintenances[0]);
            }

            if ((rnd & 2) > 0)
            {
                ret.Add(Maintenances[1]);
            }

            if ((rnd & 4) > 0)
            {
                ret.Add(Maintenances[2]);
            }

            return ret;
        }
    }
}
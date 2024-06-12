using System.Security.Cryptography;
using System.Text;

namespace vgt_api.Models.Common;

public class Rating
{
    private static readonly double MinRating = 1.0;
    private static readonly double MaxRating = 5.0;
    
    public static double RandomizeRating(string name)
    {
        using (MD5 md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(name));
            var seed = BitConverter.ToInt32(hash, 0);
            Random random = new Random(seed);
            return random.NextDouble() * (MaxRating - MinRating) + MinRating;
        }
    }

    public static double CombineRandomizeRating(string[] names)
    {
        var total = 0.0;
        foreach (var name in names)
        {
            total += RandomizeRating(name);
        }
        return total / names.Length;
    }
}
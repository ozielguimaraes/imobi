using System.Globalization;
using System.Threading;

namespace Imobi.Globalization
{
    public static class AppCulture
    {
        public static CultureInfo Get() => Thread.CurrentThread.CurrentCulture;
    }
}
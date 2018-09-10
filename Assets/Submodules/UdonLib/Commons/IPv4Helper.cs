using System.Net;

namespace UdonLib.Commons
{
    public static class IPv4Helper
    {
        public static string GetIP()
        {
            string ip = "";
            string hostName = Dns.GetHostName();    // 自身のホスト名を取得
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);

            foreach (IPAddress address in addresses)
            {
                // IPv4判定
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = address.ToString();
                }
            }

            return ip;
        }
    }
}
using System;
using System.Net;

namespace Hime.Structures
{
    public struct ServerConfig
    {
        public IPAddress IP { get; set; }

        public ushort Port { get; set; }
    }
}

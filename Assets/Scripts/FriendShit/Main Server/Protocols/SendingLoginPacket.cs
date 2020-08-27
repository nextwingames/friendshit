using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class SendingLoginPacket:Packet
        {
            public string Id;
            public string Pw;

            public SendingLoginPacket(string id, string pw)
            {
                Id = id;
                Pw = pw;
            }
        }
    }
}
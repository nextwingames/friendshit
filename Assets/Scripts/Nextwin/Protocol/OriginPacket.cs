namespace Nextwin
{
    namespace Protocol
    {
        public class OriginPacket
        {
            public Header Header { get; }
            public byte[] Data { get; }

            public OriginPacket(Header header, byte[] data)
            {
                Header = header;
                Data = data;
            }
        }
    }
}
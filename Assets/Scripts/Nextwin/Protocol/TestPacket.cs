namespace Nextwin
{
    namespace Protocol
    {
        public class TestPacket
        {
            public int Data;
            public string Str;

            public TestPacket(int data, string str)
            {
                Data = data;
                Str = str;
            }
        }
    }
}
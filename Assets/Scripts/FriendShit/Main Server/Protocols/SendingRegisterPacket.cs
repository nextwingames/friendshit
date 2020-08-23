namespace Friendshit
{
    namespace Protocols
    {
        public class SendingRegisterPacket
        {
            public string Nickname;
            public string Id;
            public string Pw;
            public string Mail;

            public SendingRegisterPacket(string nickname, string id, string pw, string mail)
            {
                Nickname = nickname;
                Id = id;
                Pw = pw;
                Mail = mail;
            }
        }
    }
}


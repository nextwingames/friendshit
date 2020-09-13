namespace Friendshit
{
    namespace Protocols
    {
        /// <summary>
        /// 헤더 번호 모음 (16 ~ 30만 사용)
        /// </summary>
        public partial class Protocol
        {
            public const int Register = 16;
            public const int Login = 17;
            public const int Lobby = 18;
            public const int LobbyChat = 19;
            public const int CreateRoom = 20;
            public const int EnterRoom = 21;
            public const int ExitRoom = 22;
        }
    }
}

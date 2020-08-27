using Nextwin.Net;
using Nextwin.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    namespace Services
    {
        public abstract class Service
        {
            protected NetworkManager _networkManager;
            protected Packet _packet;

            protected Service(Packet packet)
            {
                _networkManager = NetworkManager.Instance;
                _packet = packet;
            }

            public abstract void Execute();
        }
    }
}

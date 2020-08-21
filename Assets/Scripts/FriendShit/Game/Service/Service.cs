using Nextwin.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    namespace Game
    {
        public abstract class Service
        {
            protected NetworkManager _networkManager;

            protected Service()
            {
                _networkManager = NetworkManager.Instance;
            }

            public abstract void Execute();
        }
    }
}

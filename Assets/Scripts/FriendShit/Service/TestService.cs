using Nextwin.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    namespace Service
    {
        public class TestService : Service
        {
            private TestPacket _testPacket;

            public TestService(TestPacket testPacket)
            {
                _testPacket = testPacket;
            }

            public override void Execute()
            {
                Debug.Log("Data " + _testPacket.Data + ", Str " + _testPacket.Str);
            }
        }
    }
}

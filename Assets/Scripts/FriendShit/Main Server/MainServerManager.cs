using Nextwin.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainServerManager : MonoBehaviour
{
    private NetworkManager _networkManager;

    // Start is called before the first frame update
    void Start()
    {
        _networkManager = NetworkManager.Instance;
        _networkManager.Connect(NetworkManager.MainPort);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

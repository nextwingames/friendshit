using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nextwin.Net;

public class GameManager : MonoBehaviour
{
    private NetworkManager _networkManager;

    private int _frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        _networkManager = NetworkManager.Instance;
        // 서버 없이 확인 시 아래 Connect문 주석
        _networkManager.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        _frame++;
        if(_frame % 300 == 20)
            _networkManager.Send(_frame);
    }

    private void OnApplicationQuit()
    {
        _networkManager.Disconnect();
    }
}

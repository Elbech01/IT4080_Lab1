using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class Main : NetworkBehaviour
{
    public It4080.NetworkSettings netSettings;
    // Start is called before the first frame update
    void Start(){
        netSettings.startServer += NetSettingsOnServerStart;
        netSettings.startHost += NetSettingsOnHostStart;
        netSettings.startClient += NetSettingsOnClientStart;
    }
    private void startClient(IPAddress ip, ushort port) {
        Debug.Log("Start client");
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartClient();
        netSettings.hide();
    }



    //------------------------------------
    // Events
    private void NetSettingsOnServerStart(IPAddress ip, ushort port) {
        Debug.Log("Server started");
    }

    private void NetSettingsOnHostStart(IPAddress ip, ushort port) {
        Debug.Log("Host started");
    }

    private void NetSettingsOnClientStart(IPAddress ip, ushort port) {
        Debug.Log("Client started");
    }


}

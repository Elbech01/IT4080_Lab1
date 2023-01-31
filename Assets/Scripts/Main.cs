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
       // netSettings.setStatusText("Not Connected");
    }
    private void startClient(IPAddress ip, ushort port) {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartClient();
        netSettings.hide();
        Debug.Log("Started client");
    }

    private void startHost(IPAddress ip, ushort port) {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.OnClientConnectedCallback += HostOnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += HostOnClientDisconnected;

        NetworkManager.Singleton.StartHost();
        netSettings.hide();
        Debug.Log("Started host");
    }

    private void startServer(IPAddress ip, ushort port) {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartServer();
        netSettings.hide();
        Debug.Log("Started server");
    }



    // ------------------------------------
    // Events
    private void NetSettingsOnServerStart(IPAddress ip, ushort port) {
        startServer(ip, port);
        Debug.Log("Server started");
        printIs("");
    }

    private void NetSettingsOnHostStart(IPAddress ip, ushort port) {
        startHost(ip, port);
        Debug.Log("Host started");
        printIs("");
    }

    private void NetSettingsOnClientStart(IPAddress ip, ushort port) {
        startClient(ip, port);
        Debug.Log("Client started");
        printIs("");
    }

    private void printIs(string msg) {
        Debug.Log($"[{msg}] Server:{IsServer} Host: {IsHost} Client: {IsClient} Owner: {IsOwner}");
    }

    private void HostOnClientConnected(ulong clientID) {
        Debug.Log($"Client connected: {clientID}");
        
    }

    private void HostOnClientDisconnected(ulong clientID)
    {
        Debug.Log($"Client disconnected: {clientID}");
    }


}

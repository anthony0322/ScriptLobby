using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

namespace Prototype.NetworkLobby
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;
     
     

        public void PopulateEntry(string address, LobbyManager lobbyManager, Color c)
        {
            serverInfoText.text = address;

            slotInfo.text = "01/10";
            
       
            string networkID = address;

            joinButton.onClick.RemoveAllListeners();
            joinButton.onClick.AddListener(() => { JoinMatch(networkID, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        void JoinMatch(string networkID, LobbyManager lobbyManager)
        {
            NetworkManager.singleton.networkAddress = networkID;
            NetworkManager.singleton.networkPort = 7777;
            NetworkManager.singleton.StartClient();
            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();
        }
    }
}
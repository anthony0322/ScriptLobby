using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;
   
        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public void OnEnable()
        {
            lobbyManager.topPanel.ToggleVisibility(true);

        }

        public void OnClickHost()
        {
            lobbyManager.StartHost();   
        }

        public void OnClickJoin()
        {
            lobbyManager.ChangeTo(lobbyPanel);
            lobbyManager.StartClient();
            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }
        
     
        public void OnClickOpenServerList()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
        }
        
    }
}

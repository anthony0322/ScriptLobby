using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
namespace Prototype.NetworkLobby
{
    public class CustomNetworkDiscovery : NetworkDiscoveryEditor
    {
        int ctr = 0;
        public LobbyMainMenu mainMenu;
        public LobbyManager lobbyManager;
        public RectTransform serverListRect;
        public GameObject serverEntryPrefab;
        public GameObject noServerFound;
        bool isThereAServer = false;


        private float timeout = 5f;
        private string data;
        private string fromAddress;
        private string ipAddress;
        private int port;
        public string name = "99999999";

        static Color OddServerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        static Color EvenServerColor = new Color(.94f, .94f, .94f, 1.0f);

        private Dictionary<LanConnectionInfo, float> lanAddresses = new Dictionary<LanConnectionInfo, float>();


        public void OnClickClient()
        {
            
            base.Initialize();     
            base.StartAsClient();
            mainMenu.OnClickOpenServerList();

            if (!isThereAServer)
            {
                noServerFound.SetActive(true);
            }
            else
            {
                noServerFound.SetActive(false);
            }
        }

        public void OnClickServer()
        {
           if(running)
                StopBroadcast();
            base.Initialize();
            base.StartAsServer();
            print("server created");
            mainMenu.OnClickHost();
            
        }

        public void StopBroadcasting()
        {
            if (running)
                StopBroadcast();
        }
 

        public override void OnReceivedBroadcast(string fromAddress, string data)
        {
            ctr++;
            isThereAServer = true;
            base.OnReceivedBroadcast(fromAddress, data);
            if (name != fromAddress)
            {
                this.data = data;
                this.fromAddress = fromAddress;
            }
           
            noServerFound.SetActive(false);
            if(name != fromAddress)
                OnJoinButton(fromAddress, data);

        }

      

        public void OnJoinButton(string fromAddress, string data)
        {

            ipAddress = fromAddress.Substring(fromAddress.LastIndexOf(":") + 1, fromAddress.Length - (fromAddress.LastIndexOf(":") + 1));
            string portText = data.Substring(data.LastIndexOf(":") + 1, data.Length - (data.LastIndexOf(":") + 1));
            port = 7777;
            int.TryParse(portText, out port);

          
            name = fromAddress;
            GameObject o = Instantiate(serverEntryPrefab) as GameObject;
            o.GetComponent<LobbyServerEntry>().PopulateEntry(ipAddress, lobbyManager, (ctr % 2 == 0) ? OddServerColor : EvenServerColor);
            o.transform.SetParent(serverListRect, false);

            
            
           
        }






    }
}
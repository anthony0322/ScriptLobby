﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    public class LobbyServerList : MonoBehaviour
    {
        public LobbyManager lobbyManager;
    
        public RectTransform serverListRect;
        public GameObject serverEntryPrefab;
        public GameObject noServerFound;
      

        protected int currentPage = 0;
        protected int previousPage = 0;

        static Color OddServerColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        static Color EvenServerColor = new Color(.94f, .94f, .94f, 1.0f);
        

 

        void OnEnable()
        {
            
            currentPage = 0;
            previousPage = 0;

            foreach (Transform t in serverListRect)
                Destroy(t.gameObject);

            noServerFound.SetActive(false);

            RequestPage(0);
        }
  
        public void ChangePage(int dir)
        {
            int newPage = Mathf.Max(0, currentPage + dir);

            //if we have no server currently displayed, need we need to refresh page0 first instead of trying to fetch any other page
            if (noServerFound.activeSelf)
                newPage = 0;

            RequestPage(newPage);
        }

        public void RequestPage(int page)
        {
            previousPage = currentPage;
            currentPage = page;
			//lobbyManager.matchMaker.ListMatches(page, 6, "", true, 0, 0, OnGUIMatchList);
		}
    }
}
using Project.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace NodeNetwork {
    public class NetworkSerialize : MonoBehaviour {
        [SerializeField]
        [GreyOut]
        private string id;
        [SerializeField]
        [GreyOut]
        private bool isControlling;

        private SocketIOComponent socket;

        public void Awake() {
            isControlling = false;
        }

        public void SetControllerId(string id) {
            this.id = id;
            this.isControlling = (NetworkClient.clientId == id) ? true : false;
            
            //mudar cor do player cliente, amarelo caso for cliente, branco caso for outros players
            GetComponent<SpriteRenderer>().color = (NetworkClient.clientId == id) ? Color.yellow : Color.white;
        }

        public void SetSocketReference(SocketIOComponent socket) {
            this.socket = socket;
        }

        public bool IsControlling() {
            return isControlling;
        }

        public SocketIOComponent GetSocket() {
            return socket;
        }
    }
}
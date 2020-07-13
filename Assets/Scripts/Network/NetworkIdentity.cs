using NodeNetwork.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace NodeNetwork {
    public class NetworkIdentity : MonoBehaviour {
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
            isControlling = (NetworkClient.clientId == id) ? true : false;
        }

        public void SetSocketReference(SocketIOComponent Socket){
            socket = Socket;
        }

        public string GetID(){
            return id;
        }

        public bool IsControlling(){
            return isControlling;
        }

        public SocketIOComponent GetSocket(){
            return socket;
        }
    }
}
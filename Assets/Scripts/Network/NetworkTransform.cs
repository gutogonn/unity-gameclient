using NodeNetwork.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeNetwork {
    [RequireComponent(typeof(NetworkIdentity))]
    public class NetworkTransform : MonoBehaviour {
        [SerializeField]
        [GreyOut]
        private Vector3 lastPosition;

        private NetworkIdentity networkIdentity;
        private NetworkObject networkObject;

        private float sendDataCD = 0;

        public void Start() {
            networkIdentity = GetComponent<NetworkIdentity>();
            lastPosition = transform.position;
            networkObject = new NetworkObject();
            networkObject.position = new Vector2(0, 0);

            if (!networkIdentity.IsControlling()) {
                enabled = false;
            }
        }

        public void Update() {
            if (networkIdentity.IsControlling()) {
                if (lastPosition != transform.position) {
                    lastPosition = transform.position;
                    sendDataCD = 0;
                    sendData();
                } else {
                    sendDataCD += Time.deltaTime;

                    if (sendDataCD >= 1) {
                        sendDataCD = 0;
                        sendData();
                    }
                }
            }
        }

        private void sendData() {
            networkObject.position.x = Mathf.Round(transform.position.x * 1000.0f) / 1000.0f;
            networkObject.position.y = Mathf.Round(transform.position.y * 1000.0f) / 1000.0f;
            networkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(networkObject)));
        }
    }
}
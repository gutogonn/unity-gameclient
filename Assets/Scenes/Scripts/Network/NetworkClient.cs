using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace NodeNetwork {
    public class NetworkClient : SocketIOComponent {
        [SerializeField]
        private Transform networkContainer;

        private Dictionary<string, NetworkSerialize> networkObjects;

        [SerializeField]
        private GameObject playerPrefab;

        public static string clientId { get; private set; }

        // Start is called before the first frame update
        public override void Start() {
            base.Start();
            init();
            configEvents();
        }

        // Update is called once per frame
        public override void Update() {
            base.Update();
        }

        private void init() {
            networkObjects = new Dictionary<string, NetworkSerialize>();
        }

        private void configEvents() {
            On("open", (e) => {
                //CUIDADO aqui roda 2x mas ele sempre vai fazer uma conexão!
                Debug.Log("Connectado");
            });

            On("register", (e) => {
                clientId = e.data["id"].ToString().Replace("\"", "");
                Debug.Log("ClientID " + clientId);
            });

            On("spawn", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");
                GameObject go = Instantiate(playerPrefab, networkContainer);
                go.name = string.Format("Player - ({0})", id);
                NetworkSerialize network = go.GetComponent<NetworkSerialize>();
                network.SetControllerId(id);
                network.SetSocketReference(this);
                networkObjects.Add(id, network);
            });

            On("disconnected", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");

                GameObject go = networkObjects[id].gameObject;
                Destroy(go);
                networkObjects.Remove(id);
            });

            On("updatePosition", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");
                float x = e.data["position"]["x"].f;
                float y = e.data["position"]["y"].f;

                NetworkSerialize network = networkObjects[id];
                network.transform.position = new Vector3(x, y, 0);
            });
        }
    }
}

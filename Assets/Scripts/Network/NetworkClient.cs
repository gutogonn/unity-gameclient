using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

namespace NodeNetwork {
    public class NetworkClient : SocketIOComponent {
        private Dictionary<string, NetworkIdentity> serverObjects;

        public static string clientId { get; private set; }

        // Start is called before the first frame update
        public override void Start() {
            base.Start();
            init();
            networkEvents();
        }

        // Update is called once per frame
        public override void Update() {
            base.Update();
        }

        private void init() {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }

        private void networkEvents() {
            On("open", (e) => {
                //CUIDADO aqui roda 2 vezes mas estabelece apenas uma conexão!
                Debug.Log("Conexão feita");
            });

            On("register", (e) => {
                clientId = e.data["id"].ToString().Replace("\"", "");
                Debug.Log("ID: " + clientId);
            });

            On("spawn", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");
                GameObject networkObject = (GameObject)Instantiate(Resources.Load("Player"), gameObject.transform);
                networkObject.name = string.Format("Player - ({0})", id);
                NetworkIdentity networkIdentity = networkObject.GetComponent<NetworkIdentity>();
                networkIdentity.SetControllerId(id);
                networkIdentity.SetSocketReference(this);
                serverObjects.Add(id, networkIdentity);
            });

            On("disconnected", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");
                GameObject networkObject = serverObjects[id].gameObject;
                Destroy(networkObject);
                serverObjects.Remove(id);
            });

            On("updatePosition", (e) => {
                string id = e.data["id"].ToString().Replace("\"", "");
                float x = e.data["position"]["x"].f;
                float y = e.data["position"]["y"].f;
                NetworkIdentity networkIdentity = serverObjects[id];
                networkIdentity.transform.position = new Vector3(x, y, 0);
            });
        }
    }
}

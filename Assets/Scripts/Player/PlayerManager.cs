using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeNetwork.Player {
    public class PlayerManager : MonoBehaviour {
        [SerializeField]
        private float speed = 4;

        [SerializeField]
        private NetworkIdentity networkIdentity;

        private float lastRotation;

        public void Update() {
            if (networkIdentity.IsControlling()) {
                checkMovement();
            }
        }

        private void checkMovement() {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        }
    }
}
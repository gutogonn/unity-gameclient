using NodeNetwork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float speed = 4;

    [SerializeField]
    private NetworkSerialize networkIdentity;

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

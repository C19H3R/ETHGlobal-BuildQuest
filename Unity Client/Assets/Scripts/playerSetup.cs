using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject fpsCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<PlayerMovementFPS>().enabled = true;
            transform.GetComponent<MouseLookFPS>().enabled = true;
            fpsCamera.GetComponent<Camera>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerMovementFPS>().enabled = false;
            transform.GetComponent<MouseLookFPS>().enabled = false;
            fpsCamera.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

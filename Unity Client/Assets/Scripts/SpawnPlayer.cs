using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    void Start()
    {
        int randomPoint = Random.Range(-40, 40);
        if (PhotonNetwork.IsConnected)
        {
            GameObject currentPlayer = PhotonNetwork.Instantiate(player.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity);
        }
    }

}

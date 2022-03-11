using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



public class CreateWeaponInBattle : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        
        if (SceneManager.GetActiveScene().name == "battle"&& photonView.IsMine)
        {
            GameObject newWeapon = PhotonNetwork.Instantiate("Weapon", transform.position,Quaternion.identity);
            newWeapon.transform.parent = transform;
            newWeapon.transform.localPosition = Vector3.zero;
            GenerateWeapnObject tmp = newWeapon.GetComponent<GenerateWeapnObject>();
            tmp.WeaponID = TransferDataToBattle.instance.nft_id;
            tmp.GetWeaponFromId();
            tmp.getWeaponGfx();
            tmp.UpdateNFTWeaponStats();
            NFTWeaponStats tmp2 = newWeapon.GetComponent<NFTWeaponStats>();
            tmp2.isEquiped = true;
            //  generate go and its parent should be this
        }
        else
        {

            GameObject newWeapon = PhotonNetwork.Instantiate("Weapon", transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            newWeapon.transform.localPosition = Vector3.zero;
            GenerateWeapnObject tmp = newWeapon.GetComponent<GenerateWeapnObject>();
            tmp.WeaponID = Random.Range(0,8399);
            tmp.GetWeaponFromId();
            tmp.getWeaponGfx();
            tmp.UpdateNFTWeaponStats();
            NFTWeaponStats tmp2 = newWeapon.GetComponent<NFTWeaponStats>();
            tmp2.isEquiped = true;
        }
    }

   
}

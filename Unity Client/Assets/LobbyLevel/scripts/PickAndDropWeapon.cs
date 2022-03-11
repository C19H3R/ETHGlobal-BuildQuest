using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDropWeapon : MonoBehaviour
{
    public Transform weaponContainerTransform;

    GameObject currentWeapon = null;

    private void Start()
    {

    }

    private void Update()
    {
        GetSwapInput();
    }

    private void GetSwapInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SwapWeaponWithRaycst();
        }
    }

    void SwapWeaponWithRaycst()
    {

        RaycastHit hit;

        Transform tmp = Camera.main.transform;
        if (Physics.Raycast(tmp.position + tmp.forward, tmp.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "NFTWeapon")
            {
                unEquipcurrentWeapon();
                equipOtherWeapon(hit.collider.gameObject);
                Debug.Log("Did Hit");
            }
            Debug.DrawRay(tmp.position, tmp.forward * 1000, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, tmp.forward * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }

    void equipOtherWeapon(GameObject other)
    {
        if (currentWeapon == null)
        {
            NFTWeaponStats currentWeaponObj = other.GetComponent<NFTWeaponStats>();
            currentWeaponObj.setWeaponToParent(weaponContainerTransform);
            currentWeapon = currentWeaponObj.gameObject;
            TransferDataToBattle.instance.Nft_id = currentWeapon.GetComponent<GenerateWeapnObject>().WeaponID;
        }
    }
    void unEquipcurrentWeapon()
    {
        if (currentWeapon != null)
        {
            NFTWeaponStats currentWeaponObj = currentWeapon.GetComponent<NFTWeaponStats>();
            currentWeaponObj.setWeaponToOriginalPos();
            currentWeapon = null;
        }
    }
}

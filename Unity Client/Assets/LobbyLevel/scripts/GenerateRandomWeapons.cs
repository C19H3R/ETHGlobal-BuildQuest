using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomWeapons : MonoBehaviour
{
    public int noOFWeapons;

    public GameObject weaponPrefab;

    public Transform wall;

    private void Start()
    {
        for (int i = 0; i < noOFWeapons; i++)
        {
            Vector3 WeaponPosition = wall.transform.position;
            WeaponPosition += new Vector3(6 - 2 * i, 0, -2);
            GameObject newWeapon = weaponPrefab;
            GenerateWeapnObject newWeaponObj = newWeapon.GetComponent<GenerateWeapnObject>();
            newWeaponObj.WeaponID = Random.Range(0, 8400);
            Instantiate(newWeapon, WeaponPosition,Quaternion.Euler(Vector3.zero));
        }
    }
}

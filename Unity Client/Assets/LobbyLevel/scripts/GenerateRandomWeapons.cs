using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomWeapons : MonoBehaviour
{
    public int noOFWeapons;

    List<int> tokensOwned;

    public GameObject weaponPrefab;

    public Transform wall;

    private void Start()
    {
        tokensOwned = TransferDataToNewScene.instance.tokensOwned;
        for (int i = 0; i < tokensOwned.Count; i++)
        {
            Vector3 WeaponPosition = wall.transform.position;
            WeaponPosition += new Vector3(6 - 2 * i, 0, -2);
            GameObject newWeapon = weaponPrefab;
            GenerateWeapnObject newWeaponObj = newWeapon.GetComponent<GenerateWeapnObject>();
            newWeaponObj.WeaponID = tokensOwned[i];
            Instantiate(newWeapon, WeaponPosition,Quaternion.Euler(Vector3.zero));
        }
    }
}

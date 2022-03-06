using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    //var :: body parts list
    //var :: barrel part list
    //var :: stock part list
    //var :: handle part list
    //var :: scope part list 
    //var :: magzine part list

    public List<GameObject> weaponBodyTypes;
    public List<GameObject> weaponBarrelTypes;
    public List<GameObject> weaponStockTypes;
    public List<GameObject> weaponHandleTypes;
    public List<GameObject> weaponScopeTypes;
    public List<GameObject> weaponMagzineTypes;


    public int  WeaponID;

    private int currentBodyId;
    private int currentBarrelId;
    private int currentStockId;
    private int currentHandleId;
    private int currentScopeId;
    private int currentMagzineId;


    GameObject prevWeapon = null;


   

    // Update is called once per frame
    void Update()
    {

        int totalBodyTypes = weaponBodyTypes.Count;
        int totalBarelTypes = weaponBarrelTypes.Count;
        int totalStockTypes = weaponStockTypes.Count;
        int totalHandleTypes = weaponHandleTypes.Count;
        int totalScopeTypes = weaponScopeTypes.Count;
        int totalMagzineTypes = weaponMagzineTypes.Count;



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (prevWeapon != null)
            {
                Destroy(prevWeapon);
            }

            GetWeaponFromId(WeaponID);
            WeaponID++;
            Debug.Log(WeaponID);
            Debug.Log(currentBodyId + " " + currentBarrelId + " " + currentStockId + " " + currentHandleId + " " + currentMagzineId);
            GameObject currWeaponBody = Instantiate(weaponBodyTypes[currentBodyId]);

            Transform currentWeaponBodySocket = currWeaponBody.transform.Find("Sockets");

            Transform barrelSocket = currentWeaponBodySocket.transform.Find("barrel");
            Transform stockSocket = currentWeaponBodySocket.transform.Find("stock");
            Transform scopeSocket = currentWeaponBodySocket.transform.Find("scope");
            Transform magzineSocket = currentWeaponBodySocket.transform.Find("magzine");
            Transform handleSocket = currentWeaponBodySocket.transform.Find("grip");

            GameObject currWeaponBarrel = Instantiate(weaponBarrelTypes[currentBarrelId], barrelSocket);
            GameObject currWeaponStock = Instantiate(weaponStockTypes[currentStockId], stockSocket);
            GameObject currWeaponHandle = Instantiate(weaponHandleTypes[currentHandleId], handleSocket);
            GameObject currWeaponScope = Instantiate(weaponScopeTypes[currentScopeId], scopeSocket);
            GameObject currWeaponMagzine = Instantiate(weaponMagzineTypes[currentMagzineId], magzineSocket);






            currWeaponBody.transform.parent = transform;
            currWeaponBody.transform.rotation = transform.rotation;
            prevWeapon = currWeaponBody;

        }

    }


    void GetWeaponFromId(int WeaponID)
    {
        int totalBodyTypes = weaponBodyTypes.Count;
        int totalBarelTypes = weaponBarrelTypes.Count;
        int totalStockTypes = weaponStockTypes.Count;
        int totalHandleTypes = weaponHandleTypes.Count;
        int totalScopeTypes = weaponScopeTypes.Count;
        int totalMagzineTypes = weaponMagzineTypes.Count;

        currentBodyId =0;//7
        currentBarrelId = 0;//5
        currentStockId =0;//5
        currentHandleId =0;//4
        currentScopeId = 0;//4
        currentMagzineId = 0;//3


        //int body's Place
        int bodyNumber = 0;

        //int barrel's Place
        int barrelNumber = 0;

        //int stock's Place
        int stockNumber = 0;

        //int handle's Place
        int handleNumber = 0;

        //int scope's Place
        int scopeNumber = 0;

        //int mags's Place
        int magNumber = 0;


        int carryOver=totalBarelTypes*totalStockTypes*totalHandleTypes*totalScopeTypes*totalMagzineTypes;

        //getbody
        currentBodyId = WeaponID / carryOver;

        WeaponID = WeaponID > carryOver ? WeaponID % carryOver : WeaponID;
        carryOver /= totalBarelTypes;

        //get barrel
        currentBarrelId = WeaponID / carryOver;

        WeaponID = WeaponID > carryOver ? WeaponID % carryOver : WeaponID;
        carryOver /= totalStockTypes;

        //get stock
        currentStockId = WeaponID / carryOver;

        WeaponID = WeaponID > carryOver ? WeaponID % carryOver : WeaponID;
        carryOver /= totalHandleTypes;

        //get handle
        currentHandleId = WeaponID / carryOver;

        WeaponID = WeaponID > carryOver ? WeaponID % carryOver : WeaponID;
        carryOver /= totalScopeTypes;

        //get scope
        currentScopeId = WeaponID / carryOver;

        WeaponID = WeaponID > carryOver ? WeaponID % carryOver : WeaponID;
        carryOver /= totalMagzineTypes;

        //get mag
        currentStockId = WeaponID / carryOver;

        Debug.Log(currentBodyId + " " + currentBarrelId + " " + currentStockId + " " + currentHandleId + " " + currentMagzineId);
    }
}
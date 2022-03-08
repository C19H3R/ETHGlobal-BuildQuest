using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateWeapnObject : MonoBehaviour
{
    public TextMeshProUGUI weaponRightInfoText;
    public int WeaponID;
    public Transform gfxParent;
    public List<GameObject> weaponBodyTypes;
    public List<GameObject> weaponBarrelTypes;
    public List<GameObject> weaponStockTypes;
    public List<GameObject> weaponHandleTypes;
    public List<GameObject> weaponScopeTypes;
    public List<GameObject> weaponMagzineTypes;

    #region forStats
    public List<ArmPartStatsSO> weaponBodyTypesSO;
    public List<ArmPartStatsSO> weaponBarrelTypesSO;
    public List<ArmPartStatsSO> weaponStockTypesSO;
    public List<ArmPartStatsSO> weaponHandleTypesSO;
    public List<ArmPartStatsSO> weaponScopeTypesSO;
    public List<MagSizeSO> weaponMagzineTypesSO;
    #endregion

    private int currentBodyId;
    private int currentBarrelId;
    private int currentStockId;
    private int currentHandleId;
    private int currentScopeId;
    private int currentMagzineId;

    float damageVal;
    float AccuVal;
    float RangeVal;
    float WeightVal;
    float MagSize;
    ArmPartStatsSO.ArmType weaponType;

    public NFTWeaponStats nftWeaponStatsComponents;

    GameObject WeaponGfx;

    private void Start()
    {
        weaponRightInfoText.text = "#" + WeaponID;
        GetWeaponFromId();
        getWeaponGfx();
        UpdateNFTWeaponStats();
    }
    void GetWeaponFromId()
    {
        int totalBodyTypes = weaponBodyTypes.Count;
        int totalBarelTypes = weaponBarrelTypes.Count;
        int totalStockTypes = weaponStockTypes.Count;
        int totalHandleTypes = weaponHandleTypes.Count;
        int totalScopeTypes = weaponScopeTypes.Count;
        int totalMagzineTypes = weaponMagzineTypes.Count;

        currentBodyId = 0;//7
        currentBarrelId = 0;//5
        currentStockId = 0;//5
        currentHandleId = 0;//4
        currentScopeId = 0;//4
        currentMagzineId = 0;//3

        int currID = 0;

        for (int bodyID = 0; bodyID < totalBodyTypes; bodyID++)
        {
            for (int barrelID = 0; barrelID < totalBarelTypes; barrelID++)
            {
                for (int stockID = 0; stockID < totalStockTypes; stockID++)
                {
                    for (int handleID = 0; handleID < totalHandleTypes; handleID++)
                    {
                        for (int scopeID = 0; scopeID < totalScopeTypes; scopeID++)
                        {
                            for (int magID = 0; magID < totalMagzineTypes; magID++)
                            {
                                if (currID >= WeaponID)
                                {
                                    currentBodyId = bodyID;
                                    currentBarrelId = barrelID;
                                    currentStockId = stockID;
                                    currentHandleId = handleID;
                                    currentScopeId = scopeID;
                                    currentMagzineId = magID;
                                    return;
                                }
                                currID++;
                            }
                        }
                    }
                }
            }
        }

    }


    GameObject getWeaponGfx()
    {
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

        currWeaponBody.transform.parent = gfxParent;
        currWeaponBody.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currWeaponBody.transform.localPosition= Vector3.zero;
        return currWeaponBody;
    }
    private void UpdateNFTWeaponStats()
    {
        //damage
        damageVal = weaponBarrelTypesSO[currentBarrelId].damage;
        damageVal += weaponBodyTypesSO[currentBodyId].damage;
        damageVal += weaponHandleTypesSO[currentHandleId].damage;
        damageVal += weaponStockTypesSO[currentStockId].damage;
        damageVal += weaponScopeTypesSO[currentScopeId].damage;

        //Accu
        AccuVal = weaponBarrelTypesSO[currentBarrelId].accuracy;
        AccuVal += weaponBodyTypesSO[currentBodyId].accuracy;
        AccuVal += weaponHandleTypesSO[currentHandleId].accuracy;
        AccuVal += weaponStockTypesSO[currentStockId].accuracy;
        AccuVal += weaponScopeTypesSO[currentScopeId].accuracy;

        //range
        RangeVal = weaponBarrelTypesSO[currentBarrelId].range;
        RangeVal += weaponBodyTypesSO[currentBodyId].range;
        RangeVal += weaponHandleTypesSO[currentHandleId].range;
        RangeVal += weaponStockTypesSO[currentStockId].range;
        RangeVal += weaponScopeTypesSO[currentScopeId].range;


        //weight
        WeightVal = weaponBarrelTypesSO[currentBarrelId].weight;
        WeightVal += weaponBodyTypesSO[currentBodyId].weight;
        WeightVal += weaponHandleTypesSO[currentHandleId].weight;
        WeightVal += weaponStockTypesSO[currentStockId].weight;
        WeightVal += weaponScopeTypesSO[currentScopeId].weight;

        //magSize
        MagSize = weaponMagzineTypesSO[currentMagzineId].magSize;

        //firemode
        weaponType = weaponBodyTypesSO[currentBodyId].type;

        nftWeaponStatsComponents.weaponDamage = damageVal;
        nftWeaponStatsComponents.weaponFiringMode = weaponType;
        nftWeaponStatsComponents.weaponMag = MagSize;
        nftWeaponStatsComponents.weaponRange = RangeVal;
        nftWeaponStatsComponents.weaponAccuracy = AccuVal;
        nftWeaponStatsComponents.weaponWeight = WeightVal;
    }
}

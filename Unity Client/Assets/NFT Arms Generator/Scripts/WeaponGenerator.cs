using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


// Write to file
/*File.AppendAllText(path, System.String.Format("{0} {1} {3}\n", var1, var2, var3));
*/
public class WeaponGenerator : MonoBehaviour
{
    string path = "testImages/imagesData.json";


    //var :: body parts list
    //var :: barrel part list
    //var :: stock part list
    //var :: handle part list
    //var :: scope part list 
    //var :: magzine part list

    public TextMeshProUGUI weaponIDUI;

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

    float damageVal;
    float AccuVal;
    float RangeVal;
    float WeightVal;
    float MagSize;
    ArmPartStatsSO.ArmType weaponType;

    public TextMeshProUGUI damageDisp;
    public TextMeshProUGUI accuracyDisp;
    public TextMeshProUGUI rangeDisp;
    public TextMeshProUGUI wegihtDisp;
    public TextMeshProUGUI magSizeDisp;
    public TextMeshProUGUI FireModeDisp;
    #endregion
    public int  WeaponID;

    private int currentBodyId;
    private int currentBarrelId;
    private int currentStockId;
    private int currentHandleId;
    private int currentScopeId;
    private int currentMagzineId;


    GameObject prevWeapon = null;


    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        int totalBodyTypes = weaponBodyTypes.Count;
        int totalBarelTypes = weaponBarrelTypes.Count;
        int totalStockTypes = weaponStockTypes.Count;
        int totalHandleTypes = weaponHandleTypes.Count;
        int totalScopeTypes = weaponScopeTypes.Count;
        int totalMagzineTypes = weaponMagzineTypes.Count;



        if (Input.GetKey(KeyCode.Space))
        {
            if (prevWeapon != null)
            {
                Destroy(prevWeapon);
            }
            int randomID = Random.Range(10, 5000);
           
            GetWeaponFromId();
            GetWeaponStat();
           

            weaponIDUI.text = "SuprArms #" + WeaponID;
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
            saveImage();
            updateJson();
            WeaponID++;

        }

    }


    void GetWeaponFromIdTmp(int WeaponID)
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
                                    Debug.Log(currentBodyId + " " + currentBarrelId + " " + currentStockId + " " + currentHandleId + " " +currentScopeId+" "+ currentMagzineId);
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

    void GetWeaponFromIdJson(int WeaponID)
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
                                    Debug.Log(currentBodyId + " " + currentBarrelId + " " + currentStockId + " " + currentHandleId + " " + currentScopeId + " " + currentMagzineId);
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

    void GetWeaponStat()
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
        UpdateWeaponUI();
    }
    void UpdateWeaponUI()
    {
        damageDisp.text = "Damage : " + damageVal;
        accuracyDisp.text = "Accuracy : " + AccuVal;
        wegihtDisp.text = "Weight : " + WeightVal;
        rangeDisp.text = "Range : " + RangeVal;
        magSizeDisp.text = "Mag Size : " + MagSize;
        FireModeDisp.text = "Fire Mode : " + weaponType;
    }
    void saveImage()
    {
        ScreenCapture.CaptureScreenshot("testImages/suprArms-" + WeaponID + ".png");
    }
    void updateJson()
    {
        MetadataObject obj = new MetadataObject();
        obj.image = "./suprArms-" + WeaponID + ".png";

        obj.id =""+WeaponID;
        obj.name = "SuprArms #" + WeaponID;


        //damage
        MetadataObject.Attribute damageAttr = new MetadataObject.Attribute();
        damageAttr.trait_type = "Damage";
        damageAttr.value = "" +damageVal;

        //accuracy
        MetadataObject.Attribute accuracyAttr = new MetadataObject.Attribute();
        accuracyAttr.trait_type = "Accuracy";
        accuracyAttr.value = "" + AccuVal;

        //weight
        MetadataObject.Attribute weightAttr = new MetadataObject.Attribute();
        weightAttr.trait_type = "Weight";
        weightAttr.value = "" + WeightVal;

        //type
        MetadataObject.Attribute typeAttr = new MetadataObject.Attribute();
        typeAttr.trait_type = "Weapon Type";
        typeAttr.value = "" + weaponType.ToString();

        //magSize
        MetadataObject.Attribute MagSizeAttr = new MetadataObject.Attribute();
        MagSizeAttr.trait_type = "Mag Size";
        MagSizeAttr.value = "" + MagSize;

        //range
        MetadataObject.Attribute rangeAttr = new MetadataObject.Attribute();
        rangeAttr.trait_type = "Range";
        rangeAttr.value = "" + RangeVal;

        obj.attributes.Add(damageAttr);
        obj.attributes.Add(accuracyAttr);
        obj.attributes.Add(weightAttr);
        obj.attributes.Add(typeAttr);
        obj.attributes.Add(MagSizeAttr);
        obj.attributes.Add(rangeAttr);
        string newObject = "\"" + WeaponID + "\":" + JsonUtility.ToJson(obj, true) + ",";
        Debug.Log(newObject);/*
        JsonUtility.ToJson(obj);*/
        File.AppendAllText(path, newObject);
    }

}
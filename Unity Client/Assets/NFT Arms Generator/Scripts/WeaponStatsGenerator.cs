using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStatsGenerator : MonoBehaviour
{
    public List<ArmPartStatsSO> weaponBodyTypes;
    public List<ArmPartStatsSO> weaponBarrelTypes;
    public List<ArmPartStatsSO> weaponStockTypes;
    public List<ArmPartStatsSO> weaponHandleTypes;
    public List<ArmPartStatsSO> weaponScopeTypes;/*
    public List<ArmPartStatsSO> weaponMagzineTypes;*/


    public TextMeshProUGUI damageDisp;
    public TextMeshProUGUI accuracyDisp;
    public TextMeshProUGUI rangeDisp;
    public TextMeshProUGUI wegihtDisp;


     float damageVal;
     float AccuVal;
     float RangeVal;
     float WeightVal;
     float MagSize;
     ArmPartStatsSO.ArmType weaponType;


    private int currentBodyId;
    private int currentBarrelId;
    private int currentStockId;
    private int currentHandleId;
    private int currentScopeId;/*
    private int currentMagzineId;*/


    void Update()
    {

        int totalBodyTypes = weaponBodyTypes.Count;
        int totalBarelTypes = weaponBarrelTypes.Count;
        int totalStockTypes = weaponStockTypes.Count;
        int totalHandleTypes = weaponHandleTypes.Count;
        int totalScopeTypes = weaponScopeTypes.Count;/*
        int totalMagzineTypes = weaponMagzineTypes.Count;*/



        if (Input.GetKey(KeyCode.Space))
        {
            
            currentBodyId = Random.Range(0, totalBodyTypes);
            currentBarrelId = Random.Range(0, totalBarelTypes);
            currentStockId = Random.Range(0, totalStockTypes);
            currentHandleId = Random.Range(0, totalHandleTypes);
            currentScopeId = Random.Range(0, totalScopeTypes);/*
            currentMagzineId = Random.Range(0, totalMagzineTypes);*/

            //damage
            damageVal = weaponBarrelTypes[currentBarrelId].damage;
            damageVal += weaponBodyTypes[currentBodyId].damage;
            damageVal += weaponHandleTypes[currentHandleId].damage;
            damageVal += weaponStockTypes[currentStockId].damage;
            damageVal += weaponScopeTypes[currentScopeId].damage;

            //Accu
            AccuVal = weaponBarrelTypes[currentBarrelId].accuracy;
            AccuVal += weaponBodyTypes[currentBodyId].accuracy;
            AccuVal += weaponHandleTypes[currentHandleId].accuracy;
            AccuVal += weaponStockTypes[currentStockId].accuracy;
            AccuVal += weaponScopeTypes[currentScopeId].accuracy;

            //range
            RangeVal = weaponBarrelTypes[currentBarrelId].range;
            RangeVal += weaponBodyTypes[currentBodyId].range;
            RangeVal += weaponHandleTypes[currentHandleId].range;
            RangeVal += weaponStockTypes[currentStockId].range;
            RangeVal += weaponScopeTypes[currentScopeId].range;


            //weight
            WeightVal = weaponBarrelTypes[currentBarrelId].weight;
            WeightVal += weaponBodyTypes[currentBodyId].weight;
            WeightVal += weaponHandleTypes[currentHandleId].weight;
            WeightVal += weaponStockTypes[currentStockId].weight;
            WeightVal += weaponScopeTypes[currentScopeId].weight;

            damageDisp.text = "damage :" + damageVal;
            accuracyDisp.text = "acccu :" + AccuVal;
            wegihtDisp.text = "weight :" + WeightVal;
            rangeDisp.text = "range :" + RangeVal;


        }

    }

}

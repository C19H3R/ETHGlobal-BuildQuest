using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartType_num", menuName = "PartStats/PartStatSo", order = 1)]
public class ArmPartStatsSO : ScriptableObject
{
    [System.Serializable]
    public enum ArmType { semi_auto,auto,burst}

    public float range;
    public float damage;
    public float accuracy;
    public float weight;
    public ArmType type;

}

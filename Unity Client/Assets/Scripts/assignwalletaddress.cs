using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class assignwalletaddress : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI walletaddress;
    // Start is called before the first frame update
    void Start()
    {

        walletaddress.text = TransferDataToNewScene.instance.walletAddress;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

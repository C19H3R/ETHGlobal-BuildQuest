using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferDataToBattle : MonoBehaviour
{

    public int Nft_id;
    public static TransferDataToBattle instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

}

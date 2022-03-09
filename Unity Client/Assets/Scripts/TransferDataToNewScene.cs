using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferDataToNewScene : MonoBehaviour
{
    public List<int> tokensOwned;

    public static TransferDataToNewScene instance;

    private void Start()
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

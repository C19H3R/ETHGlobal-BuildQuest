using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpdateBattleTimer : MonoBehaviour
{
    [SerializeField]
    TextMeshPro textele;

    public float time=20;

    private float timeGone = 0;

    void Start()
    {
        InvokeRepeating("updateColor", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timeGone += Time.deltaTime;
        float timeLeft = time - timeGone;
        if (timeGone < time)
        {
            textele.text = "Battle starts in : " + (int)timeLeft + "s";
        }

    }

   void updateColor()
    {
        textele.color = new Color32(
     (byte)Random.Range(100, 200),
     (byte)Random.Range(100, 200),
     (byte)Random.Range(100, 200),
     255
 );
    }

}

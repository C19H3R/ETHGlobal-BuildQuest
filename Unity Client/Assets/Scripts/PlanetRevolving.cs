using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRevolving : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 newposition;
    void Start()
    {
        newposition = new Vector3(0, 0, 0);
    }
    void Update()
    {
        newposition = new Vector3(0, newposition.y + Time.deltaTime, newposition.z - Time.deltaTime);
        transform.eulerAngles = newposition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem effects;
    private void OnCollisionEnter(Collision collision)
    {
        effects.Play();
        Vector3 position = this.gameObject.transform.position;
        Destroy(this.gameObject);
    }
}

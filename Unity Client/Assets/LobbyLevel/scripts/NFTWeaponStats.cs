using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTWeaponStats : MonoBehaviour
{
    public GameObject bulletObject;
    public Rigidbody bulletObjectrb;
    public Transform barrelEnd;

    public float weaponDamage;// _/ - high damage more bullet speed
    public float weaponMag;
    public float weaponWeight;// _/ - high weight stable recoil
    public float weaponRange;// _/  - high range more bullet speed
    public float weaponAccuracy;// _/ -high accurace small fire cone
    public ArmPartStatsSO.ArmType weaponFiringMode;// _/ fire mode 

    public bool isEquiped;


    float firingRate;
    float bulletSpeed;

    Quaternion initialRotation;
    Vector3 initialPos;
    bool canFire = true;
    private void Start()
    {
        initialPos = transform.position;
        initialRotation = transform.localRotation;

    }

    private void Update()
    {
        firingRate = weaponRange / 200;
        bulletSpeed = weaponDamage + weaponRange;
        OnFireButtonClick();
    }

    private void OnFireButtonClick()
    {

        //left mouse button
        if (Input.GetMouseButtonDown(0) && isEquiped && canFire)
        {
            Debug.Log("click");
            Shoot();
        }
    }
    private void Shoot()
    {
        Debug.Log("shoot");

        canFire = false;
        switch (weaponFiringMode)
        {
            case ArmPartStatsSO.ArmType.semi_auto:
                StartCoroutine(SemiAutoFireCoroutine());
                break;
            case ArmPartStatsSO.ArmType.auto:
                StartCoroutine(AutoFireCoroutine());
                break;
            case ArmPartStatsSO.ArmType.burst:
                StartCoroutine(BurstFireCoroutine());
                break;
            default:
                canFire = true;
                break;
        }
    }

    IEnumerator AutoFireCoroutine()
    {
        while (Input.GetButton("Fire1"))
        {
            ShootBullet();
            generateRecoil();
            yield return new WaitForSeconds(firingRate / 2);
        }

        canFire = true;
        transform.localRotation = initialRotation;


        yield return null;
    }

    IEnumerator BurstFireCoroutine()
    {
        int totalBullets = 4;

        for (int i = 0; i < totalBullets; i++)
        {
            ShootBullet();
            generateRecoil();
            yield return new WaitForSeconds(firingRate);
        }

        canFire = true;
        transform.localRotation = transform.localRotation = initialRotation;
        ;


        yield return null;
    }

    IEnumerator SemiAutoFireCoroutine()
    {

        ShootBullet();
        generateRecoil();
        yield return new WaitForSeconds(firingRate);

        canFire = true;
        transform.localRotation = transform.localRotation = initialRotation;
        ;

        yield return null;
    }

    private void generateRecoil()
    {
        //weight

        Quaternion recoilShift = new Quaternion();

        float recoilval = -weaponWeight / 50 - 0.5f;
        Debug.Log(recoilval);
        recoilShift.eulerAngles = new Vector3(recoilval, 0, 0);/*
        transform.RotateAround(transform.right, -0.1f);*/
        transform.Rotate(recoilShift.eulerAngles);
    }

    private void ShootBullet()
    {
        Vector3 foreward = barrelEnd.TransformDirection(Vector3.forward) * 10;

        GameObject newBullet = Instantiate(bulletObject, barrelEnd.position, transform.rotation);
        Rigidbody newBulletRb = newBullet.GetComponent<Rigidbody>();
        newBulletRb.AddForce(Vector3.Normalize(foreward) * bulletSpeed);


        Debug.Log("shootbullet");
        Debug.DrawRay(barrelEnd.position, foreward, Color.green);
    }

    public void setWeaponToOriginalPos()
    {
        isEquiped = false;
        transform.parent = null;
        transform.position = initialPos;
        transform.rotation = initialRotation;
    }
    public void setWeaponToParent(Transform parent)
    {
        isEquiped = true;
        transform.parent = parent;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
    }

}

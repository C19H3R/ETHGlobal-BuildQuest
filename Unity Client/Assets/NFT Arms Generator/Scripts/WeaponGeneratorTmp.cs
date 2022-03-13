using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneratorTmp : MonoBehaviour
{
    //var :: body parts list
    //var :: barrel part list
    //var :: stock part list
    //var :: handle part list
    //var :: scope part list 
    //var :: magzine part list

    public List<GameObject> weaponBodyTypes;
    public List<GameObject> weaponBarrelTypes;
    public List<GameObject> weaponStockTypes;
    public List<GameObject> weaponHandleTypes;
    public List<GameObject> weaponScopeTypes;
    public List<GameObject> weaponMagzineTypes;



    private int currentBodyId;
    private int currentBarrelId;
    private int currentStockId;
    private int currentHandleId;
    private int currentScopeId;
    private int currentMagzineId;


    GameObject prevWeapon = null;


    // Start is called before the first frame update
    void Start()
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

            currentBodyId = Random.Range(0, totalBodyTypes);
            currentBarrelId = Random.Range(0, totalBarelTypes);
            currentStockId = Random.Range(0, totalStockTypes);
            currentHandleId = Random.Range(0, totalHandleTypes);
            currentScopeId = Random.Range(0, totalScopeTypes);
            currentMagzineId = Random.Range(0, totalMagzineTypes);




            GameObject currWeaponBody = Instantiate(weaponBodyTypes[currentBodyId]);

            Transform currentWeaponBodySocket = currWeaponBody.transform.Find("Sockets");

            Transform barrelSocket = currentWeaponBodySocket.transform.Find("barrel");
            Transform stockSocket = currentWeaponBodySocket.transform.Find("stock");
            Transform scopeSocket = currentWeaponBodySocket.transform.Find("scope");
            Transform magzineSocket = currentWeaponBodySocket.transform.Find("magzine");
            Transform handleSocket = currentWeaponBodySocket.transform.Find("grip");

            /*GameObject currWeaponBarrel = Instantiate(weaponBarrelTypes[currentBarrelId], barrelSocket.position, Quaternion.EulerAngles(0, 0, 0), currWeaponBody.transform);
            GameObject currWeaponStock = Instantiate(weaponStockTypes[currentStockId], stockSocket.position, Quaternion.EulerAngles(0, 0, 0), currWeaponBody.transform);
            GameObject currWeaponHandle = Instantiate(weaponHandleTypes[currentHandleId], handleSocket.position, Quaternion.EulerAngles(0, 0, 0), currWeaponBody.transform);
            GameObject currWeaponScope = Instantiate(weaponScopeTypes[currentScopeId], scopeSocket.position, Quaternion.EulerAngles(0, 0, 0), currWeaponBody.transform);
            GameObject currWeaponMagzine = Instantiate(weaponMagzineTypes[currentMagzineId], magzineSocket.position, Quaternion.EulerAngles(0, 0, 0), currWeaponBody.transform);

*/

            GameObject currWeaponBarrel = Instantiate(weaponBarrelTypes[currentBarrelId],barrelSocket);
            GameObject currWeaponStock = Instantiate(weaponStockTypes[currentStockId], stockSocket);
            GameObject currWeaponHandle = Instantiate(weaponHandleTypes[currentHandleId], handleSocket);
            GameObject currWeaponScope = Instantiate(weaponScopeTypes[currentScopeId], scopeSocket);
            GameObject currWeaponMagzine = Instantiate(weaponMagzineTypes[currentMagzineId], magzineSocket);






            currWeaponBody.transform.parent = transform;
            currWeaponBody.transform.rotation = transform.rotation;
            prevWeapon = currWeaponBody;

        }

    }
}
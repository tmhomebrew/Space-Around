using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotater : MonoBehaviour
{
    Vector3 pointTowards;
    bool whenHit;
    public SpriteRenderer myRendr;

    public void ShieldOnHit(Vector3 hitDir)
    {
        //Set pointTowards of hitDir
        //Rotate shield to pointTowards.
        StartCoroutine(ShieldIsOn());
    }

    IEnumerator ShieldIsOn()
    {
        //Turn shieldRenderer ON 
        myRendr.enabled = true;
        
        //Displaying shield for 0.5 sec (??)
        yield return new WaitForSeconds(0.5f);
        
        //Turn shieldRenderer off
        myRendr.enabled = false;
    }
}

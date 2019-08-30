using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapScreenHandler : MonoBehaviour
{
    public GameObject spaceShipBody;
    [Header("Level Bounds:")]
    public GameObject Left, Right, Top, Bot;

    void Wrap()
    {
        //x-axis, Left side
        if (transform.position.x < Left.transform.position.x)
        {
            transform.position = new Vector3(Right.transform.position.x, transform.position.y, transform.position.z);
        }
        //x-axis, Right side
        if (transform.position.x > Right.transform.position.x)
        {
            transform.position = new Vector3(Left.transform.position.x, transform.position.y, transform.position.z);
        }
        //y-axis, Top side
        if (transform.position.y > Top.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Bot.transform.position.y, transform.position.z);
        }
        //y-axis, Right side
        if (transform.position.y < Bot.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Top.transform.position.y, transform.position.z);
        }
    }

    public IEnumerator CheckVisable()
    {
        yield return new WaitForEndOfFrame();
        if (spaceShipBody.transform.GetComponent<Renderer>().isVisible)
        {
            Wrap();
        }
    }
}

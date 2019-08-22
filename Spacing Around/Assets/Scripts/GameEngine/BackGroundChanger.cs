using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    #region Field
    [SerializeField]
    Sprite currentBackground;
    GameObject backgroundHolder;
    [SerializeField]
    List<Object> BackGroundPNGs = new List<Object>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundHolder == null)
        {
            backgroundHolder = transform.Find("BackgroundHolder").GetComponent<Transform>().gameObject;
        }
        if(currentBackground = null)
        {
            currentBackground = backgroundHolder.GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

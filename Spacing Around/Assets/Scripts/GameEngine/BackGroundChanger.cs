using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundChanger : MonoBehaviour
{
    #region Field
    [SerializeField]
    Sprite currentBackground;
    GameObject backgroundHolder;
    [SerializeField]
    List<Sprite> BackGroundPNGs = new List<Sprite>();

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
}

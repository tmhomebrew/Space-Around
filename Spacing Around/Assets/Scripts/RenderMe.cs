using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMe : MonoBehaviour
{
    #region Fields
    public Collider2D myParentCol;
    public SpriteRenderer myParentRendere;
    private Collider2D myCol;
    private Rigidbody2D myRig;
    
    private float myCurVel;
    private Vector2 myCurForce;
    private bool isInitialized;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        myParentRendere = GetComponentInParent<SpriteRenderer>();
        myCol = GetComponent<Collider2D>();
        myRig = GetComponentInParent<Rigidbody2D>();

        isInitialized = true;
    }

    public void SwitchState(bool isVisible)
    {
        if (myParentCol == null)
        {
            foreach (Collider2D col in GetComponentsInParent<Collider2D>())
            {
                if (col != myCol)
                {
                    myParentCol = col;
                    break;
                }
            }
        }

        if (isVisible)
        {
            myParentRendere.enabled = true;
            myParentCol.gameObject.GetComponent<AstroidScript>().AstroidIsWithinRange = true;
            myRig.WakeUp();
            //myParentCol.enabled = true;
            if (!isInitialized)
            {
                ResetVelocity(true);
            }
            else
            {
                isInitialized = !isInitialized;
            }
            //print("Im visible.!");
        }
        else
        {
            myParentRendere.enabled = false;
            myParentCol.gameObject.GetComponent<AstroidScript>().AstroidIsWithinRange = false;
            ResetVelocity(false);
            myRig.Sleep();
            //myParentCol.enabled = false;
            //print("Im invisible.!");
        }
    }

    public void ResetVelocity(bool visible)
    {
        if (visible)
        {
            myParentCol.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            myRig.angularVelocity = myCurVel;
            myRig.velocity = myCurForce;
        }
        else
        {
            myCurVel = myRig.angularVelocity;
            myCurForce = myRig.velocity;
            myParentCol.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
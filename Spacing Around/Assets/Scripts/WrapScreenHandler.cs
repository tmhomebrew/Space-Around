using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapScreenHandler : MonoBehaviour
{
    public GameObject GO;
    [Header("Level Bounds:")]
    public GameObject Left, Right, Top, Bot;
    List<Transform> newList = new List<Transform>();

    private void Start()
    {
        GO = transform.gameObject;
        if(GO.name.ToLower() == "spaceship")
        {
            GO = GetComponentInChildren<SpriteRenderer>().gameObject;
        }
        SetupBounds();
    }

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

    void SetupBounds()
    {
        //If Bounding boxes are empty..
        if (Left == null || Right == null || Top == null || Bot == null)
        {
            newList.AddRange(GameObject.FindGameObjectWithTag("BoundingBox").GetComponentsInChildren<Transform>());
            foreach (Transform go in newList)
            {
                if (go.name == "LevelBounds")
                {
                    continue;
                }
                if (go.name == "BoundLeft")
                {
                    Left = go.gameObject;
                }
                if (go.name == "BoundRight")
                {
                    Right = go.gameObject;
                }
                if (go.name == "BoundTop")
                {
                    Top = go.gameObject;
                }
                if (go.name == "BoundBot")
                {
                    Bot = go.gameObject;
                }
            }
        }
    }

    public IEnumerator CheckVisable()
    {
        yield return new WaitForEndOfFrame();
        if (GO.transform.GetComponent<Renderer>().isVisible)
        {
            Wrap();
        }
    }
}

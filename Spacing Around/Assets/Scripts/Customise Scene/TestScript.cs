using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    private bool hasChanged;

    public bool HasChanged { get => hasChanged; set => hasChanged = value; }

    private void Awake()
    {
        HasChanged = false;
    }
}

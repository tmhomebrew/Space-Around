using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct Translation : IComponentData
{
    public Vector3 Value;
}

using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public struct Rotation : IComponentData
{
    public Quaternion Value;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ObjectInfo
{
    public ObjectType type;
    public GameObject prefab;
    public int startCount;
}

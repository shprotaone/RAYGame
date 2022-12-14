using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BuilderSettings : ScriptableObject
{
    public float minRadius = 2;
    public float lineStepDistance = 0.5f;
    public float maximumLine = 8;
}

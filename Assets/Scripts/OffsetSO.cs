using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OffsetSO", menuName = "ScriptableObjects/OffsetSO", order = 1)]
public class OffsetSO : ScriptableObject
{
    public Vector3 translation;
    public Vector3 rotation;
    public Vector3 scale;
}

using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "ScriptableObjects/ProjectileSO", order = 2)]
public class ProjectileSO : ScriptableObject
{
    public GameObject prefabInit;
    public GameObject prefabAfter;
    public GameObject fx;
}

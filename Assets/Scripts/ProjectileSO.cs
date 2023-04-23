using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSO", menuName = "ScriptableObjects/ProjectileSO", order = 2)]
public class ProjectileSO : ScriptableObject
{
    public GameObject prefabWhole;
    public GameObject prefabCut;
    public GameObject fx;
}

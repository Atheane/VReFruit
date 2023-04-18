using UnityEngine;

[CreateAssetMenu(fileName = "FruitSO", menuName = "ScriptableObjects/FruitSO", order = 2)]
public class FruitSO : ScriptableObject
{
    public GameObject prefabWhole;
    public GameObject prefabCut;
    public GameObject fx;
}

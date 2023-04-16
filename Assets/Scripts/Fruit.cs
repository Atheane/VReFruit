using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fruit : MonoBehaviour
{
    [SerializeField]
    private FruitSO fruitSO;
    [SerializeField]
    private OffsetSO offsetSO;
    private GameObject instance;
    private bool isCut;

    void Start()
    {
        this.instance = Instantiate(this.fruitSO.prefabWhole, this.transform.position, Quaternion.identity);
        this.instance.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.instance.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        this.instance.AddComponent<Rigidbody>();
        this.instance.GetComponent<Rigidbody>().useGravity = true;
        this.instance.AddComponent<XRGrabInteractable>();
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

enum Interaction {
    HOVER_ENTERED,
    SELECT_ENTERED,
}
public class Fruit : MonoBehaviour
{
    [SerializeField]
    private FruitSO fruitSO;
    [SerializeField]
    private OffsetSO offsetSO;
    [SerializeField]
    private GameObject instance;
    private GameObject fruitFx;
    private bool isCut;
    private bool isSelected;
    private XRGrabInteractable interactable;
    private Canvas scoreUI;
    private int score;
    void Start()
    {
        this.CreateFruit();
        this.AttachInteractable();
    }

    void Update()
    {
        if (this.fruitFx && this.instance) {
            this.fruitFx.transform.position = this.instance.transform.position;
        }
    }

    private void CreateFruit() {
        this.instance = Instantiate(this.fruitSO.prefabWhole, this.transform.position, Quaternion.identity);
        this.instance.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.instance.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        Rigidbody rigidbody = this.instance.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;
        this.fruitFx = Instantiate(this.fruitSO.fx, this.instance.transform.position, Quaternion.identity);
        this.fruitFx.SetActive(false);
    }

    private void AttachInteractable() {
        this.interactable = this.instance.AddComponent<XRGrabInteractable>();
        this.interactable.hoverEntered.AddListener(OnHoverEntered);
        this.interactable.selectEntered.AddListener(OnSelectEntered);
        this.interactable.selectExited.AddListener(OnSelectExited); 
    }
    
    private void DetachInteractable() {
        this.interactable.hoverEntered.RemoveAllListeners();
        this.interactable.selectEntered.RemoveAllListeners();
        this.interactable.selectEntered.RemoveAllListeners();
    }

    private void OnHoverEntered(HoverEnterEventArgs args) {
        if (this.isSelected) return;
        this.Slice(Interaction.HOVER_ENTERED);
    }

    private void OnSelectEntered(SelectEnterEventArgs args) {
        this.isSelected = true;
        this.Slice(Interaction.SELECT_ENTERED);
    }

    private void OnSelectExited(SelectExitEventArgs args) {
        this.isSelected = false;
    }

    private void Slice(Interaction interactionType) {
        this.fruitFx.SetActive(true);
        this.DetachInteractable();
        Destroy(this.instance);
        this.instance = Instantiate(this.fruitSO.prefabCut, this.transform.position, Quaternion.identity);
        this.instance.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.instance.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        Rigidbody rigidbody = this.instance.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;
        this.isCut = true;
        //todo sendEvent to Game for points computation
    }
}

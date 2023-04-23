using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

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
    private PointsSO pointsSO;

    private GameObject fruitFx;

    private GameObject instance;
    private bool isCut;
    private bool isSelected;
    private XRGrabInteractable interactable;
    private Canvas scoreUI;
    private int score;
    private System.Random aleas = new System.Random();

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
    }
    
    private void DetachInteractable() {
        this.interactable.hoverEntered.RemoveAllListeners();
    }

    private void OnHoverEntered(HoverEnterEventArgs args) {
        Debug.Log("HOVER_ENTERED");
        this.Slice(Interaction.HOVER_ENTERED);
        this.AddPoints(Interaction.HOVER_ENTERED);
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
    }

    private void AddPoints(Interaction interactionType) {
        GameObject pointsCanvas = Instantiate(this.pointsSO.prefabPositivePoints, this.transform.position, this.transform.rotation);
        pointsCanvas.transform.parent = this.instance.transform;
        //todo sendEvent to Game for points computation
        var points = aleas.Next(25, 50);
        pointsCanvas.GetComponentInChildren<TMP_Text>().text = $"+{points}";
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

enum Interaction {
    HOVER_ENTERED,
    SELECT_ENTERED,
}
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private ProjectileSO projectileSO;
    [SerializeField]
    private OffsetSO offsetSO;
    [SerializeField]
    private PointsSO pointsSO;

    private GameObject fx;
    private GameObject instance;
    private bool isCut;
    private XRGrabInteractable interactable;
    private System.Random aleas = new System.Random();

    void Start()
    {
        this.CreateProjectile();
        this.AttachInteractable();
    }

    void Update()
    {
        if (this.fx && this.instance) {
            this.fx.transform.position = this.instance.transform.position;
        }
    }

    private void CreateProjectile() {
        this.instance = Instantiate(this.projectileSO.prefabWhole, this.transform.position, Quaternion.identity);
        this.instance.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.instance.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        Rigidbody rigidbody = this.instance.AddComponent<Rigidbody>();
        rigidbody.useGravity = true;
        this.fx = Instantiate(this.projectileSO.fx, this.instance.transform.position, Quaternion.identity);
        this.fx.SetActive(false);
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
        this.fx.SetActive(true);
        this.DetachInteractable();
        Destroy(this.instance);
        this.instance = Instantiate(this.projectileSO.prefabCut, this.transform.position, Quaternion.identity);
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

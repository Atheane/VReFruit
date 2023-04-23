using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

enum Interaction {
    HOVER_ENTERED,
    SELECT_ENTERED,
}
public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected ProjectileSO projectileSO;
    [SerializeField]
    protected OffsetSO offsetSO;
    [SerializeField]
    protected PointsSO pointsSO;

    protected GameObject fx;
    protected GameObject instance;
    protected System.Random aleas = new System.Random();
    protected XRGrabInteractable interactable;

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

    protected virtual XRGrabInteractable AttachInteractable() {
        return this.instance.AddComponent<XRGrabInteractable>();
    }
}

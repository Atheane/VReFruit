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

    protected float GROUND_LEVEL = 0.13f;

    void Start()
    {
        this.CreateProjectile();
        this.AttachInteractable();
    }

    protected virtual XRGrabInteractable AttachInteractable() {
        return this.instance.AddComponent<XRGrabInteractable>();
    }

    private void CreateProjectile() {
        this.instance = Instantiate(this.projectileSO.prefabInit, this.transform.position, Quaternion.identity);
        this.SetInitialTransform(this.instance);
        this.fx = Instantiate(this.projectileSO.fx, this.instance.transform.position + 0.2f*Vector3.up, Quaternion.identity);
        this.fx.transform.parent = this.instance.transform;
        this.fx.SetActive(false);
    }

    private void SetInitialTransform(GameObject go) {
        go.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        go.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        go.transform.localScale =  new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
    }
}

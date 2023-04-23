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
    protected PointsSO pointsSO;

    protected GameObject fx;
    protected GameObject instance;
    protected System.Random aleas = new System.Random();
    protected XRGrabInteractable interactable;

    protected float GROUND_LEVEL = 0.13f;

    private float _speed;
    private Vector3 _target;

    public void StartMovingTowards(float speed)
    {
        _target = Camera.main.transform.position;
        _speed = speed;
    }

    void FixedUpdate() {
        if (this.instance) {
            this.instance.transform.position = Vector3.MoveTowards(this.instance.transform.position, _target, _speed);
        }
    }

    void OnLoaded(IProjectileProps projectileProps) {
        Debug.Log("----OnLoaded----");
        this.CreateProjectile(projectileProps.Init);
        this.AttachInteractable();
        this.StartMovingTowards(projectileProps.Velocity);
    }

    protected virtual XRGrabInteractable AttachInteractable() {
        return this.instance.AddComponent<XRGrabInteractable>();
    }

    private void CreateProjectile(Transform initTransform) {
        this.instance = Instantiate(this.projectileSO.prefabInit, initTransform.position, initTransform.rotation);
        this.fx = Instantiate(this.projectileSO.fx, this.instance.transform.position + 0.2f*Vector3.up, Quaternion.identity);
        this.fx.transform.parent = this.instance.transform;
        this.fx.SetActive(false);
    }
}

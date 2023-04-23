using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class Fruit : Projectile
{
    protected override XRGrabInteractable AttachInteractable() {
        this.interactable = this.instance.AddComponent<XRGrabInteractable>();
        this.interactable.hoverEntered.AddListener(OnHoverEntered);
        return this.interactable;
    }

    protected void OnHoverEntered(HoverEnterEventArgs args) {
        Debug.Log("HOVER_ENTERED");
        this.Slice();
        this.AddPoints();
    }

    private void Slice() {
        this.fx.SetActive(true);
        this.interactable.hoverEntered.RemoveAllListeners();
        Destroy(this.instance);
        this.instance = Instantiate(this.projectileSO.prefabAfter, this.transform.position, Quaternion.identity);
        this.instance.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.instance.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
    }

    private void AddPoints() {
        GameObject pointsCanvas = Instantiate(this.pointsSO.prefabPositivePoints, this.transform.position, this.transform.rotation);
        pointsCanvas.transform.parent = this.instance.transform;
        //todo sendEvent to Game for points computation
        var points = this.aleas.Next(25, 50);
        pointsCanvas.GetComponentInChildren<TMP_Text>().text = $"+{points}";
    }
}

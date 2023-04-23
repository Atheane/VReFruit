using TMPro;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

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
    }

    private void Slice() {
        var lastTransform = this.instance.transform;
        Destroy(this.instance);
        this.instance = Instantiate(this.projectileSO.prefabAfter, lastTransform.position, lastTransform.rotation);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        this.fx.transform.parent = this.instance.transform;
        this.fx.SetActive(true);
        this.AddPoints();
    }

    private async void AddPoints() {
        GameObject pointsCanvas = Instantiate(this.pointsSO.prefabPositivePoints,this.instance.transform.position + new Vector3(0.25f, 0.25f, 0), this.instance.transform.rotation);
        pointsCanvas.transform.parent = this.instance.transform;
        //todo sendEvent to Game for points computation
        // create an animation on points (fade)
        var points = this.aleas.Next(25, 50);
        pointsCanvas.GetComponentInChildren<TMP_Text>().text = $"+{points}";
        Debug.Log("SEND MESSAGE TO GAME WITH + " + points + "POINTS");
        await Task.Delay(1000);
        Destroy(this.instance);
    }
}

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using TMPro;

public class Bomb : Projectile
{
    protected override XRGrabInteractable AttachInteractable() {
        this.interactable = this.instance.AddComponent<XRGrabInteractable>();
        this.interactable.hoverEntered.AddListener(OnHoverEntered);
        return this.interactable;
    }

    protected void OnHoverEntered(HoverEnterEventArgs args) {
        Debug.Log("BOMB HOVER");
        this.Slice();
    }

    private void Slice() {
        this.fx.SetActive(true);
        this.interactable.hoverEntered.RemoveAllListeners();
    }

    private void RemovePoints() {
        GameObject pointsCanvas = Instantiate(this.pointsSO.prefabNegativePoints, this.transform.position, this.transform.rotation);
        pointsCanvas.transform.parent = this.instance.transform;
        //todo sendEvent to Game for points computation
        var points = this.aleas.Next(50, 100);
        pointsCanvas.GetComponentInChildren<TMP_Text>().text = $"-{points}";
    }
}

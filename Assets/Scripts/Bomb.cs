using TMPro;
using System.Threading.Tasks;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class Bomb : Projectile
{
    protected override XRGrabInteractable AttachInteractable() {
        this.interactable = this.instance.AddComponent<XRGrabInteractable>();
        this.interactable.hoverEntered.AddListener(OnHoverEntered);
        return this.interactable;
    }

    protected void OnHoverEntered(HoverEnterEventArgs args) {
        Debug.Log("BOMB HOVER");
        this.LaunchCoolDown();
    }

    private async void LaunchCoolDown() {
        this.fx.SetActive(true);
        this.interactable.hoverEntered.RemoveAllListeners();
        await Task.Delay(4000);
        this.Explode();
}

    private void Explode() {
        var lastTransform = this.instance.transform;
        Destroy(this.instance);
        this.instance = Instantiate(this.projectileSO.prefabAfter, lastTransform.position, lastTransform.rotation);
        this.instance.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
        this.RemovePoints();
    }

    private async void RemovePoints() {
        GameObject pointsCanvas = Instantiate(this.pointsSO.prefabNegativePoints, this.transform.position, this.transform.rotation);
        pointsCanvas.transform.parent = this.instance.transform;
        //todo sendEvent to Game for points computation
        var points = this.aleas.Next(50, 100);
        pointsCanvas.GetComponentInChildren<TMP_Text>().text = $"-{points}";
        Debug.Log("SEND MESSAGE TO GAME WITH - " + points + "POINTS");
        await Task.Delay(1000);
        Destroy(this.instance);
    }
}

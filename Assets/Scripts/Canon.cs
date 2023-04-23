using System.Collections;
using UnityEngine;


public interface IProjectileProps {
    public float Velocity  {get;}
    public Transform Init {get;}
}
public class ProjectileProps : IProjectileProps {
    public float Velocity  {get; private set;}
    public Transform Init {get; private set;}
    public ProjectileProps(float velocity, Transform init) {
        Velocity = velocity;
        Init = init;
    }

}

public class Canon : MonoBehaviour
{
    [SerializeField]
    private GameObject[] projectileTypes;
    [SerializeField]
    private int gameDuration;
    private float playDuration;
    private System.Random aleas = new System.Random();
    private int frequency;
    private IProjectileProps projectileProps;

    private GameObject currentProjectile;

    void Start()
    {
        Debug.Log("Start Canon");
        this.frequency = 5;
        this.projectileProps = new ProjectileProps(0.15f, this.transform);
        StartCoroutine(this.SendProjectiles());
    }

    IEnumerator SendProjectiles()
    {
        if (this.playDuration > this.gameDuration*60) {
            Debug.Log("GAME OVER");
        }
        while(this.playDuration < this.gameDuration*60) {
            Debug.Log("SendProjectiles");
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            var projectileIndex = this.aleas.Next(0, 7);
            this.currentProjectile = this.projectileTypes[projectileIndex];
            this.currentProjectile.SendMessage("OnLoaded", this.projectileProps);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(this.frequency);
            this.playDuration += Time.time;
            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        }
    }
}

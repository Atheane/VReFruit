using System.Collections;
using UnityEngine;

enum Fruits {
    APPLE,
    COCONUT,
    DRAGONFRUIT,
    DURIAN,
    GRAPEFRUIT,
    GUAVA,
    LEMON,
    MANGO,
    ORANGE,
    PAPAYA,
    PEAR,
    PINEAPPLE,
    POMEGRANATE,
    WATERMELON
}

public interface IProjectileProps {
    public int Velocity  {get;}
    public Transform Init {get;}
}
public class ProjectileProps : IProjectileProps {
    public int Velocity  {get; private set;}
    public Transform Init {get; private set;}
    public ProjectileProps(int velocity, Transform init) {
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
    private System.Random aleas = new System.Random();
    private int frequency;
    private IProjectileProps projectileProps;

    private GameObject currentProjectile;

    void Start()
    {
        Debug.Log("Start Canon");
        this.frequency = 1;
        this.projectileProps = new ProjectileProps(2, this.transform);
        StartCoroutine(this.SendProjectiles());
    }

    IEnumerator SendProjectiles()
    {
        Debug.Log("SendProjectiles");
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        var projectileIndex = this.aleas.Next(0, 13);
        this.currentProjectile = this.projectileTypes[projectileIndex];
        this.currentProjectile.SendMessage("OnLoaded", this.projectileProps);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(this.frequency);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}

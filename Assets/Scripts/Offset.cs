using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private OffsetSO offsetSO;
    void Start()
    {
        this.transform.Translate(new Vector3(this.offsetSO.translation.x, this.offsetSO.translation.y, this.offsetSO.translation.z));
        this.transform.Rotate(this.offsetSO.rotation.x, this.offsetSO.rotation.y, this.offsetSO.rotation.z);
        this.transform.localScale = new Vector3(this.offsetSO.scale.x, this.offsetSO.scale.y, this.offsetSO.scale.z);
    }

}

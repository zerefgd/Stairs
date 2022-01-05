using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject follower;

    private Vector3 offest;
    // Start is called before the first frame update
    void Start()
    {
        offest = transform.position - follower.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = follower.transform.position + offest;
    }
}

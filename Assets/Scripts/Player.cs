using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Vector3 movementSpeed;
    [SerializeField]
    private float speed;
    public bool hasGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        hasGameStarted = false;
        movementSpeed = new Vector3(0, 1, 2);
    }


    private void FixedUpdate()
    {
        transform.Translate(movementSpeed * Time.fixedDeltaTime);
        if (!hasGameStarted) return;
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, 0, 0);
        Vector3 temp = transform.position;
        if (temp.x > 3.75f)
            temp.x = 3.75f;
        if (temp.x < -3.75f)
            temp.x = -3.75f;
        transform.position = temp;
    }
}

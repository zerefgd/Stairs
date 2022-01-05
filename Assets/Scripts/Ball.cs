using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField]
    private float startSpeed, forceMultiplier;

    private float acceleration, speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        acceleration = -startSpeed * forceMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -1f)
            GameManager.instance.GameOver();
    }

    private void FixedUpdate()
    {
        speed += acceleration * Time.fixedDeltaTime;
        Vector3 temp = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        transform.localPosition += temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        speed = startSpeed;
        GameManager.instance.SpawnBlock();
        if (!GameManager.instance.hasGameStarted) return;
        if(collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject, 5f);
            GameManager.instance.UpdateScore();
        }
        else if(collision.gameObject.CompareTag("Combo"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.UpdateCombo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Diamond"))
        {
            GameManager.instance.UpdateDiamond();
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Spike"))
        {
            GameManager.instance.GameOver();
        }
    }

}

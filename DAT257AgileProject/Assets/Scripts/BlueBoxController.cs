using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBoxController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    private float distanceTraveled = 0;
    private float startPosX;
    private int direction = 1;

    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        moveBox();
    }

    private void moveBox()
    {
        Vector3 movement = new Vector3(speed * Time.deltaTime * direction, 0f, 0f);
        transform.Translate(movement);

        // Switch direction if needed
        distanceTraveled = transform.position.x - startPosX;
        if (distanceTraveled >= 4) { direction = -1; }
        if (distanceTraveled <= 0) { direction = 1; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            isColliding = false;
        }
    }

    public bool BoxIsColliding()
    {
        return isColliding;
    }
}

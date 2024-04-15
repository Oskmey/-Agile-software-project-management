using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.x.normalized);
        animator.SetFloat("Horizontal", rb.velocity.x.normalized);
        animator.SetFloat("Vertical", rb.velocity.y.normalized);
    }
}

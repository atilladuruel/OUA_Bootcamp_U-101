using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Attack");
            animator.SetTrigger("Attack"); animator.SetBool("Jump", true);
            animator.SetBool("Attack", true);
        }
    }
}

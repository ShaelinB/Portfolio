using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTomato : MonoBehaviour
{
    public Animator animator;

    public Vector2 dir;

    public GameObject player;
    public Rigidbody2D rb;
    public float RollForce;
    public bool rolling;
    public bool canRoll;

    public List<GameObject> bodyParts;
    public List<GameObject> OnBodyParts;
    public Transform throwPos;
    public bool canThrow;
    public bool throwing;


    public float JumpForce;
    public bool canJump;
    public bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rolling = false;
        jumping = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {

            if (bodyParts.Count > 0)
            {

                
                    throwing = false;
                    animator.SetInteger("Attack", Random.Range(1, 5));
                
            }
            else
            {
                animator.SetInteger("Attack", Random.Range(2, 5));

            }
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
        {
            if (canThrow && !throwing)
            {
                canThrow = false;
               
                if (bodyParts.Count > 0)
                {
                    Instantiate(bodyParts[bodyParts.Count - 1], throwPos.position, Quaternion.identity);
                    OnBodyParts[bodyParts.Count - 1].SetActive(false);
                    bodyParts.RemoveAt(bodyParts.Count - 1);                    
                    throwing = true;
                }
            }

        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {

        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
        {
            if (canRoll && !rolling)
            {
                canRoll = false;
                dir = (player.transform.position - transform.position);
                rb.AddForce(Vector2.up * RollForce,ForceMode2D.Impulse);
                rolling = true;
            }

        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            if (canJump && !jumping)
            {
                canJump = false;
                rb.AddForce(Vector2.up * JumpForce);
                jumping = true;
            }

            if (transform.position.y > 50)
            {
                transform.position = new Vector2(player.transform.position.x, 50);
            }
        }

    }
}

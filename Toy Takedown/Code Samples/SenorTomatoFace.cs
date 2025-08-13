using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenorTomatoFace : MonoBehaviour
{
    public Animator animator;
    int attackChoice;
    int throwChoice;
    int bodyPartsRemaining = 3;
    public GameObject[] bodyParts = new GameObject[3];
    bool[] isBodyPartThere;
    public GameObject player;
    Vector2 playerPos;
    Vector2 tomatoPos;
    Rigidbody2D tomatoRB;
    public float speed;
    bool rolling;
    public float jumpHeight;
    bool jumping;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        isBodyPartThere = new bool[3] { true, true, true };
        rolling = false;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetInteger("Attack") == 0)
        {
            TomatoIdle();
        }
    }

    void TomatoIdle()
    {
        attackChoice = Random.Range(1, 4);
        switch (attackChoice)
        {
            case 1:
                //punch
                Punch();
                break;

            case 2:
                //throw
                Throw();
                break;

            case 3:
                //roll
                Roll();
                break;

            case 4:
                //jump
                Jump();
                break;
        }

    }
    void Punch()
    {
        animator.SetInteger("Attack", 1);
        audioManager.PlaySFXForScene(audioManager.tomatoKick);
        animator.SetInteger("Attack", 0);
    }

    void Throw()
    {
        animator.SetInteger("Attack", 2);
        if (bodyPartsRemaining>0)
        {
            throwChoice = Random.Range(1, 3);
            switch(throwChoice)
            {
                case 1: 
                    //stem
                    if(isBodyPartThere[0])
                    {
                        animator.SetInteger("ObjectThrown", 1);
                        isBodyPartThere[0] = false;
                    }
                    else
                    {
                        throwChoice = Random.Range(1, 2);
                        switch(throwChoice)
                        {
                            case 1:
                                //left ear
                                if (isBodyPartThere[1])
                                {
                                    //left ear
                                    animator.SetInteger("ObjectThrown", 2);
                                    isBodyPartThere[1] = false;
                                }
                                else
                                {
                                    //right ear
                                    animator.SetInteger("ObjectThrown", 3);
                                    isBodyPartThere[2] = false;
                                }
                                break;

                            case 2:
                                //right ear
                                if (isBodyPartThere[2])
                                {
                                    //right ear
                                    animator.SetInteger("ObjectThrown", 3);
                                    isBodyPartThere[2] = false;
                                }
                                else
                                {
                                    //left ear
                                    animator.SetInteger("ObjectThrown", 2);
                                    isBodyPartThere[1] = false;
                                }
                                break;
                        }
                    }
                    break;

                case 2:
                    //left ear
                    if (isBodyPartThere[1])
                    {
                        animator.SetInteger("ObjectThrown", 2);
                        isBodyPartThere[1] = false;
                    }
                    else
                    {
                        throwChoice = Random.Range(1, 2);
                        switch (throwChoice)
                        {
                            case 1:
                                //stem
                                if (isBodyPartThere[0])
                                {
                                    //stem
                                    animator.SetInteger("ObjectThrown", 1);
                                    isBodyPartThere[0] = false;
                                }
                                else
                                {
                                    //right ear
                                    animator.SetInteger("ObjectThrown", 3);
                                    isBodyPartThere[2] = false;
                                }
                                break;

                            case 2:
                                //right ear
                                if (isBodyPartThere[2])
                                {
                                    //right ear
                                    animator.SetInteger("ObjectThrown", 3);
                                    isBodyPartThere[2] = false;
                                }
                                else
                                {
                                    //stem
                                    animator.SetInteger("ObjectThrown", 1);
                                    isBodyPartThere[0] = false;
                                }
                                break;
                        }
                    }
                    break;

                case 3:
                    //right ear
                    if (isBodyPartThere[2])
                    {
                        animator.SetInteger("ObjectThrown", 3);
                        isBodyPartThere[2] = false;
                    }
                    else
                    {
                        throwChoice = Random.Range(1, 2);
                        switch (throwChoice)
                        {
                            case 1:
                                //stem
                                if (isBodyPartThere[0])
                                {
                                    //stem
                                    animator.SetInteger("ObjectThrown", 1);
                                    isBodyPartThere[0] = false;
                                }
                                else
                                {
                                    //left ear
                                    animator.SetInteger("ObjectThrown", 2);
                                    isBodyPartThere[1] = false;
                                }
                                break;

                            case 2:
                                //left ear
                                if (isBodyPartThere[1])
                                {
                                    //left ear
                                    animator.SetInteger("ObjectThrown", 2);
                                    isBodyPartThere[1] = false;
                                }
                                else
                                {
                                    //stem
                                    animator.SetInteger("ObjectThrown", 1);
                                    isBodyPartThere[0] = false;
                                }
                                break;
                        }
                    }
                    break;
            }
            bodyPartsRemaining--;
            animator.SetInteger("ObjectThrown", 0);
            audioManager.PlaySFXForScene(audioManager.tomatoThrow);
            animator.SetInteger("Attack", 0);
        }
    }

    void Roll()
    {
        tomatoPos = gameObject.GetComponent<Transform>().position;
        playerPos = player.GetComponent<Transform>().position;
        rolling = true;
        if(playerPos.x < tomatoPos.x)
        {
            animator.SetInteger("Attack", 3);
            if(rolling)
            {
                Vector2 newPos = new Vector2(tomatoPos.x-speed, tomatoPos.y);
                tomatoPos = newPos;
                audioManager.PlaySFXForScene(audioManager.tomatoRoll);
            }
        }
        else if(playerPos.x > tomatoPos.x)
        {
            animator.SetInteger("Attack", 3);
            if (rolling)
            {
                Vector2 newPos = new Vector2(tomatoPos.x - speed, tomatoPos.y);
                tomatoPos = newPos;
                audioManager.PlaySFXForScene(audioManager.tomatoRoll);
            }
        }
        else
        {
            animator.SetInteger("Attack", 0);
            animator.SetInteger("Attack", 1);
            rolling = false;
            audioManager.PlaySFXForScene(audioManager.tomatoKick);
        }
        animator.SetInteger("Attack", 0);
    }

    void Jump()
    {
        animator.SetInteger("Attack", 4);
        jumping = true;
        playerPos = player.GetComponent<Transform>().position;
        tomatoPos = gameObject.GetComponent<Transform>().position;
        tomatoRB.freezeRotation = true;
        float playerPosX = playerPos.x;
        Vector2 jumpPos = new Vector2(playerPosX, jumpHeight);
        Vector2 jumpDirection = jumpPos-tomatoPos;
        float force = jumpHeight - tomatoPos.y;
        tomatoRB.AddForce(jumpDirection.normalized * force);
        while(jumping)
        {
            if (tomatoPos.y == jumpHeight)
            {
                tomatoRB.velocity = new Vector2(0,0);
                jumping = false;
            }
        }
        audioManager.PlaySFXForScene(audioManager.tomatoLand);
        animator.SetInteger("Attack", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player") || collision.gameObject.CompareTag("wall"))
        {
            if(collision.gameObject.CompareTag("wall"))
            {
                animator.SetInteger("Attack", 0);
                rolling = false;
            }

            if(collision.gameObject.CompareTag("player"))
            {
                animator.SetInteger("Attack", 0);
                rolling = false;
            }
        }
        animator.SetInteger("Attack", 0);
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
}

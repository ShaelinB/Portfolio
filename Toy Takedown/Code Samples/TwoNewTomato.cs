using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoNewTomato : MonoBehaviour
{
    public GameObject player;
    public float force;
    public Rigidbody2D rb;

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        else
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            audioManager.PlaySFXForScene(audioManager.tomatoThrow);
            Vector2 dir = (player.transform.position + new Vector3(0, 20, 0)) - transform.position;
            rb.AddForce(dir.normalized * force, ForceMode2D.Impulse);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }
}

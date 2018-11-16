using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBHV : MonoBehaviour {
    [SerializeField]
    protected float minTimeToWait, maxTimeToWait, minTimeWalking, maxTimeWalking, health, moveSpeed, damage, invincibilityTime;
    [SerializeField]
    protected GameObject playerObj;

    private float waitingTime, walkingTime, walkUntil, waitUntil, invincibilityCount;
    private bool isWalking, isInvincible;

    private void Awake()
    {
        waitingTime = 0.0f;
        walkingTime = 0.0f;
        isWalking = false;
        waitUntil = Random.Range(minTimeToWait, maxTimeToWait);
        playerObj = Player.instance.gameObject;
        isInvincible = false;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isInvincible)
            if (invincibilityTime >= invincibilityCount)
                isInvincible = false;
            else
                invincibilityCount += Time.deltaTime;

        if (isWalking)
        {
            if(walkingTime < walkUntil)
                Walk();
            else
            {
                walkingTime = 0.0f;
                isWalking = false;
                waitUntil = Random.Range(minTimeToWait, maxTimeToWait);
            }
        }
        else
        {
            if (waitingTime < waitUntil)
                Wait();
            else
            {
                waitingTime = 0.0f;
                isWalking = true;
                walkUntil = Random.Range(minTimeWalking, maxTimeWalking);
            }
        }
	}

    void Walk()
    {
        Vector2 target = new Vector2(playerObj.transform.position.x - transform.position.x, playerObj.transform.position.y - transform.position.y);
        transform.position += new Vector3 ((target.x+1) * moveSpeed * Time.deltaTime, (target.y+1) * moveSpeed * Time.deltaTime, 0f);
        walkingTime += Time.deltaTime;
    }

    void Wait()
    {
        //TODO Scream
        waitingTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            if (!isInvincible)
            {
                health -= collision.GetComponent<BulletController>().damage;
                CheckDeath();
                isInvincible = true;
                invincibilityCount = 0f;
            }
        }
        else if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().ReceiveDamage(damage);
        }
    }

    private void CheckDeath()
    {
        if(health <= 0f)
        {
            //TODO Audio and Particles
            Destroy(gameObject);
        }
    }
}

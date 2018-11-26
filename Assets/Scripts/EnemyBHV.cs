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
    private Color originalColor;

    private void Awake()
    {
        waitingTime = 0.0f;
        walkingTime = 0.0f;
        isWalking = false;
        waitUntil = Random.Range(minTimeToWait, maxTimeToWait);
        playerObj = Player.instance.gameObject;
        isInvincible = false;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isInvincible)
            if (invincibilityTime < invincibilityCount)
            {
                isInvincible = false;
                gameObject.GetComponent<SpriteRenderer>().color = originalColor;

            }
            else
            {
                invincibilityCount += Time.deltaTime;
            }

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
        int xOffset, yOffset;
        Vector2 target = new Vector2(playerObj.transform.position.x - transform.position.x, playerObj.transform.position.y - transform.position.y);
        target.Normalize();
        if (target.x >= 0)
            xOffset = 1;
        else
            xOffset = -1;
        if (target.y >= 0)
            yOffset = 1;
        else
            yOffset = -1;

        transform.position += new Vector3 ((target.x + xOffset) * moveSpeed * Time.deltaTime, (target.y + yOffset) * moveSpeed * Time.deltaTime, 0f);
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
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                health -= collision.GetComponent<BulletController>().damage;
                CheckDeath();
                isInvincible = true;
                invincibilityCount = 0f;
                collision.GetComponent<BulletController>().DestroyBullet();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collide with Player");
            collision.gameObject.GetComponent<PlayerController>().ReceiveDamage(damage);
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

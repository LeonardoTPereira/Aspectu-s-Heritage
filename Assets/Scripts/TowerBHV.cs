using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBHV : MonoBehaviour {
    [SerializeField]
    protected float minTimeToWait, maxTimeToWait, health, damage, invincibilityTime, shootSpeed;
    [SerializeField]
    protected GameObject playerObj, bulletSpawn, bulletPrefab;

    protected Animator anim;
    protected float waitingTime, waitUntil, invincibilityCount;
    protected bool isShooting, isInvincible;
    protected Color originalColor;
    protected float lastX, lastY;

    private void Awake()
    {
        waitingTime = 0.0f;
        isShooting = false;
        waitUntil = 1.5f;
        playerObj = Player.instance.gameObject;
        isInvincible = false;
        anim = GetComponent<Animator>();
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
        {
            if (invincibilityTime < invincibilityCount)
            {
                isInvincible = false;
                gameObject.GetComponent<SpriteRenderer>().color = originalColor;

            }
            else
            {
                invincibilityCount += Time.deltaTime;
            }
        }

        if (isShooting)
        {
            Debug.Log("IsShooting");
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                Debug.Log("Shoot it!");
                Shoot();
                isShooting = false;
                EndShootAnimation();
                waitUntil = Random.Range(minTimeToWait, maxTimeToWait);
            }
            else
            {
                anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
                StartShootAnimation();
            }
            
        }
        else
        {
            if (waitingTime < waitUntil)
                Wait();
            else
            {
                waitingTime = 0.0f;
                isShooting = true;
            }
        }
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

    protected void StartShootAnimation()
    {
        anim.SetBool("IsShooting", true);
    }

    protected void EndShootAnimation()
    {
        anim.SetBool("IsShooting", false);
    }

    protected void Shoot()
    {
        Vector2 target = new Vector2(playerObj.transform.position.x - transform.position.x, playerObj.transform.position.y - transform.position.y);
        target.Normalize();
        target = target*shootSpeed;
        

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(target, ForceMode2D.Impulse);
        bullet.GetComponent<BulletController>().damage = this.damage;
    }
}

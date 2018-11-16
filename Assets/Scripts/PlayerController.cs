using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
	protected float speed, shootSpeed, shootCD, shootDmg, health, invincibilityTime;
    [SerializeField]
    protected GameObject bulletPrefab, bulletSpawn;

    Animator anim;
    float lastX, lastY;
    private AudioSource audioSrc;
    private float timeAfterShoot, invincibilityCount;
    private Vector2 shootForce = new Vector2(0f, 0f);
    private bool isInvincible;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isInvincible = false;
        timeAfterShoot = 0.0f;
        invincibilityCount = 0f;
    }
    // Use this for initialization
    void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {
        if (isInvincible)
            if (invincibilityTime >= invincibilityCount)
                isInvincible = false;
            else
                invincibilityCount += Time.deltaTime;
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
        movement.Normalize();
		Move(movement);

        if (shootCD < timeAfterShoot)
        {
            float shootHorizontal = Input.GetAxisRaw("HorizontalShoot");
            float shootVertical = Input.GetAxisRaw("VerticalShoot");
            Vector2 shootDir = new Vector2(shootHorizontal, shootVertical);
            shootDir.Normalize();
            Shoot(shootDir, movement);
            timeAfterShoot = 0f;
        }
        else
            timeAfterShoot += Time.fixedDeltaTime;
	}

	protected void Move(Vector2 movement){
		transform.position += (Vector3)movement*speed;
        UpdateAnimation(movement);
		/*if(movement != Vector2.zero){
			movement.x *= 100000; //favorece olhar na horizontal (elimina olhar diagonal)
			transform.up = movement;	
		}*/
	}

    protected void Shoot(Vector2 shootDir, Vector2 movementDir)
    {
        bool willShoot = false;
        if (shootDir.x > 0.01f)
        {
            shootForce = new Vector2(shootSpeed, 0f);
            willShoot = true;
        }
        else if (shootDir.x < - 0.01f)
        {
            shootForce = new Vector2(-shootSpeed, 0f);
            willShoot = true;
        }
        else if (shootDir.y > 0.01f)
        {
            shootForce = new Vector2(0f, shootSpeed);
            willShoot = true;
        }
        else if (shootDir.y < - 0.01f)
        {
            shootForce = new Vector2(0f, -shootSpeed);
            willShoot = true;
        }
        if (willShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(shootForce + movementDir, ForceMode2D.Impulse);
            bullet.GetComponent<BulletController>().damage = this.shootDmg;
        }
    }

    protected void UpdateAnimation(Vector2 movement)
    {
        if(movement.x == 0f && movement.y == 0f)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("IsMoving", false);
        }
        else
        {
            lastX = movement.x;
            lastY = movement.y;
            anim.SetBool("IsMoving", true);
        }
        anim.SetFloat("DirX", movement.x);
        anim.SetFloat("DirY", movement.y);
    }

    public void PlayGetkey()
    {
        audioSrc.PlayOneShot(audioSrc.clip, 1.0f);
    }

    public void ReceiveDamage(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            if (health <= 0)
            {
                //TODO KILL
                Debug.Log("RIP");
            }
            isInvincible = true;
            invincibilityCount = 0f;
        }
    }

}

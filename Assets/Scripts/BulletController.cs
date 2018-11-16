using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField]
    private AudioClip popSnd;
    private AudioSource audioSrc;

    private bool canDestroy;
    public float damage;
    // Use this for initialization
    void Awake () {
        canDestroy = false;
        audioSrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!audioSrc.isPlaying && canDestroy)
        {
            Debug.Log("Stopped playing");
            Destroy(gameObject);
        }
    }

    public void DestroyBullet()
    {
        Debug.Log("Destroying Bullet");
        audioSrc.PlayOneShot(popSnd, 1.0f);
        canDestroy = true;
    }
}

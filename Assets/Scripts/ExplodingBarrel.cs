using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour
{
    public float explosionSize;
    private bool ignited;
    private Animator anim;
    private SpriteRenderer sr;
    private Color blank, flash;
    private RaycastHit2D hit;
    private ParticleSystem ps;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        ps = this.GetComponent<ParticleSystem>();
        sound = this.GetComponent<AudioSource>();
        blank = Color.white;
        flash = new Color(255, 98, 98);
        ps.Stop();
        ignited = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage()
    {
        if (!ignited)
        StartCoroutine("Explode");
    }

   private IEnumerator Explode()
    {
        ignited = true;
        gameObject.GetComponent<Animator>().SetTrigger("ignite");
        sound.Play();
        yield return new WaitForSeconds(5f);
      foreach ( RaycastHit2D hit in Physics2D.BoxCastAll(this.gameObject.transform.position, new Vector2(explosionSize,explosionSize), 0f, new Vector2(0,0), 1f))
      {
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.BroadcastMessage("OnDamage", 3);
                
            }
            else
            {
                hit.collider.gameObject.BroadcastMessage("OnDamage");
            } 
      }
       this.gameObject.SetActive(false);
    }
}

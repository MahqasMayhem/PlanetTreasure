using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour
{
    public float explosionSize;
    private Animator anim;
    private SpriteRenderer sr;
    private Color blank, flash;
    private RaycastHit2D hit;
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        ps = this.GetComponent<ParticleSystem>();
        blank = Color.white;
        flash = new Color(255, 98, 98);
        ps.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamage()
    {
        StartCoroutine("Explode");
    }

   private IEnumerator Explode()
    {
      for (int i = 0; i < 5; i++)
      {
            sr.color = blank;
            yield return new WaitForSeconds(0.8f);
            sr.color = flash;
             yield return new WaitForSeconds(0.8f);
            ps.Play();
      }
      foreach ( RaycastHit2D hit in Physics2D.BoxCastAll(this.gameObject.transform.position, new Vector2(explosionSize,explosionSize), 0f, new Vector2(0,0), 1f))
      {
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.BroadcastMessage("OnDamage");
                hit.collider.gameObject.SetActive(false);
            }
            else
            {
                hit.collider.gameObject.BroadcastMessage("OnDamage");
            } 
      }
       this.gameObject.SetActive(false);
    }
}

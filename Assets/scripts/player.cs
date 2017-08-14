using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    private Rigidbody2D rb20;
     float horinzontal;
    [SerializeField]
    private float velocidade = 0;
    private bool ladoreito;
    private Animator animator;
	void Start () {
        rb20 = GetComponent<Rigidbody2D>();
        ladoreito = transform.localScale.x > 0;
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate() {
      
        horinzontal = Input.GetAxis("Horizontal");
        Movimentar(horinzontal);
        mudardirecao(horinzontal);


    }
    private void Movimentar(float h)
    {
        rb20.velocity = new Vector2(h * velocidade,rb20.velocity.y);
        animator.SetFloat("velocidade", Mathf.Abs(h));
        

    }
    private void mudardirecao(float horizontal)
    {
        if (horinzontal > 0 && !ladoreito || horinzontal < 0 && ladoreito)
        {
            ladoreito = !ladoreito;
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.position.z);
        }
    }
}

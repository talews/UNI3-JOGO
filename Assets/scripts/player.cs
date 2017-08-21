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
    public bool estaNaParede;
    public float raioparede;
    private bool acao;
    public GameObject posicaoprojetil;
    public GameObject projetil; 
   
    //Jump
    public LayerMask plataforma;
    public Vector2 pontoColisaoPiso = Vector2.zero;
    public Vector2 pontocolisaoparede = Vector2.zero;
    public bool estaNoChao;
    public float forcapulo = 500;
    public float raio;
    public Color debugColisao = Color.red;
    void Start () {
        rb20 = GetComponent<Rigidbody2D>();
        ladoreito = transform.localScale.x > 0;
        animator = GetComponent<Animator>();

	}
	
	void FixedUpdate() {
      
        horinzontal = Input.GetAxis("Horizontal");
        Movimentar(horinzontal);
        mudardirecao(horinzontal);
        EstaNoChao();
        ControleEntrada();
        controlarLayer();
        Acao();
        resetar();
    }
   

    private void Pular()
    {
        rb20.gravityScale = 1.6f;
         if(estaNoChao && rb20.velocity.y <= 0)
        {
            rb20.AddForce(new Vector2(0, forcapulo));
            animator.SetTrigger("Pular");
        }
    }
    private void Cair()
    {
        if (!estaNoChao && rb20.velocity.y <= 0)
        {
            animator.SetBool("Cair", true);
            animator.ResetTrigger("Pular");
            
        }
        if (estaNoChao )
        {
            animator.SetBool("Cair", false);
        }
    }
   private void EstaNoChao()
    {
        var pontoPosicao = pontoColisaoPiso;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        estaNoChao = Physics2D.OverlapCircle(pontoPosicao,raio,plataforma);
        Cair();
    }
    private void ControleEntrada()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Pular();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            acao = true;
        }
    }
    void resetar()
    {
        acao = false;
    }
    private void Movimentar(float h)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("atirar")) 
        {
            rb20.velocity = new Vector2(h * velocidade, rb20.velocity.y);
        }
        
        animator.SetFloat("velocidade", Mathf.Abs(h));
        if (rb20.velocity.y == 0)
        {
            rb20.gravityScale = 1.0f;
        }

    }
    private void mudardirecao(float horizontal)
    {
        if (horinzontal > 0 && !ladoreito || horinzontal < 0 && ladoreito)
        {
            ladoreito = !ladoreito;
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.position.z);
        }
}

 void   Acao()
    {
        if (acao && !animator.GetCurrentAnimatorStateInfo(0).IsTag("atirar"))
        {
            animator.SetTrigger("atirar");
            rb20.velocity = Vector2.zero;
            Acaoatirar();
        }
    }
    private void Acaoatirar()
    {
        GameObject tmprojetil = (GameObject)(Instantiate(projetil,posicaoprojetil.transform.position, Quaternion.identity));
        if (ladoreito)
        {
            tmprojetil.GetComponent<Bala>().inicializar(Vector2.right);
        }
        else
        {
            tmprojetil.GetComponent<Bala>().inicializar(Vector2.left);
        }
    }

        





    private void OnDrawGizmos()
    {
        Gizmos.color = debugColisao;
        var pontoPosicao = pontoColisaoPiso;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        var colisaoparede = pontocolisaoparede;
        colisaoparede.x += transform.position.x;
        colisaoparede.y += transform.position.y;

        estaNoChao = Physics2D.OverlapCircle(pontoPosicao, raio, plataforma);
        estaNaParede = Physics2D.OverlapCircle(colisaoparede,raioparede, plataforma);


        Gizmos.DrawSphere(pontoPosicao, raio);
        Gizmos.DrawSphere(colisaoparede, raioparede);
    }
    void controlarLayer()
    {
        if (!estaNoChao)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

}


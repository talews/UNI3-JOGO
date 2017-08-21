using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
    [SerializeField]
    private float velocidade;
    private Vector2 direcao;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject explosao;
    private float tempo = 0.08f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = direcao * velocidade;
	}
    public void inicializar(Vector2 _direcao)
    {
        direcao = _direcao;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.tag == "Inimigo")
        {
            explosao.SetActive(true);
            StartCoroutine("Destruir");
            
        }
    }
    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(tempo);
        DestroyObject(gameObject);
    }
        
}

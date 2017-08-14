using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSeguir : MonoBehaviour {

    [SerializeField]
    private float MaximoX;
    [SerializeField]
    private float MinX;
    [SerializeField]
    private float MaximoY;
    [SerializeField]
    private float MinY;

    public Transform player;
	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, MinX, MaximoX), Mathf.Clamp(player.position.y, MinY, MaximoY),transform.position.z);
		
	}
}

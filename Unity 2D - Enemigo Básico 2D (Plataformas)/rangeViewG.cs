using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeViewG : MonoBehaviour
{
    public Animator ani;
    public patrulleoEnemy script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("player"))
        {
            ani.SetBool("run", false);
            ani.SetBool("walk", false);
            ani.SetBool("Aattack", true);
            script.atacando = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

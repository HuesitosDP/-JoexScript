using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrulleoEnemy : MonoBehaviour
{
    int rutina;
    float cronometro;
    public Animator ani;
    int direccion;
    public float walkS;
    public float runS;
    public GameObject target;
    public bool atacando;
    public float tiempoEE;

    public enemigo enemyScr;
    public LayerMask playerL;
    public float attR;
    public Transform attP;

    public float rangoV;
    public float rangoA;
    public GameObject rango;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comportamientos();
    }

    public void comportamientos()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoV && !atacando)
        {
            ani.SetBool("run", false);
            cronometro += Time.deltaTime;
            if (cronometro >= tiempoEE)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;
                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.localScale = Vector3.one;
                            transform.Translate(Vector3.right * walkS * Time.deltaTime);
                            break;
                        case 1:
                            transform.localScale = new Vector3(-1, 1, 1);
                            transform.Translate(Vector3.left * walkS * Time.deltaTime);
                            break;
                    }
                    ani.SetBool("walk", true);
                    break;
            }   
        }
        else 
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangoA && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.right * runS * Time.deltaTime);
                    transform.localScale = Vector3.one;
                    ani.SetBool("Aattack", false);
                }
                else
                {
                    ani.SetBool("walk", false);
                    ani.SetBool("run", true);
                    transform.Translate(Vector3.left * runS * Time.deltaTime);
                    transform.localScale = new Vector3(-1, 1, 1);
                    ani.SetBool("Aattack", false);
                }
            }
            else if (!atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    gameObject.transform.localScale = Vector3.one;
                }
                else
                {
                    gameObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                ani.SetBool("walk", false);
                ani.SetBool("run", false);
            }
        }
    }
    public void finAni()
    {
        ani.SetBool("Aattack", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void overlapeateElCircle()
    {
        Collider2D[] hitPl = Physics2D.OverlapCircleAll(attP.position, attR, playerL);
        foreach (Collider2D player in hitPl)
        {
            player.GetComponent<vidaManagment>().autodañarme(enemyScr.daños);
        }
    }

    private void OnDrawGizmos()
    {
        if (attP == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attP.position, attR);
    }
}

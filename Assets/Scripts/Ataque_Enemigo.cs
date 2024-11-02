using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Enemigo : MonoBehaviour
{
    [SerializeField] private Transform controlador_Ataque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpe;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlador_Ataque.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                //GameObject.FindGameObjectsWithTag("Salida_1").GetComponent<Salida_Level1>().EnemigoEliminado();
                colisionador.transform.GetComponent<Vida_Player>().TomarDano(danoGolpe);
                //Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controlador_Ataque.position, radioGolpe);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Maximus : MonoBehaviour
{
    [SerializeField] private Transform controlador_Ataque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpe;


    
    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlador_Ataque.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if(colisionador.CompareTag("Enemigo") || colisionador.CompareTag("Trampa"))
            {
                colisionador.transform.GetComponent<Vida_Enemigo>().TomarDano(danoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controlador_Ataque.position, radioGolpe);
    }
}

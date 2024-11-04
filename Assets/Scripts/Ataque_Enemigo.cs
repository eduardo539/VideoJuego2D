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
                // Intentar obtener el componente Vida_Player
                Vida_Player vidaJugador = colisionador.GetComponent<Vida_Player>();

                if (vidaJugador != null)
                {
                    // Si el objeto tiene Vida_Player, causarle daño
                    vidaJugador.TomarDano(danoGolpe);
                }
                else
                {
                    // Intentar obtener un componente de vida aria
                    Vida_Aria aria = colisionador.GetComponent<Vida_Aria>();
                    
                    if (aria != null)
                    {
                        // Si el objeto tiene aria, causarle daño
                        aria.TomarDano(danoGolpe);
                    }
                    else
                    {
                        Debug.LogWarning("El objeto colisionado no tiene un componente de vida válido.");
                    }
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controlador_Ataque.position, radioGolpe);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano_Tocar : MonoBehaviour
{
    public int danoContacto; // Daño que causa el ataque

    // Método que se activa al colisionar con otro objeto
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Intentar obtener el componente Vida_Player
            Vida_Player jugador = other.gameObject.GetComponent<Vida_Player>();

            if (jugador != null)
            {
                // Si el objeto tiene Vida_Player, causarle daño
                jugador.TomarDano(danoContacto, other.GetContact(0).normal);
            }
            else
            {
                // Intentar obtener otro componente de vida aria
                Vida_Aria aria = other.gameObject.GetComponent<Vida_Aria>();
                
                if (aria != null)
                {
                    // Si el objeto tiene aria, causarle daño
                    aria.TomarDano(danoContacto, other.GetContact(0).normal);
                }
                else
                {
                    Debug.LogWarning("El objeto colisionado no tiene un componente de vida válido.");
                }
            }
        }
    }
}

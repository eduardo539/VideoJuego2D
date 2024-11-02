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
            Vida_Player jugador = other.gameObject.GetComponent<Vida_Player>();

            if (jugador != null)
            {
                // Si el goblin está en estado de ataque, causarle daño al jugador
                jugador.TomarDano(danoContacto, other.GetContact(0).normal);
            }
            else
            {
                Debug.LogWarning("El objeto colisionado no tiene un componente Vida_Player.");
            }
        }

    }
}

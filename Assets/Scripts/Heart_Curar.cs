using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Curar : MonoBehaviour
{
    [SerializeField] public float cantidadCura; // La cantidad de vida que el corazón restaurará

    // Método para detectar colisiones con el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vida_Player jugador = other.gameObject.GetComponent<Vida_Player>();

        // Solo curamos si la vida no está al máximo
        if (jugador != null && jugador.VidaNoMaxima())
        {
            jugador.Curar(cantidadCura); // Curar al jugador
            Destroy(gameObject); // Destruir el corazón solo si el jugador se curó
        }
        else
        {
            Debug.Log("Vida al máximo, no se puede recoger el corazón.");
        }
    }
}

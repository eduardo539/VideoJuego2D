using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Script : MonoBehaviour
{

    private Puntaje_Script dropsPuntos;

    [SerializeField] private float puntosEntrada;



    void Start()
    {
        // Encuentra el objeto que contiene el script Puntaje_Script en la escena
        dropsPuntos = FindObjectOfType<Puntaje_Script>();

        // Verifica si se encontró el script de puntaje
        if (dropsPuntos == null)
        {
            Debug.LogError("No se encontró el script Puntaje_Script en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que tocó la fruta tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            dropsPuntos.SumarPuntos(puntosEntrada);
            // Destruye el objeto de la fruta
            Destroy(gameObject);
        }
    }
}

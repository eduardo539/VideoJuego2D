using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas_Script : MonoBehaviour
{

    private Puntaje_Script frutasPuntos;

    [SerializeField] private float puntosEntrada;

    void Start()
    {
        // Encuentra el objeto que contiene el script Puntaje_Script en la escena
        frutasPuntos = FindObjectOfType<Puntaje_Script>();

        // Verifica si se encontró el script de puntaje
        if (frutasPuntos == null)
        {
            Debug.LogError("No se encontró el script Puntaje_Script en la escena.");
        }
    }

    // Método que se llama cuando otro objeto entra en el trigger del collider de la fruta
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que tocó la fruta tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            frutasPuntos.SumarPuntos(puntosEntrada);
            // Destruye el objeto de la fruta
            Destroy(gameObject);
        }
    }
}

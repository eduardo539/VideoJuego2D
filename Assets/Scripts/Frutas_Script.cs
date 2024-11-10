using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas_Script : MonoBehaviour
{

    private Puntaje_Script frutasPuntos;

    [SerializeField] private float puntosEntrada;

    void Awake()
    {
        frutasPuntos = FindObjectOfType<Puntaje_Script>();
        if (frutasPuntos == null)
        {
            Debug.LogError("No se encontró el script Puntaje_Script en la escena.");
        }
    }

    // Método que se llama cuando otro objeto entra en el trigger del collider de la fruta
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (frutasPuntos != null)
            {
                frutasPuntos.SumarPuntos(puntosEntrada);
            }
            else
            {
                Debug.LogWarning("No se puede sumar puntos porque frutasPuntos no está inicializado.");
            }

            Destroy(gameObject);
        }
    }


}


/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (frutasPuntos != null)
            {
                frutasPuntos.SumarPuntos(puntosEntrada);
            }
            else
            {
                Debug.LogWarning("No se puede sumar puntos porque frutasPuntos no está inicializado.");
            }

            Destroy(gameObject);
        }
    }
*/
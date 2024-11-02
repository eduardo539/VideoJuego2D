using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Script : MonoBehaviour
{
    public GameObject Maximus; // Referencia al personaje
    private float limiteIzquierdo; // Límite izquierdo basado en la posición inicial de la cámara
    public float limiteDerecho = 30f; // Límite derecho ajustable de la cámara

    void Start()
    {
        // Establecer el límite izquierdo en la posición inicial de la cámara
        limiteIzquierdo = transform.position.x;
    }

    void Update()
    {
        if (Maximus != null) // Verificar si Maximus todavía existe
        {
            Vector3 position = transform.position;

            // Seguir la posición del personaje en el eje X
            position.x = Maximus.transform.position.x;

            // Limitar el movimiento de la cámara entre el límite izquierdo (fijo) y derecho (ajustable)
            position.x = Mathf.Clamp(position.x, limiteIzquierdo, limiteDerecho);

            // Asignar la nueva posición a la cámara
            transform.position = position;
        }
        else
        {
            // Opcional: puedes agregar algún comportamiento aquí si Maximus se destruye,
            // como detener el movimiento de la cámara o cambiar de objetivo.
        }
    }
}

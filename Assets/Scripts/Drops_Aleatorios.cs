using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops_Aleatorios : MonoBehaviour
{
    [SerializeField] private GameObject[] objetosDrop;  // Array de prefabs de objetos que se pueden soltar
    [SerializeField] private float probabilidadDrop = 0.5f;  // Probabilidad de drop (por ejemplo, 0.5f para 50%)
    [SerializeField] private LayerMask tilemapLayer; // Capa del Tilemap para el raycast

    // Método para instanciar el drop cuando el enemigo muere
    public void SoltarDrop()
    {
        // Genera un número aleatorio entre 0 y 1, y verifica si está dentro de la probabilidad
        if (Random.value <= probabilidadDrop)
        {
            // Selecciona un objeto aleatorio del array de drops
            GameObject objetoSuelto = objetosDrop[Random.Range(0, objetosDrop.Length)];

            // Realiza un raycast hacia abajo desde la posición actual para detectar el tilemap
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, tilemapLayer);

            if (hit.collider != null)
            {
                // Ajusta la posición del objeto drop justo encima del tilemap
                Vector2 posicionSobreTilemap = new Vector2(hit.point.x, hit.point.y + 0f); // Añade 0f para que quede justo sobre el tilemap
                Instantiate(objetoSuelto, posicionSobreTilemap, Quaternion.identity);
            }
            else
            {
                // Si no hay tilemap debajo, instancia el objeto en la posición actual del enemigo
                Instantiate(objetoSuelto, transform.position, Quaternion.identity);
            }
        }
    }
}

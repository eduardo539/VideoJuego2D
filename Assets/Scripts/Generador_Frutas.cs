using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador_Frutas : MonoBehaviour
{
    public GameObject[] frutas; // Array de prefabs de frutas
    [SerializeField] private int cantidadMinima; // Cantidad mínima de frutas a generar
    [SerializeField] private int cantidadMaxima; // Cantidad máxima de frutas a generar
    [SerializeField] private float areaMinX; // Límite mínimo en X
    [SerializeField] private float areaMaxX; // Límite máximo en X
    [SerializeField] private LayerMask tilemapLayer; // Layer del Tilemap Collider
    [SerializeField] private float distanciaMinimaEntreFrutas = 1.5f; // Distancia mínima entre frutas
    [SerializeField] private float alturaSobreTilemap = 0.5f; // Altura mínima sobre el tilemap

    private List<Vector2> posicionesGeneradas = new List<Vector2>(); // Lista de posiciones de frutas generadas

    void Start()
    {
        GenerarFrutasAleatorias();
    }

    void GenerarFrutasAleatorias()
    {
        int cantidadFrutas = Random.Range(cantidadMinima, cantidadMaxima);

        for (int i = 0; i < cantidadFrutas; i++)
        {
            GameObject frutaAleatoria = frutas[Random.Range(0, frutas.Length)];
            Vector2 posicionAleatoria = Vector2.zero; // Inicializa la variable con Vector2.zero

            // Genera posiciones hasta encontrar una adecuada
            int intentos = 0;
            do
            {
                float posicionAleatoriaX = Random.Range(areaMinX, areaMaxX);

                // Raycast hacia abajo desde un punto alto en el eje Y
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(posicionAleatoriaX, 10f), Vector2.down, Mathf.Infinity, tilemapLayer);

                // Verifica si el Raycast impacta en el Tilemap Collider
                if (hit.collider != null)
                {
                    // Ajusta la posición ligeramente por encima del Tilemap
                    posicionAleatoria = new Vector2(posicionAleatoriaX, hit.point.y + alturaSobreTilemap);
                }
                else
                {
                    continue;
                }

                // Incrementa el contador de intentos
                intentos++;
                
            } while (!EsPosicionValida(posicionAleatoria) && intentos < 10); // Intenta hasta 10 veces si es necesario

            // Agrega la posición válida y genera la fruta
            posicionesGeneradas.Add(posicionAleatoria);
            Instantiate(frutaAleatoria, posicionAleatoria, Quaternion.identity);
        }
    }

    // Verifica si la nueva posición está suficientemente lejos de las frutas generadas
    bool EsPosicionValida(Vector2 nuevaPosicion)
    {
        foreach (Vector2 posicion in posicionesGeneradas)
        {
            if (Vector2.Distance(posicion, nuevaPosicion) < distanciaMinimaEntreFrutas)
            {
                return false;
            }
        }
        return true;
    }
}

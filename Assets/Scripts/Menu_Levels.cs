using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Levels : MonoBehaviour
{
    public Button[] botones;           // Array de botones del menú
    public RectTransform selector;     // Referencia al objeto selector
    private int indiceActual = 0;      // Índice del botón actualmente seleccionado

    void Start()
    {
        // Inicializa el selector en el primer botón
        MoverSelector();
    }

    void Update()
    {
        // Navegar hacia la izquierda con la tecla A
        if (Input.GetKeyDown(KeyCode.A))
        {
            indiceActual = (indiceActual > 0) ? indiceActual - 1 : botones.Length - 1;
            MoverSelector();
        }

        // Navegar hacia la derecha con la tecla D
        if (Input.GetKeyDown(KeyCode.D))
        {
            indiceActual = (indiceActual < botones.Length - 1) ? indiceActual + 1 : 0;
            MoverSelector();
        }

        // Seleccionar nivel con la tecla Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SeleccionarNivel();
        }
    }

    void MoverSelector()
    {
        // Posiciona el selector sobre el botón actualmente seleccionado usando su posición en la cuadrícula
        if (selector != null && botones.Length > 0)
        {
            selector.position = botones[indiceActual].transform.position;
        }
    }

    void SeleccionarNivel()
    {
        // Llama a la función de cambio de nivel usando el nombre del nivel o índice asociado al botón actual
        string numeroNivel = botones[indiceActual].name; // O usa una propiedad específica si tienes nombres personalizados
        CambiarNivel(numeroNivel);
    }

    public void CambiarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void CambiarNivel(int numeroNivel)
    {
        SceneManager.LoadScene(numeroNivel);
    }
}









/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Levels : MonoBehaviour
{
    public void CambiarNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void CambiarNivel(int numeroNivel)
    {
        SceneManager.LoadScene(numeroNivel);
    }
}
*/
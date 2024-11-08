using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Menu_Principal : MonoBehaviour
{
    [SerializeField] public Button[] botones;           // Array de botones del menú
    public RectTransform selector;     // Referencia al objeto selector
    private int indiceActual = 0;      // Índice del botón actualmente seleccionado

    public event EventHandler alertaNewJuego;

    private Puntaje_Script puntaje;



    void Start()
    {
        puntaje = FindObjectOfType<Puntaje_Script>();
        // Inicializa el selector en el primer botón
        MoverSelector();
    }

    void Update()
    {
        // Navegar hacia arriba con la tecla W
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (indiceActual > 0) // Verifica que no esté en el primer botón
            {
                indiceActual--; // Mueve el selector hacia arriba
                MoverSelector();
            }
        }

        // Navegar hacia abajo con la tecla S
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (indiceActual < botones.Length - 1) // Verifica que no esté en el último botón
            {
                indiceActual++; // Mueve el selector hacia abajo
                MoverSelector();
            }
        }

        // Seleccionar el botón correspondiente al selector con la tecla Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EjecutarBotonSeleccionado();
        }

        // Mover el selector según la posición del ratón
        MoverSelectorConMouse();
    }

    void MoverSelector()
    {
        // Posiciona el selector a la izquierda del botón actualmente seleccionado
        if (selector != null && botones.Length > 0)
        {
            // Obtiene la posición del botón seleccionado
            Vector3 botonPos = botones[indiceActual].transform.position;

            // Ajusta la posición del selector a la izquierda del botón
            selector.position = new Vector3(botonPos.x - (selector.rect.width / 2) - 130f, botonPos.y, botonPos.z); // Ajusta 10f para el espaciado
        }
    }

    void MoverSelectorConMouse()
    {
        // Verifica si el puntero del ratón está sobre algún botón
        for (int i = 0; i < botones.Length; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(botones[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                // Actualiza el índice actual y mueve el selector
                if (indiceActual != i)
                {
                    indiceActual = i;
                    MoverSelector();
                }
                break; // Salimos del bucle si encontramos un botón
            }
        }
    }

    public void EjecutarBotonSeleccionado()
    {
        // Llama al método onClick del botón actualmente seleccionado
        botones[indiceActual].onClick.Invoke();
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Menu_Niveles");
    }

    public void NuevoJuego()
    {
        alertaNewJuego?.Invoke(this, EventArgs.Empty);
        //puntaje.ReiniciarPuntaje();
        //SceneManager.LoadScene("Menu_Personajes");
    }


    public void Aceptar()
    {
        puntaje.ResetearPuntosTemporales();
        SceneManager.LoadScene("Menu_Personajes");
    }


    public void Puntajes()
    {
        SceneManager.LoadScene("Puntajes");
    }

    public void Instrucciones()
    {
        SceneManager.LoadScene("Menu_Instrucciones");
    }

    public void Salir()
    {
        Application.Quit(); // Esto cierra la aplicación
    }
}








/* selector para el lado derecho
    void MoverSelector()
    {
        // Posiciona el selector al lado del botón actualmente seleccionado
        if (selector != null && botones.Length > 0)
        {
            // Obtiene la posición del botón seleccionado
            Vector3 botonPos = botones[indiceActual].transform.position;
            
            // Ajusta la posición del selector a la derecha del botón
            selector.position = new Vector3(botonPos.x + 50f, botonPos.y, botonPos.z); // Cambia 50f al valor que desees para el espaciado
        }
    }
*/
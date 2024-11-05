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

        // Mover el selector según la posición del ratón
        MoverSelectorConMouse();
    }

    void MoverSelector()
    {
        // Posiciona el selector en el centro del botón actualmente seleccionado
        if (selector != null && botones.Length > 0)
        {
            // Obtiene la posición del botón seleccionado
            Vector3 botonPos = botones[indiceActual].transform.position;

            // Ajusta la posición del selector al centro del botón
            selector.position = new Vector3(botonPos.x, botonPos.y, botonPos.z); // Centra el selector
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


    void SeleccionarNivel()
    {
        // Llama al método onClick del botón actualmente seleccionado
        botones[indiceActual].onClick.Invoke();
    }

    public void Nivel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Nivel2()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Nivel3()
    {
        SceneManager.LoadScene("Level_3");
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu_Principal");
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
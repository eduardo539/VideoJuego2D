using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPause;
    [SerializeField] private GameObject menuPausa;


    private Puntaje_Script puntaje;

    void Start()
    {
        puntaje = FindObjectOfType<Puntaje_Script>();
        // Asegúrate de que el juego esté en tiempo normal y el menú de pausa esté oculto
        Time.timeScale = 1f;
        botonPause.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPause.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPause.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Debug.Log("Regresando al menú de niveles");
        puntaje.ResetearPuntosTemporales();
        SceneManager.LoadScene("Menu_Niveles"); // Cambia "Menu_Niveles" por el nombre exacto de tu escena de menú de niveles
    }
}

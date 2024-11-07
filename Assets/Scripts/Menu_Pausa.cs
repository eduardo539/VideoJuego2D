using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPause;
    [SerializeField] private GameObject menuPausa;

    void Start()
    {
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
        Debug.Log("Cerrando el juego");
        Application.Quit();
    }
}

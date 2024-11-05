using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Instrucciones : MonoBehaviour
{
    public Button[] botones; // Array de botones del menú

    void Start()
    {
        // Verifica que haya al menos un botón en el array y le asigna la función Regresar al primer botón
        if (botones.Length > 0 && botones[0] != null)
        {
            botones[0].onClick.AddListener(Regresar);
        }
        else
        {
            Debug.LogWarning("No se encontró ningún botón en el array.");
        }
    }

    public void Regresar()
    {
        SceneManager.LoadScene("Menu_Principal");
    }
}

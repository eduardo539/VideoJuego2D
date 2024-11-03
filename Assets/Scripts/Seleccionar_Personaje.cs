using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Seleccionar_Personaje : MonoBehaviour
{

    private int index;

    [SerializeField] private Image imagen;

    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI danoAtaque;

    [SerializeField] private TextMeshProUGUI ataque;

    [SerializeField] private TextMeshProUGUI equipamiento;

    private GameManager gameManager;

    private void Start(){
        gameManager = GameManager.Instance;

        index = PlayerPrefs.GetInt("JugadorIndex");

        if(index > gameManager.personajes.Count - 1)
        {
            index = 0;
        }

        CamviarPantalla();
    }

    private void CamviarPantalla()
    {
        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = gameManager.personajes[index].imagen;
        nombre.text = gameManager.personajes[index].nombre;
        danoAtaque.text = gameManager.personajes[index].danoAtaque;
        ataque.text = gameManager.personajes[index].ataque;
        equipamiento.text = gameManager.personajes[index].equipamiento;
    }

    public void SiguientePersonaje()
    {
        if(index == gameManager.personajes.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }

        CamviarPantalla();
    }

    public void AnteriorPersonaje()
    {
        if(index == 0)
        {
            index = gameManager.personajes.Count - 1;
        }
        else
        {
            index -= 1;
        }

        CamviarPantalla();
    }

    public void VolverAtras()
    {
        Debug.Log("Este boton es para regresar");
    }

    public void IniciarJuego()
    {
        //Debug.Log("Este es para seleccionar el personaje");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

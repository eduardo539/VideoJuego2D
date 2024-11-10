using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagerScript : MonoBehaviour
{
    private static AudioManagerScript instance;
    private AudioSource audioSource;

    // Lista de nombres de escenas donde el audio del menú debería detenerse
    [SerializeField] private List<string> escenasNiveles;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Hace que el objeto no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye la nueva
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Play(); // Reproduce el audio al inicio
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de cambio de escena
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribirse del evento de cambio de escena
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena actual es un nivel y pausa el audio
        if (escenasNiveles.Contains(scene.name))
        {
            audioSource.Pause(); // Pausa el audio del menú en las escenas de nivel
        }
        else
        {
            // Si es una escena de menú, reanuda el audio
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

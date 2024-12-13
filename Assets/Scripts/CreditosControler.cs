using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importa el espacio de nombres para TextMeshPro

public class CreditosControler : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup; // CanvasGroup para controlar la opacidad
    [SerializeField] private TextMeshProUGUI presionaTeclaTexto; // TextMeshProUGUI para mostrar el texto
    [SerializeField] private float fadeInDuration = 2f; // Duración del fade-in
    [SerializeField] private float fadeOutDuration = 2f; // Duración del fade-out
    [SerializeField] private string siguienteEscena = "Juego"; // Nombre de la escena siguiente

    private bool puedeIniciar = false; // Controla si ya se puede pasar a la siguiente escena

    void Start()
    {
        // Inicializa el CanvasGroup en transparente
        canvasGroup.alpha = 0;
        presionaTeclaTexto.enabled = false; // Oculta el texto al inicio

        // Inicia la animación de fade-in
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        // Espera a que el fade-in termine antes de permitir la entrada del jugador
        if (puedeIniciar && Input.anyKeyDown)
        {
            // Inicia el fade-out y carga la siguiente escena
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        // Aumenta la opacidad del CanvasGroup gradualmente
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            yield return null;
        }

        // Activa el texto después del fade-in
        presionaTeclaTexto.enabled = true;
        puedeIniciar = true; // Ahora el jugador puede presionar una tecla
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        // Reduce la opacidad del CanvasGroup gradualmente
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeOutDuration));
            yield return null;
        }

        // Carga la siguiente escena
        SceneManager.LoadScene(siguienteEscena);
    }
}

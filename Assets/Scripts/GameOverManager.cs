
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject painelGameOver;
    public TMP_Text textoPontuacaoFinal;

    private bool terminou = false;

    void Start()
    {
        Time.timeScale = 1f;

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
    }

    public void VoltarAoInicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }

    public void GameOver()
    {
        if (terminou)
            return;

        terminou = true;

        // Para a cobra na posição da colisão
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        if (player != null)
            player.PararCobra();

        // Guarda a pontuação final
        int pontos = 0;

        if (Pontuacao.Instancia != null)
            pontos = Pontuacao.Instancia.Pontos;

        if (textoPontuacaoFinal != null)
            textoPontuacaoFinal.text = "Pontuação final: " + pontos;

        // Toca o som de derrota
        if (AudioManager.Instancia != null)
            AudioManager.Instancia.TocarSomDerrota();

        // Inicia a pausa de 4 segundos
        StartCoroutine(MostrarGameOverDepoisDaPausa());
    }

    IEnumerator MostrarGameOverDepoisDaPausa()
    {
        // Congela o jogo, mantendo a cobra na posição da morte
        Time.timeScale = 0f;

        // Espera 4 segundos em tempo real
        yield return new WaitForSecondsRealtime(3f);

        // Mostra o painel de Game Over
        if (painelGameOver != null)
            painelGameOver.SetActive(true);
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}

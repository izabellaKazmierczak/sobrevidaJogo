
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

    public void GameOver()
    {
        if (terminou)
            return;

        terminou = true;

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();

        if (player != null)
            player.PararCobra();

        int pontos = 0;

        if (Pontuacao.Instancia != null)
            pontos = Pontuacao.Instancia.Pontos;

        if (textoPontuacaoFinal != null)
            textoPontuacaoFinal.text = "Pontuação final: " + pontos;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
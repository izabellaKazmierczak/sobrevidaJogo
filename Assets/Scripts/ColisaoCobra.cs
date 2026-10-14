
using UnityEngine;
using System.Collections;

public class ColisaoCobra : MonoBehaviour
{
    private GameOverManager gameOverManager;
    private Transform cabeca;
    private bool podeColidir = false;

    void Start()
    {
        gameOverManager = FindFirstObjectByType<GameOverManager>();

        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        if (jogador != null)
            cabeca = jogador.transform;

        StartCoroutine(LiberarColisao());
    }

    IEnumerator LiberarColisao()
    {
        // Dá tempo para a cabeça se afastar da nova parte.
        yield return new WaitForSeconds(1f);
        podeColidir = true;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (!podeColidir)
            return;

        if (colisao.gameObject.CompareTag("Player"))
        {
            if (gameOverManager != null)
                gameOverManager.GameOver();
        }
    }
}

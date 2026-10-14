using UnityEngine;

public class ColisaoParede : MonoBehaviour
{
    private GameOverManager gameOverManager;

void Start()
    {
        gameOverManager = FindFirstObjectByType<GameOverManager>();
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        // Só encerra o jogo se a cabeça da cobra bater na parede.
        if (colisao.gameObject.CompareTag("Player"))
        {
            if (gameOverManager != null)
            {
                gameOverManager.GameOver();
            }
        }
    }
}


using UnityEngine;

public class Comida : MonoBehaviour
{
    public bool comidaLenta = false;

    private bool foiComida = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (foiComida || !other.CompareTag("Player"))
            return;

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        foiComida = true;

        FoodSpawner spawner = FindFirstObjectByType<FoodSpawner>();

        if (comidaLenta)
        {
            player.DiminuirVelocidade();

            if (spawner != null)
                spawner.ComidaLentaFoiComida();
        }
        else
        {
            player.Crescer();
            player.AumentarVelocidade();

            if (Pontuacao.Instancia != null)
                Pontuacao.Instancia.AdicionarPontos(1);
            else
                Debug.LogError("Pontuacao.Instancia está nula!");

            if (spawner != null)
                spawner.ComidaFoiComida();
        }

        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject comidaLentaPrefab;

    public float limiteX = 4f;
    public float limiteY = 4f;

    public int comidasParaFase2 = 10;

    private int comidasComidas = 0;
    private bool comidaLentaAtiva = false;

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        if (foodPrefab == null)
        {
            Debug.LogError("Prefab da comida normal não configurado!");
            return;
        }

        Vector3 posicao = NovaPosicao();
        Instantiate(foodPrefab, posicao, Quaternion.identity);
    }

    public void ComidaFoiComida()
    {
        comidasComidas++;

        Debug.Log("Comidas normais: " + comidasComidas);

        if (comidasComidas >= comidasParaFase2
            && comidasComidas % comidasParaFase2 == 0
            && !comidaLentaAtiva)
        {
            SpawnComidaLenta();
        }

        StartCoroutine(SpawnFoodComAtraso());
    }

    IEnumerator SpawnFoodComAtraso()
    {
        yield return new WaitForSeconds(1f);
        SpawnFood();
    }

    public void SpawnComidaLenta()
    {
        if (comidaLentaPrefab == null || comidaLentaAtiva)
            return;

        comidaLentaAtiva = true;

        Vector3 posicao = NovaPosicao();
        Instantiate(comidaLentaPrefab, posicao, Quaternion.identity);
    }

    public void ComidaLentaFoiComida()
    {
        comidaLentaAtiva = false;
    }

    Vector3 NovaPosicao()
    {
        float x = Random.Range(-limiteX, limiteX);
        float y = Random.Range(-limiteY, limiteY);

        return new Vector3(
            Mathf.Round(x),
            Mathf.Round(y),
            -0.2f
        );
    }
}


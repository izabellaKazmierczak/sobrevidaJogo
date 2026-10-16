
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 1.5f;
    public float aumentoVelocidade = 0.05f;

    [Header("Crescimento da cobra")]
    public GameObject prefabCorpo;
    public float distanciaEntrePartes = 0.8f;

    private Vector2 direcao = Vector2.right;
    private Vector2 proximaDirecao = Vector2.right;

    private readonly List<Transform> partesCorpo = new List<Transform>();
    private readonly List<Vector3> historicoPosicoes = new List<Vector3>();

    private bool jogoEncerrado = false;

    void Start()
    {
        velocidade = PlayerPrefs.GetFloat("VelocidadeInicial", 1.5f);

        historicoPosicoes.Add(transform.position);
    }

    void Update()
    {
        if (jogoEncerrado)
            return;

        // Lê diretamente as teclas de direção.
        if (Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.W))
        {
            if (direcao.y == 0)
                proximaDirecao = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Input.GetKeyDown(KeyCode.S))
        {
            if (direcao.y == 0)
                proximaDirecao = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                 Input.GetKeyDown(KeyCode.A))
        {
            if (direcao.x == 0)
                proximaDirecao = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) ||
                 Input.GetKeyDown(KeyCode.D))
        {
            if (direcao.x == 0)
                proximaDirecao = Vector2.right;
        }

        direcao = proximaDirecao;

        // Move a cabeça.
        transform.Translate(
            (Vector3)(direcao * velocidade * Time.deltaTime),
            Space.World
        );

        // Guarda o caminho percorrido.
        Vector3 posicaoAtual = transform.position;

        if (Vector3.Distance(historicoPosicoes[0], posicaoAtual) > 0.001f)
            historicoPosicoes.Insert(0, posicaoAtual);

        // Faz as partes seguirem o caminho da cabeça.
        for (int i = 0; i < partesCorpo.Count; i++)
        {
            float distanciaAlvo = (i + 1) * distanciaEntrePartes;
            float distanciaPercorrida = 0f;

            for (int j = 0; j < historicoPosicoes.Count - 1; j++)
            {
                Vector3 recente = historicoPosicoes[j];
                Vector3 antiga = historicoPosicoes[j + 1];

                float trecho = Vector3.Distance(recente, antiga);

                if (distanciaPercorrida + trecho >= distanciaAlvo)
                {
                    float restante = distanciaAlvo - distanciaPercorrida;
                    float proporcao = trecho > 0f
                        ? restante / trecho
                        : 0f;

                    partesCorpo[i].position =
                        Vector3.Lerp(recente, antiga, proporcao);

                    break;
                }

                distanciaPercorrida += trecho;
            }
        }

        // Limita o tamanho do histórico.
        float distanciaNecessaria =
            (partesCorpo.Count + 2) * distanciaEntrePartes;

        float distanciaGuardada = 0f;
        int ultimoIndice = 0;

        for (int i = 0; i < historicoPosicoes.Count - 1; i++)
        {
            distanciaGuardada += Vector3.Distance(
                historicoPosicoes[i],
                historicoPosicoes[i + 1]
            );

            ultimoIndice = i + 1;

            if (distanciaGuardada > distanciaNecessaria)
                break;
        }

        if (ultimoIndice + 1 < historicoPosicoes.Count)
        {
            historicoPosicoes.RemoveRange(
                ultimoIndice + 1,
                historicoPosicoes.Count - ultimoIndice - 1
            );
        }
    }



    public void Crescer()
    {
        if (prefabCorpo == null)
        {
            Debug.LogError("Configure o Prefab Corpo no Player!");
            return;
        }

        GameObject novaParte = Instantiate(
            prefabCorpo,
            transform.position,
            Quaternion.identity
        );

        Collider2D colisorCabeca = GetComponent<Collider2D>();
        Collider2D colisorCorpo = novaParte.GetComponent<Collider2D>();

        // Evita que a nova parte provoque Game Over ao nascer.
        if (colisorCabeca != null && colisorCorpo != null)
        {
            Physics2D.IgnoreCollision(colisorCabeca, colisorCorpo, true);
        }

        partesCorpo.Add(novaParte.transform);

        Debug.Log("Parte do corpo criada!");
    }



    public void AumentarVelocidade()
    {
        velocidade += aumentoVelocidade;
    }

    public void DiminuirVelocidade()
    {
        StopCoroutine(nameof(ReducaoTemporaria));
        StartCoroutine(ReducaoTemporaria());
    }

    IEnumerator ReducaoTemporaria()
    {
        float velocidadeOriginal = velocidade;

        velocidade = Mathf.Max(0.5f, velocidadeOriginal * 0.5f);

        yield return new WaitForSeconds(3f);

        velocidade = velocidadeOriginal;
    }

    public void PararCobra()
    {
        jogoEncerrado = true;
    }
}

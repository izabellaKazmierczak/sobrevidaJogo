
using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public static Pontuacao Instancia;

    public TMP_Text textoPontuacao;

    private int pontos = 0;

    public int Pontos => pontos;

    void Awake()
    {
        Instancia = this;
        AtualizarTexto();
    }

    public void AdicionarPontos(int quantidade)
    {
        pontos += quantidade;
        AtualizarTexto();

        Debug.Log("Pontuação atual: " + pontos);
    }

    void AtualizarTexto()
    {
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontuação: " + pontos;
        }
        else
        {
            Debug.LogError("O campo Texto Pontuacao não foi configurado!");
        }
    }
}
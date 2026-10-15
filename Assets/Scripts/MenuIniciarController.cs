
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialController : MonoBehaviour
{
    // Executado quando o jogador clica em JOGAR
    public void Jogar()
    {
        SceneManager.LoadScene("Sobrevida");
    }

    // Executado quando o jogador clica em SAIR
    public void Sair()
    {
        Application.Quit();
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuIniciarController : MonoBehaviour
{
    // Executado quando o jogador clica em JOGAR
    public void Jogar()
    {
        SceneManager.LoadScene("SelecaoVelocidade");
    }

    // Executado quando o jogador clica em SAIR
    public void Sair()
    {
        Application.Quit();
    }
}

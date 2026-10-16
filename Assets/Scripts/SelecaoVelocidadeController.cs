
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecaoVelocidadeController : MonoBehaviour
{
    // Velocidades iniciais para cada dificuldade.
    public void EscolherFacil()
    {
        PlayerPrefs.SetFloat("VelocidadeInicial", 3.7f);
        IniciarJogo();
    }

    public void EscolherNormal()
    {
        PlayerPrefs.SetFloat("VelocidadeInicial", 4.2f);
        IniciarJogo();
    }

    public void EscolherDificil()
    {
        PlayerPrefs.SetFloat("VelocidadeInicial", 5.0f);
        IniciarJogo();
    }

    private void IniciarJogo()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Sobrevida");
    }
}

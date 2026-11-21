
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instancia;

    public AudioSource musicaFundo;
    public AudioSource efeitos;

    public AudioClip musica;
    public AudioClip somComer;
    public AudioClip somDerrota;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        musicaFundo.clip = musica;
        musicaFundo.loop = true;
        musicaFundo.Play();
    }

    public void TocarSomComer()
    {
        efeitos.PlayOneShot(somComer);
    }

    public void TocarSomDerrota()
    {
        musicaFundo.Stop();
        efeitos.PlayOneShot(somDerrota);
    }
}

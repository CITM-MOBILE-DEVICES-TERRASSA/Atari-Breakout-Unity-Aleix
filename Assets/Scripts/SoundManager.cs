using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;    
    public AudioSource fxSource;      

    public AudioClip backgroundMusic;  
    public AudioClip hitSound;         
    public AudioClip loseLifeSound;    
    public AudioClip ballLaunch;    
    public AudioClip loseLife;   
    public AudioClip leave;    
    public AudioClip ballBounce;    
    public AudioClip buttonClick;
    public AudioClip breaking;
    public AudioClip particleSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    
    public void StartBackgroundMusic()
    {
        if (!musicSource.isPlaying) 
        {
            PlayBackgroundMusic(); 
        }
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic; 
        musicSource.loop = true;             
        musicSource.Play();                  
        Debug.Log("Reproduciendo música de fondo");
    }

    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying) 
        {
            musicSource.Stop(); 
            Debug.Log("Música de fondo detenida");
        }
    }

    
    public void DebugPlayBackgroundMusic()
    {
        Debug.Log("Intentando reproducir música de fondo");
        PlayBackgroundMusic();
    }

    public void PlayFx(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);  
    }

    public void PlayButtonClick()
    {
        PlayFx(buttonClick);
    }
}

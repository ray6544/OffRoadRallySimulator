using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource MenuSounds;
    [SerializeField]
    public int SoundFx;

    #region Unity Functions

    private void Awake()
    {
        Init();
        VariablesInit();
    }
    void Start()
    {
        GameMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
    #region Custom Functions 
    void Init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }
    }
    void VariablesInit()
    {
        SoundFx = PlayerPrefs.GetInt("SoundFx");
        AudioSource[] audiosource = GetComponents<AudioSource>();
        MenuSounds = audiosource[0];
    }
    public void IsLoading(bool b)
    {
        if (b == true)
        {
            if (MenuSounds.isPlaying)
                MenuSounds.Pause();

            
        }
    }
    public void GameMusic()
    {
       

            if (SceneManager.GetActiveScene().name != "GamePlay" && SceneManager.GetActiveScene().name != "Splash")
            {

                Debug.Log(SceneManager.GetActiveScene().name);
                if (!MenuSounds.isPlaying)
                    MenuSounds.Play();



            }
            else if (SceneManager.GetActiveScene().name == "GamePlay" && SceneManager.GetActiveScene().name != "Splash")
            {
                if (MenuSounds.isPlaying)
                    MenuSounds.Stop();


            }
        
        
    }
    //---------Setter and Getter---------
    public void SetSoundFx(int i)
    {
        SoundFx = i;
        PlayerPrefs.SetInt("SoundFx", SoundFx);
    }
    public int GetSoundFx()
    {
        return SoundFx;
    }
    public void AudioListnereMute(int b)
    {
        if (b == 0)
        {
            AudioListener.volume = 1.0f;
        }
        else if (b == 1)
        {
            AudioListener.volume = 0.0f;
        }

    }
    #endregion
}

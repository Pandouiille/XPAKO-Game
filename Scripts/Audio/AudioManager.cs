using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public AudioClip[] otherClip;
    private AudioSource audioSource;
    private string lastScene;
    public AudioMixerGroup soundEffectMixer;
    public static AudioManager instance;
    private int musicIndex = 0;
    private string sceneName;
    public float delay=1;
    public enum Mode {EnsurePlaying, Restart, Stop};
    public Mode mode;

    private void Awake()
    {
        MakeSingleton();
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource> ();

    }

    void Start()
    {
        audioSource.clip = otherClip[0];
        audioSource.Play();
    }

    void Update()
    {
        musicIndex = (musicIndex + 1) % otherClip.Length;
        Scene currentScene = SceneManager.GetActiveScene();
         
        if (currentScene.name == "main + Karma")
        {
            audioSource.clip = otherClip[1];
            StartBGMusic(false);
        }
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }

    public void StartBGMusic(bool aRestart)
    {
        if (!audioSource.isPlaying || aRestart)
        {
            audioSource.Play();
        }

    }

    public void StartBackgroundMusic()
    {
        audioSource.Stop();
    }

    void MakeSingleton () {
   
        if (instance != null) {
            Destroy (gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad (gameObject);
        }
    }
 
}
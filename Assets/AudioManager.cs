using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager: MonoBehaviour
{
    public static AudioManager instance = null;

    public float originalBGMVolume = 1;
    public float soundVolume = 1;

    [SerializeField] private AudioSource musicBGM;
    [SerializeField] private AudioSource soundSource;
    private float musicBGMVolume;

    public AudioClip calmBGM;
    public AudioClip aggresiveBGM;
    public float timeToFade = 1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        StopAllCoroutines();
        musicBGM.clip = calmBGM;
        musicBGM.loop = true;
        musicBGM.volume = originalBGMVolume;
        musicBGM.Play();

        musicBGMVolume = originalBGMVolume;

    }

    void Update()
    {
        //Update Volume accordingly at all times
        musicBGM.volume = musicBGMVolume;

        LiveUpdateVolume(soundVolume);
    }



    public void AggresiveMode()
    {
        StartCoroutine(SwitchSong(true, aggresiveBGM));
    }

    public void FadeOut()
    {
        StartCoroutine(JustFadeOut());
    }

    public void FadeIn(AudioClip _music, float _timeToFade)
    {
        StartCoroutine(JustFadeIn(_music, _timeToFade));
    }

    public IEnumerator SwitchSong(bool _InOrOut, AudioClip _music) //Always start out as _InOrOut == true, to fade out music, then it starts its own coroutine with _InOrOut == false, to fade in new music.
    {


        if (!_InOrOut) //If fading in new music, then switch clip;
        {
            musicBGM.clip = _music;
            musicBGM.Play();
        }

        switch (_InOrOut)
        {
            case true:
                float _delta1 = (originalBGMVolume) / (timeToFade * 60);
                for (int i = 0; i <= timeToFade * 60; i++) //The for loop will last for the entire "Duration To Return"
                {
                    //FadeOut
                    musicBGMVolume -= _delta1;

                    yield return new WaitForFixedUpdate(); //60Hz per second
                }
                break;

            case false:
                float _delta2 = (originalBGMVolume) / (timeToFade * 10); //only 1/6 of the duration
                for (int i = 0; i <= timeToFade * 10; i++) //The for loop will last for the entire "Duration To Return"
                {
                    //FadeIn
                    musicBGMVolume += _delta2;

                    yield return new WaitForFixedUpdate(); //60Hz per second
                }
                break;


        }

        //float _delta = (originalBGMVolume) / (timeToFade * 60);
        //for (int i = 0; i <= timeToFade * 60; i++) //The for loop will last for the entire "Duration To Return"
        //{
        //    switch (_InOrOut)
        //    {
        //        case true: //FadeOut
        //            musicBGMVolume -= _delta;
        //
        //            break;
        //
        //        case false: //FadeIn
        //            musicBGMVolume += _delta; 
        //
        //            break;
        //
        //    }
        //    Debug.Log(i);
        //    yield return new WaitForFixedUpdate(); //60Hz per second
        //}

        switch (_InOrOut)
        {
            case true:
                musicBGM.volume = 0;
                musicBGM.Stop();
                StartCoroutine(SwitchSong(false, _music));
                break;

            case false:
                musicBGM.volume = musicBGMVolume;
                break;
        }
        yield return null;

    }

    public IEnumerator JustFadeIn(AudioClip _music, float _timeToFade)
    {
        //Guard
        musicBGM.Stop();

        //Set Music
        musicBGM.clip = _music;
        musicBGM.Play();

        float _delta = (musicBGMVolume) / (_timeToFade * 60);
        for (int i = 0; i <= timeToFade * 60; i++) //The for loop will last for the entire "Duration To Return"            
        {
            musicBGMVolume += _delta;
            yield return new WaitForFixedUpdate(); //60Hz per second
        }

        musicBGM.volume = originalBGMVolume;
    }

    public IEnumerator JustFadeOut()
    {
        float _delta = (musicBGMVolume) / (timeToFade * 60);
        for (int i = 0; i <= timeToFade * 60; i++) //The for loop will last for the entire "Duration To Return"            
        {
            musicBGMVolume -= _delta;
            yield return new WaitForFixedUpdate(); //60Hz per second
        }

        musicBGM.volume = 0;
    }

    public void UpdateMusicVolume(float _volume)
    {
        musicBGM.volume = _volume;
    }

    public void UpdateSoundVolume(float _volume)
    {
        LiveUpdateVolume(_volume);
    }

    public void LiveUpdateVolume(float _soundVolume)
    {
        foreach (AudioSource _source in GameObject.FindObjectsOfType<AudioSource>())
        {
            if (_source != musicBGM)
            {
                _source.volume = _soundVolume;
            }
        }
    }

    public void PlaySound(AudioClip _clip, bool _loop, float _localMultiplier, GameObject _obj) //Creates a new audio source everytime a play sound is requeste
    {       

        soundSource.PlayOneShot(_clip, _localMultiplier);


        //StartCoroutine(KillAudioSource(_source, _obj));

        //Non-looped sound will be killed when it ends.
        //if (!_loop) 
    }
    public IEnumerator KillAudioSource(AudioSource _source, GameObject _sourceOfSound)
    {
        while (_source.isPlaying)
        {
            //Adjust sound volume if far away



            yield return new WaitForFixedUpdate();
        }

        Destroy(_source);
    }
}


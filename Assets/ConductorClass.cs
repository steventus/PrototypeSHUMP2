using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using UnityEngine;

public class ConductorClass : MonoBehaviour
{
    public static ConductorClass instance = null;

    public bool LoopSong;

    private bool PauseSong;

    //Keeps track of the song position at all times - to be detected for input or create beatmaps with.

    public float songBPM;
    public float secPerBeat, songPosition, songPositionInBeats, oldSongPositionInBeats, dspSongTime, pauseOffset;
    [HideInInspector] public AudioSource musicSource;
    public float firstbeatOffset;
    public float difficultymultiplier;
    public float playeroffset;

    //Keep track of loop positions;
    public float beatsperloop;
    public int completedloop;
    public float looppositionInBeats;

    public int completedindexloop = 0;
    public int indexperloop;

    //Making TriggerBeat and OnBeat Stuff
    private int nextIndex = 0;
    private int nextBigIndex = 0;
    public int BigIndexThreshold = 2;

    //Keep Track of Audiosettings.dsptime paused at
    public float PausedSongPos;

    //Debug
    public GameObject square;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        

        //Number of seconds for each beat. Number of minutes per beat * total number of seconds in a minute.
        secPerBeat = 60f / songBPM;

        //UIManager.instance.BeatDuration = secPerBeat;
                

        //Record offset time when music starts
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update()
    {
        //if (GameManager.instance.Lost == true || PauseSong)
        //{
        //    PausedSongPos = (float)(AudioSettings.dspTime - dspSongTime - firstbeatOffset);
        //    return;
        //}
        //
        //if (musicSource.isPlaying == false)
        //    GameManager.instance.WinGame();



        //The songposition accounts for inherent offset within the audio player, additional offset within the music itself and
        //is most accurate in terms of using only the internal music timer for determining the music instead of external Time.time;
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstbeatOffset - pauseOffset + playeroffset);
        songPositionInBeats = songPosition / secPerBeat;



        //Beat Commands
        #region Beat
        if (nextIndex < songPositionInBeats)
        {
            //Small Index (Beat) Commands
            nextIndex++;         
            square.GetComponent<Beat_Behaviour>().TriggerBeat();
            //FindObjectOfType<ShockwaveBehaviour>().TriggerSmallBeat();
            FindObjectOfType<TestLaserBehaviour>().TriggerSmallBeat();
            foreach (TestRhythmBullet _obj in FindObjectsOfType<TestRhythmBullet>())
            {
                _obj.TriggerSmallBeat();
            }

            //Big Index (Beat) Commands
            nextBigIndex++;
            if (nextBigIndex == BigIndexThreshold)
            {
                nextBigIndex = 0;
            }
        }
        #endregion
    }

        public bool IfPressedOnBeat()
    {
        if (Mathf.Abs(nextIndex * secPerBeat - songPosition) <= secPerBeat / 3)
        {
            Debug.Log(nextIndex * secPerBeat - songPosition + "compare with " + secPerBeat/3 + " hence YES");

            return true;
        }

        else 
        {
            Debug.Log(nextIndex * secPerBeat - songPosition + "compare with " + secPerBeat / 3 + " hence NO");
            return false;
        }
    }

    public void Pause()
    {
        //if (GameManager.instance.Pause)
        //{
        //    PauseSong = true;
        //    musicSource.Pause();
        //}
        //else if (GameManager.instance.Pause == false)
        //{
        //    //Audiosettings.dspTime contineus to record time since audio played even when time is paused etc., a raw time value-
        //    //to account for that when game is paused, we have to store temp value to offset back to original song.
        //    PauseSong = false;
        //    pauseOffset = PausedSongPos - songPosition;
        //    musicSource.Play();
        //}

    }


}

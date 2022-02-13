using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timeRemaining ;
    public bool timerIsRunning = false;
    public  float _time;
    public  int CheckPoints;
    public  int MaxCheckPoints;
    public  int Coins;
    bool StartGame;
    public Transform RespawnPos;
    Vehicle_Properties _properties;
    public RCC_Camera _camera;
    [Header("Vehicles")]
    public GameObject[] _Vehicles;
    int _levelRewards;
    int _totalReward;
    bool isFailed, isCompleted;
    public AudioSource _audiosource;
    public AudioClip click,startSoun;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        CheckPoints = 0;
        MaxCheckPoints = 0;
        GameObject[] Obj = GameObject.FindGameObjectsWithTag("CheckPoint");
        MaxCheckPoints = Obj.Length;
        UiManager.instance.CheckPointsUI(CheckPoints.ToString() + "/" + MaxCheckPoints.ToString());
        _time = timeRemaining;
        UiManager.instance.TimerUI(_time);
        UiManager.instance.CoinsUI(Coins.ToString()); ;
        StartCoroutine(Count());
        Init();
    }
    void Init()
    {
        for (int i = 0; i < _Vehicles.Length; i++)
        {
            if (i == DataManager.instance.GetCarId())
            {
                _Vehicles[i].SetActive(true);
                _properties = _Vehicles[i].GetComponent<Vehicle_Properties>();
                _camera.playerCar = _Vehicles[i].GetComponent<RCC_CarControllerV3>();
            }
            else
            {
                _Vehicles[i].SetActive(false);
            }
        }
        _properties.Speed(DataManager.instance.GetSpeed());
        _properties.Braking(DataManager.instance.GetBraking());
        _properties.Handling(DataManager.instance.GetHandling() / 2);
        _properties.Accelration(DataManager.instance.GetAccel());
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    void Timer()
    {
        if (StartGame == true)
        {
            if (timerIsRunning)
            {
                Debug.Log(_time);
                if (_time > 0)
                {
                    Debug.Log(_time);
                    _time -= Time.deltaTime;
                    UiManager.instance.TimerUI(_time);
                }
                else
                {
                    StartGame = false;
                    timerIsRunning = false;
                    _time = 0;
                    UiManager.instance.TimerUI(_time);
                    LevelFailed();

                }
            }
        }
    }
    IEnumerator Count()
    {
        if (!UiManager.instance.StartCount.activeSelf)
            UiManager.instance.StartCount.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        int i = 3;
        while (i != -1)
        {
            if (i > 0)
            {
                UiManager.instance.StartCountUi(i.ToString());
                i--;
                Audio(startSoun);
                Debug.Log(i);
                yield return new WaitForSeconds(1.0f);

            }
            else if (i == 0)
            {
                UiManager.instance.StartCountUi("GO!");
                i--;
                Audio(startSoun);
                Debug.Log(i);
                yield return new WaitForSeconds(1.0f);
            }
            
        }
        if (UiManager.instance.StartCount.activeSelf)
            UiManager.instance.StartCount.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        StartGame = true;
        timerIsRunning = true;
        UiManager.instance.GamePlay_ui();
        
    }
    //-----------Game Controllers--------------------
    public void GamePaused()
    {
        Audio(click);
        UiManager.instance.GamePlay_ui();
        UiManager.instance.PauseGame_ui();
        if (timerIsRunning == true)
            timerIsRunning = false;
        if (_properties.Rigid.isKinematic == false)
            _properties.Rigid.isKinematic = true;
    }
    public void Restart()
    {
        Audio(click);
        UiManager.instance.LoadingUi(SceneManager.GetActiveScene().name);
    }
    public void Home()
    {
        Audio(click);
        UiManager.instance.LoadingUi("MainMenu");
    }
    public void Resume_pause()
    {
        Audio(click);
        UiManager.instance.GamePlay_ui();
        UiManager.instance.PauseGame_ui();
        if (timerIsRunning == false)
            timerIsRunning = true;
        if (_properties.Rigid.isKinematic == true)
            _properties.Rigid.isKinematic = false;
    }
    public void Resume_lose()
    {
        Audio(click);
        if (isFailed == true)
            isFailed = false;
        timeRemaining = 50;
        _time = timeRemaining;
        Debug.Log(_time);
        if (timerIsRunning == false)
            timerIsRunning = true;
        if (StartGame == false)
            StartGame = true;
        if (_properties.Rigid.isKinematic == true)
            _properties.Rigid.isKinematic = false;
        _camera.gameObject.SetActive(true);
        _properties.RotationCamera.SetActive(false);
        UiManager.instance.LoseGame_Ui(); 
        UiManager.instance.GamePlay_ui();
        
    }
    public void Watch_Rewarded()
    {
        Audio(click);
        _totalReward = _totalReward * 2;
        UiManager.instance.TotalRewardUi(_totalReward.ToString());
    }
    public void NextRace()
    {

        Audio(click);
        if (PlayerPrefs.GetInt("Levels" + DataManager.instance.GetLevelNumber()) == 0)
        {
            PlayerPrefs.SetInt("Levels" + DataManager.instance.GetLevelNumber(),1);
        }
        DataManager.instance.SetLevelNumber(DataManager.instance.GetLevelNumber() + 1);
        UiManager.instance.LoadingUi("Level" + DataManager.instance.GetLevelNumber());
    }
    public void LevelFailed()
    {
        if (isFailed == false && isCompleted == false)
        {
            isFailed = true;
            StartCoroutine(_levelFailed());
            Debug.Log("w");
        }
    }
    IEnumerator _levelFailed()
    {
        yield return new WaitForSeconds(0.5f);
        UiManager.instance.GamePlay_ui();
        if (_properties.Rigid.isKinematic == false)
            _properties.Rigid.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        UiManager.instance.FadeScreen();
        yield return new WaitForSeconds(0.5f);
        _properties.RotationCamera.SetActive(true);
        _camera.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        UiManager.instance.LoseGame_Ui();
    }
    public void LevelComplete()
    {
        if (isFailed == false && isCompleted == false)
        {
            isCompleted = true;
            StartCoroutine(_levelComplete());
        }
    }
    IEnumerator _levelComplete()
    {
        if (timerIsRunning == false)
            timerIsRunning = true;
        if (StartGame == true)
        {
            StartGame = false;
        }
        yield return new WaitForSeconds(0.5f);
        UiManager.instance.GamePlay_ui();
        if (_properties.Rigid.isKinematic == false)
            _properties.Rigid.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        UiManager.instance.FadeScreen();
        yield return new WaitForSeconds(0.5f);
        _properties.RotationCamera.SetActive(true);
        _camera.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        _levelRewards = (int)Random.Range(50 , 100);
        _totalReward = _levelRewards + Coins;
        UiManager.instance.WonGame_Ui(Coins,_time,_totalReward);
        DataManager.instance.SetCoins(DataManager.instance.GetCoins() + _totalReward);
    }
    //----------------------Respawn------------------------
    public void RespawnSavePos(Transform trans)
    {
        RespawnPos.position = trans.position;
        RespawnPos.rotation = trans.rotation;
    }
    public void Respawn()
    {
        Audio(click);
        _properties.gameObject.transform.position = RespawnPos.position;
        _properties.gameObject.transform.rotation = RespawnPos.rotation;
    }
    public void Audio(AudioClip _audioClip)
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            _audiosource.PlayOneShot(_audioClip);
        }
    }
}

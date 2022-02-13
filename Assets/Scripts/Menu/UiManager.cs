using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio;
public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [Header("GamePlay Panels")]
    public GameObject fadeScreen;
    public Popup PausePanel_bg, PausePanel_bg1, PausePanel_footer;
    public Popup WonePanel_Header, WonePanel_bg, WonePanel_CoinsCollected, WonePanel_timeLeft, WonePanel_CoinsReward, WonePanel_Footer, WonePanel_Footer1;
    public Text WonPanel_CoinsRewardstxt, WonPanel_TimeLefttxt, WonPanel_CoinsCollectedtxt;
    public Popup LosePanel_bg, LosePanel_bg1, LosePanel_footer;
    public Popup GamePLay_Header,GamePlay_Footer;
    public GameObject Loading_Obj;
    [Header("Text UI")]
    public Text Timertxt;
    Animator _timetxtAnim;
    public Text Coinstxt;
    Animator _coinstxtAnim;
    public Text CheckPointstxt;
    Animator _checkpointstxtAnim;
    public GameObject StartCount;
    public Animator CoinsAnim;
    Animator _startCountAnim;
    Text _startCountTxt;
    int _minutes;
    int _seconds;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        Init();
    }
    private void Init()
    {
        _coinstxtAnim = Coinstxt.GetComponent<Animator>();
        _timetxtAnim = Timertxt.GetComponent<Animator>();
        _checkpointstxtAnim = CheckPointstxt.GetComponent<Animator>();
        _startCountAnim = StartCount.GetComponent<Animator>();
        _startCountTxt = StartCount.GetComponentInChildren<Text>();
        Debug.Log(_startCountTxt.name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CoinsUI(string st)
    {
        Coinstxt.text = st;
    }
    public void CoinsTxtAnimation()
    {
        _coinstxtAnim.Play("Anim");
    }
    public void CheckPointsUI(string st)
    {
        CheckPointstxt.text = st;
    }
    public void CheckPointsAnimation()
    {
        _checkpointstxtAnim.Play("Anim");
    }
    public void TimerUI(float f)
    {
        _minutes = Mathf.FloorToInt(f / 60);
        _seconds = Mathf.FloorToInt(f% 60);
        Timertxt.text= string.Format("{0:00}:{1:00}",_minutes, _seconds);
    }
    public void TimerTxtAnimation()
    {
        _timetxtAnim.Play("Anim");
    }
    public void FadeScreen()
    {
        fadeScreen.GetComponent<Animator>().Play("Fade Screen");
    }
    public void StartCountUi(string st)
    {
        _startCountTxt.text = st;
        _startCountAnim.Play("StartTimer");
    }
    public void PauseGame_ui()
    {
        PausePanel_bg.Toggle();
        PausePanel_bg1.Toggle();
        PausePanel_footer.Toggle();
    }
    public void GamePlay_ui()
    {
        GamePlay_Footer.Toggle();
        GamePLay_Header.Toggle();
    }
    public void LoseGame_Ui()
    {
        LosePanel_bg.Toggle();
        LosePanel_bg.Toggle();
        LosePanel_bg1.Toggle();
        LosePanel_footer.Toggle();
    }
    public void WonGame_Ui(int CoinsCollected, float TimeLeft, int Reward)
    {
        WonPanel_CoinsCollectedtxt.text = CoinsCollected.ToString();
        _minutes = Mathf.FloorToInt(TimeLeft / 60);
        _seconds = Mathf.FloorToInt(TimeLeft % 60);
        WonPanel_TimeLefttxt.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        WonPanel_CoinsRewardstxt.text = Reward.ToString();
        StartCoroutine(WOnpanel_(0.8f));
    }
    IEnumerator WOnpanel_(float _gapTime)
    {
        WonePanel_bg.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_Header.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_CoinsCollected.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_timeLeft.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_CoinsReward.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_Footer.Toggle();
        yield return new WaitForSeconds(_gapTime);
        WonePanel_Footer1.Toggle();
    }
    public void TotalRewardUi(string st)
    {
        WonPanel_CoinsRewardstxt.text = st;
    }
    public void LoadingUi(string Scenename)
    {
        Loading_Obj.GetComponent<Splash>().LoadingSceneName = Scenename;
        if (!Loading_Obj.activeSelf)
            Loading_Obj.SetActive(true);
    }
    public void CoinsAnimation()
    {
        CoinsAnim.Play("COinsAnim");
    }
}

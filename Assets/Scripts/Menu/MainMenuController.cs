using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio;
using UnityEngine.UI;
using TMPro;
public class MainMenuController : MonoBehaviour
{
    [Header("----------------MainMenu UI------------------")]
    public Popup MainMenu_header;
    public Popup MainMenu_footer;
    public TextMeshProUGUI coinstxt_mainMenu;
    [Header("----------------Grage UI------------------")]
    public Transform Selection_Obj;
    public Transform NameTag;
    int _nameTagindex;
    public Popup Grage_header;
    public Popup Grage_footer;
    public Popup Grage_left;
    public Popup Grage_right;
    public TextMeshProUGUI coinstxt_grage;
    public Image Lockimg;
    public GameObject Coinsbg_purchasing; 
    public Text coinstxt_purchasing;
    public TextMeshProUGUI VehicleName;
    public GameObject Unlock_btn, Select_btn;
    public Slider Accel_slider,Speed_slider,Braking_slider,Handling_slider;
    [Header("----------------Zoom DashBoard UI------------------")]
    public Popup Header_zoom;
    public Popup Footer_zoom;
    [Header("----------------------------------")]
    public GameObject Loading;
    public Popup ConnectivityPanel,Donnothaveenoughcoins,maxLimit,ExitPanel;

    [Header("----------------------------------")]
    public Image SoundBtn;
    public Sprite SoundOn, SoundOff;
    [Header("Links")]
    public string LikeIt_string, MoreGames_string,PrivacyPolicy_string;
    [Header("----------------------------------")]
    public GameObject[] Cams;
    bool lerpingSlider, lerpingSlider1, lerpingSlider2,lerpingSlider3;
    public VehicleProperties [] _vehicleProperties;
    int UnlockVehicleIndex;
    [Header("-----------Confirmation Panel")]
    public Popup ConfirmationPanel;
    public Text Confirmationtxt;
    public enum Confirmation{Privacy=0,moregame=1,likeit=2};
    public Confirmation _confirmation;
    int ConfirmationIndex;
    public Popup PropertiesConfirmation;
    public Text PropertiesConfirmationtxt;
    public enum Properties { accel = 0, speed = 1, braking = 2, Handling = 3 };
    public Properties _properties;
    int PropertiesIndex;
    [Header("Sound")]
    public AudioSource _audiosource;
    public AudioClip Click, SelectionClick, Alert, congrets;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Audio(Click);
            ExitUI();
        }
    }
    public void Quit()
    {
        Audio(Click);
        Application.Quit();
    }
    void ExitUI()
    {
        Audio(Click);
        ExitPanel.Toggle();
    }
    public void Exit()
    {
        Audio(Click);
        ExitUI();
    }
    void Init()
    {
        SoundManager.instance.GameMusic();
        SoundUI(SoundManager.instance.GetSoundFx());
        CoinShow(DataManager.instance.GetCoins());
        if (PlayerPrefs.GetInt("VehicleUnlock" + 0) == 0)
            PlayerPrefs.SetInt("VehicleUnlock" + 0, 1);
        for (int i = 0; i < _vehicleProperties.Length; i++)
        {
            PlayerPrefs.GetInt("VehicleUnlock" + i);
            
        }
        VehicleSelection(DataManager.instance.GetCarId());
        SetPlayerNameTag(DataManager.instance.GetCarId());
    }
    
    void CoinShow(int i)
    {
        coinstxt_grage.text = i.ToString();
        coinstxt_mainMenu.text = i.ToString();
    }
    public void SceneChange(string scenename)
    {
        Audio(Click);
        SoundManager.instance.IsLoading(true);
        Loading.GetComponent<Splash>().LoadingSceneName = scenename;
        Loading.SetActive(true);
    }
    public void AdRemove()
    {
        Audio(Click);
    }
    public void PrivacyPolicy()
    {
        Audio(Click);
        Application.OpenURL(PrivacyPolicy_string);
    }
    public void UiAppearance(int i)
    {
        Audio(Click);
        switch (i)
        {
            case 1:
                if (!MainMenu_header.isVisible)
                {
                    MainMenu_header.Show();
                    MainMenu_footer.Show();
                }
                if (Grage_header.isVisible)
                {
                    Grage_header.Hide();
                    Grage_footer.Hide();
                    Grage_left.Hide();
                    Grage_right.Hide();
                }
                CameraAppearance(0);
                break;
            case 2:
                if (MainMenu_header.isVisible)
                {
                    MainMenu_header.Hide();
                    MainMenu_footer.Hide();
                }
                if (Header_zoom.isVisible)
                {
                    Header_zoom.Hide();
                    Footer_zoom.Hide();
                }
                if (!Grage_header.isVisible)
                {
                    Grage_header.Show();
                    Grage_footer.Show();
                    Grage_left.Show();
                    Grage_right.Show();
                }
                CameraAppearance(1);
                break;
            case 3:
                if (MainMenu_header.isVisible)
                {
                    MainMenu_header.Hide();
                    MainMenu_footer.Hide();
                }
                if (Grage_header.isVisible)
                {
                    Grage_header.Hide();
                    Grage_footer.Hide();
                    Grage_left.Hide();
                    Grage_right.Hide();
                }
                if (!Header_zoom.isVisible)
                {
                    Header_zoom.Show();
                    Footer_zoom.Show();
                }
                CameraAppearance(2);
                break;

        }
    }
    public void VehicleSelection(int index)
    {
        Audio(Click);
        Vehicle(index);
        Specification(index);
    }
    void Vehicle(int index)
    {
        for (int i = 0; i < _vehicleProperties.Length; i++)
        {
            if (index == i)
            {
                _vehicleProperties[i].Vehicle.SetActive(true);
            }
            else
            {
                _vehicleProperties[i].Vehicle.SetActive(false);
            }
        }
        SetPlayerNameTag(index);
    }
    void Specification(int vehicleIndex)
    {
        if (PlayerPrefs.GetInt("VehicleUnlock" + vehicleIndex) == 0)
        {
            if (Lockimg.enabled == false)
                Lockimg.enabled = true;
            if (Select_btn.activeSelf)
                Select_btn.SetActive(false);
            if (!Unlock_btn.activeSelf)
                Unlock_btn.SetActive(true);
            coinstxt_purchasing.text = _vehicleProperties[vehicleIndex].VehiclePrice.ToString();
            if (!Coinsbg_purchasing.activeSelf)
                Coinsbg_purchasing.SetActive(true);
            UnlockVehicleIndex = vehicleIndex;
        }
        else if (PlayerPrefs.GetInt("VehicleUnlock" + vehicleIndex) == 1)
        {
            if (Lockimg.enabled == true)
                Lockimg.enabled = false;
            if (!Select_btn.activeSelf)
                Select_btn.SetActive(true);
            if (Unlock_btn.activeSelf)
                Unlock_btn.SetActive(false);
            if (Coinsbg_purchasing.activeSelf)
                Coinsbg_purchasing.SetActive(false);
            
        }
        DataManager.instance.SetCarId(vehicleIndex);
        VehicleName.text = _vehicleProperties[vehicleIndex].VehicleName;
        
        StartCoroutine(LerpSlider(Accel_slider, DataManager.instance.GetAccel()));
        StartCoroutine(LerpSlider1(Speed_slider, DataManager.instance.GetSpeed()));
        StartCoroutine(LerpSlider2(Braking_slider, DataManager.instance.GetBraking()));
        StartCoroutine(LerpSlider3(Handling_slider, DataManager.instance.GetHandling()));
    }
    public void AccelInc()
    {
        if (PlayerPrefs.GetInt("VehicleUnlock" + DataManager.instance.GetCarId()) == 1)
        {
            Debug.Log((int)Accel_slider.maxValue);
            if (DataManager.instance.GetAccel() < (int)Accel_slider.maxValue)
            {
                DataManager.instance.SetAccel(DataManager.instance.GetAccel() + 5);
                StartCoroutine(LerpSlider(Accel_slider, DataManager.instance.GetAccel()));
            }
            else
            {
                maxLimit.Toggle();
            }
        }
    }
    public void SpeedInc()
    {
        if (PlayerPrefs.GetInt("VehicleUnlock" + DataManager.instance.GetCarId()) == 1)
        {
            if (DataManager.instance.GetSpeed() < (int)Accel_slider.maxValue)
            {
                DataManager.instance.SetSpeed(DataManager.instance.GetSpeed() + 5);
                StartCoroutine(LerpSlider1(Speed_slider, DataManager.instance.GetSpeed()));
            }
            else
            {
                maxLimit.Toggle();
            }
        }
    }
    public void BrakingInc()
    {
        if (PlayerPrefs.GetInt("VehicleUnlock" + DataManager.instance.GetCarId()) == 1)
        {
            if (DataManager.instance.GetBraking() < (int)Accel_slider.maxValue)
            {
                DataManager.instance.SetBraking(DataManager.instance.GetBraking() + 5);
                StartCoroutine(LerpSlider2(Braking_slider, DataManager.instance.GetBraking()));
            }
            else
            {
                maxLimit.Toggle();
            }
        }
    }
    public void HandlingInc()
    {
        if (PlayerPrefs.GetInt("VehicleUnlock" + DataManager.instance.GetCarId()) == 1)
        {
            if (DataManager.instance.GetHandling() < Accel_slider.maxValue)
            {
                DataManager.instance.SetHandling(DataManager.instance.GetHandling() + 5);
                StartCoroutine(LerpSlider3(Handling_slider, DataManager.instance.GetHandling()));
            }
            else
            {
                maxLimit.Toggle();
            }
        }
    }
    public void UnlockTheVehicle()
    {
        if (DataManager.instance.GetCoins() >= _vehicleProperties[UnlockVehicleIndex].VehiclePrice)
        {
            DataManager.instance.SetCoins(DataManager.instance.GetCoins() - _vehicleProperties[UnlockVehicleIndex].VehiclePrice);
            CoinShow(DataManager.instance.GetCoins());
            if (Lockimg.enabled == true)
                Lockimg.enabled = false;
            if (!Select_btn.activeSelf)
                Select_btn.SetActive(true);
            if (Unlock_btn.activeSelf)
                Unlock_btn.SetActive(false);
            if (Coinsbg_purchasing.activeSelf)
                Coinsbg_purchasing.SetActive(false);
            PlayerPrefs.SetInt("VehicleUnlock" + UnlockVehicleIndex, 1);
            DataManager.instance.SetCarId(UnlockVehicleIndex);
            UnlockVehicleIndex = 0;

        }
        else
        {
            Donnothaveenoughcoins.Toggle();
        }
    }
    public void AddCoins(int coins)
    {
        Audio(Click);
        if (InternetConnectivity())
        {
            DataManager.instance.SetCoins(DataManager.instance.GetCoins() + coins);
            CoinShow(DataManager.instance.GetCoins());
        }
        else
        {
            ConnectivityPanel.Toggle();
        }
    }
    private IEnumerator LerpSlider(Slider customSlider,int finalvalue)
    {
        if (lerpingSlider == false)
        {
            lerpingSlider = true;
            float speed = 2;
            float timeScale = 0;
            float startvalue =(float) customSlider.value;
            float f = (float)finalvalue;
            while (timeScale < 1)
            {
                customSlider.value = Mathf.Lerp(startvalue, finalvalue, timeScale);
                timeScale += Time.deltaTime * speed;
                yield return null;
            }
            lerpingSlider = false;
        }
    }
    private IEnumerator LerpSlider1(Slider customSlider, int finalvalue)
    {
        if (lerpingSlider1 == false)
        {
            lerpingSlider1 = true;
            float speed = 2;
            float timeScale = 0;
            float startvalue = (float)customSlider.value;
            float f = (float)finalvalue;
            while (timeScale < 1)
            {
                customSlider.value = Mathf.Lerp(startvalue, finalvalue, timeScale);
                timeScale += Time.deltaTime * speed;
                yield return null;
            }
            lerpingSlider1 = false;
        }
    }
    private IEnumerator LerpSlider2(Slider customSlider, int finalvalue)
    {
        if (lerpingSlider2 == false)
        {
            lerpingSlider2 = true;
            float speed = 2;
            float timeScale = 0;
            float startvalue = (float)customSlider.value;
            float f = (float)finalvalue;
            while (timeScale < 1)
            {
                customSlider.value = Mathf.Lerp(startvalue, finalvalue, timeScale);
                timeScale += Time.deltaTime * speed;
                yield return null;
            }
            lerpingSlider2 = false;
        }
    }
    private IEnumerator LerpSlider3(Slider customSlider, int finalvalue)
    {
        if (lerpingSlider3 == false)
        {
            lerpingSlider3 = true;
            float speed = 2;
            float timeScale = 0;
            float startvalue = (float)customSlider.value;
            float f = (float)finalvalue;
            while (timeScale < 1)
            {
                customSlider.value = Mathf.Lerp(startvalue, finalvalue, timeScale);
                timeScale += Time.deltaTime * speed;
                yield return null;
            }
            lerpingSlider3 = false;
        }
    }
    public bool InternetConnectivity()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Mute()
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            SoundManager.instance.SetSoundFx(1);
        }
        else if (SoundManager.instance.GetSoundFx() == 1)
        {
            SoundManager.instance.SetSoundFx(0);
        }
        SoundUI(SoundManager.instance.GetSoundFx());
        SoundManager.instance.GameMusic();
    }
    void SoundUI(int i)
    {
        if (i == 0)
            SoundBtn.sprite = SoundOn;
        else
            SoundBtn.sprite = SoundOff;
    }
    void CameraAppearance(int index)
    {
        for (int i = 0; i < Cams.Length; i++)
        {
            if (index == i)
                Cams[i].SetActive(true);
            else
                Cams[i].SetActive(false);
        }
    }
    public void PropertiesConfirmationPanelUi(int index)
    {
        Audio(Click);
        switch ((Properties)index)
        {
            case Properties.accel:
                PropertiesConfirmationtxt.text = "Do You Wanna Increase the Accelration by Spending 10 Coins ?";
                PropertiesIndex = index;
                break;
            case Properties.speed:
                PropertiesConfirmationtxt.text = "Do You Wanna Increase the Speed by Spending 10 Coins ?";
                PropertiesIndex = index;
                break;
            case Properties.braking:
                PropertiesConfirmationtxt.text = "Do You Wanna Increase the Braking by Spending 10 Coins ?";
                PropertiesIndex = index;
                break;
            case Properties.Handling:
                PropertiesConfirmationtxt.text = "Do You Wanna Increase the Handling by Spending 10 Coins ?";
                PropertiesIndex = index;
                break;
        }
        PropertiesConfirmation.Toggle();
    }
    public void ConfirmationProperties_yes()
    {
        Audio(Click);
        switch ((Properties)PropertiesIndex)
        {
            case Properties.accel:
                if (DataManager.instance.GetCoins() >= 10)
                {
                    DataManager.instance.SetCoins(DataManager.instance.GetCoins() - 10);
                    AccelInc();
                }
                else
                {
                    Donnothaveenoughcoins.Toggle();
                }
                break;
            case Properties.speed:
                if (DataManager.instance.GetCoins() >= 10)
                {
                    DataManager.instance.SetCoins(DataManager.instance.GetCoins() - 10);
                    SpeedInc();
                }
                else
                {
                    Donnothaveenoughcoins.Toggle();
                }
                break;
            case Properties.braking:
                if (DataManager.instance.GetCoins() >= 10)
                {
                    DataManager.instance.SetCoins(DataManager.instance.GetCoins() - 10);
                    BrakingInc();
                }
                else
                {
                    Donnothaveenoughcoins.Toggle();
                }
                break;
            case Properties.Handling:
                if (DataManager.instance.GetCoins() >= 10)
                {
                    DataManager.instance.SetCoins(DataManager.instance.GetCoins() - 10);
                    HandlingInc();
                }
                else
                {
                    Donnothaveenoughcoins.Toggle();
                }
                break;
        }
        PropertiesConfirmation.Toggle();
    }
    void SetPlayerNameTag(int index)
    {
        NameTag.transform.SetSiblingIndex(index);
        for (int i = 0; i < Selection_Obj.childCount; i++)
        {
            Selection_Obj.transform.GetChild(i).gameObject.SetActive(true);
            Debug.Log(Selection_Obj.transform.GetChild(i).gameObject.activeSelf);
        }
        Selection_Obj.transform.GetChild(index+1).gameObject.SetActive(false);
    }
    public void Audio(AudioClip _audioClip)
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            _audiosource.PlayOneShot(_audioClip);
        }
    }
}
[System.Serializable]
public class VehicleProperties
{
    public GameObject Vehicle;
    public string VehicleName;
    public int VehiclePrice;
}
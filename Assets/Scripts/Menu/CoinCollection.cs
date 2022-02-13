using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    bool IsActive = true;
    GameObject CoinObj;
    public AudioSource _coinsAudio;
    void Start()
    {
        CoinObj = transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CoinPick();
        }
    }
    void CoinPick()
    {
        if (IsActive == true)
        {
            IsActive = false;
            CoinObj.SetActive(false);
            UiManager.instance.CoinsAnimation();
            StartCoroutine(_coins());
        }
    }
    IEnumerator _coins()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.008f);
            GameManager.instance.Coins++;
            Audio();
            UiManager.instance.CoinsUI(GameManager.instance.Coins.ToString());
            UiManager.instance.CoinsTxtAnimation();
        }
    }
    public void Audio()
    {
        if (SoundManager.instance.GetSoundFx() == 0)
        {
            _coinsAudio.Play();
        }
    }
}

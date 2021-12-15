using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenFunx : MonoBehaviour
{

    public GameObject goPanelDurdur;
    public GameObject goPanelOyun;
    public List<GameObject> selectedTiles;
    public GameObject goPanelGameOver;
    public QuestedGame qGame;
    Image imgSes;
    public int skor = 0;
    public bool isPaused = false, isFinished = false, isGameOver = false;
    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "ClassicModeSelector")
            imgSes = GameObject.Find("SoundOnOff").GetComponent<Image>();

        SetSoundState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SwitchSound()
    {
        if (imgSes.sprite.name == "soundoff")
        {
            imgSes.sprite = Resources.Load<Sprite>("UIElements/soundon");
            GameObject.Find("backMusic").GetComponent<AudioSource>().Play();
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            imgSes.sprite = Resources.Load<Sprite>("UIElements/soundoff");
            GameObject.Find("backMusic").GetComponent<AudioSource>().Stop();
            PlayerPrefs.SetInt("sound", 0);
        }
    }
    public void GoToUrl(string url)
    {
        Application.OpenURL(url);
    }
    private void SetSoundState()
    {
        AudioSource aud = GameObject.Find("backMusic").GetComponent<AudioSource>();
        if (aud != null)
        {
            if (PlayerPrefs.GetInt("sound") == 0)
            {
                imgSes.sprite = Resources.Load<Sprite>("UIElements/soundoff");
                if (aud.isPlaying)
                    aud.Stop();
            }
            else
            {
                imgSes.sprite = Resources.Load<Sprite>("UIElements/soundon");
                if (!aud.isPlaying)
                    aud.Play();
            }
        }
    }
    public void PauseGame()
    {
        if (!isGameOver)
        {
            goPanelDurdur.SetActive(true);
            goPanelOyun.SetActive(false);
            isPaused = true;
        }

        //Durdurma anındaki diğer kodlar

    }
    public void ContinueGame()
    {
        goPanelOyun.SetActive(true);
        goPanelDurdur.SetActive(false);
        isPaused = false;
        //Devam anındaki diğer kodlar
    }
    public void GameOver()
    {
        if (!isPaused)
        {
            goPanelDurdur.SetActive(false);
            goPanelOyun.SetActive(false);
            goPanelGameOver.SetActive(true);
            isGameOver = true;
        }
    }
    public bool ControlMatch(List<GameObject> selecteds)
    {
        string ilkSayi = selecteds[0].GetComponent<SpriteRenderer>().sprite.name;
        string esitsizlik = selecteds[1].GetComponent<SpriteRenderer>().sprite.name;
        string ikinciSayi = selecteds[2].GetComponent<SpriteRenderer>().sprite.name;
        bool stillControlling = true;
        byte sayi1 = 0, sayi2 = 0;

        //İlk eleman sayı ise sayi1 e at
        if (ilkSayi.Length == 1)
            sayi1 = Convert.ToByte(ilkSayi);
        else
            stillControlling = false;

        //İkinci eleman sayı ise iptal et
        if (esitsizlik.Length == 1)
            stillControlling = false;

        //Üçüncü eleman sayı ise sayi2 ye at
        if (ikinciSayi.Length == 1)
            sayi2 = Convert.ToByte(ikinciSayi);
        else
            stillControlling = false;


        if (stillControlling && SelectAndControl(sayi1, sayi2, esitsizlik))
            return true;
        else
            return false;
    }
    private bool SelectAndControl(byte s1, byte s2, string controlTo)
    {
        switch (controlTo)
        {
            case "btnEsittir":
                return s1 == s2;
            case "btnKucuktur":
                return s1 < s2;
            case "btnBuyuktur":
                return s1 > s2;
            case "btnEsitDegil":
                return s1 != s2;

            default:
                return false;
        }
    }
    public string ButtonNameToChar(string buttonName)
    {
        switch (buttonName)
        {
            case "btnEsittir":
                return "=";
            case "btnKucuktur":
                return "<";
            case "btnBuyuktur":
                return ">";
            case "btnEsitDegil":
                return "≠";
            default:
                return buttonName;
        }
    }
    public bool AmIAtThisScene(string sceneName)
    {
        return SceneManager.GetActiveScene().name == sceneName;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestedGame : MonoBehaviour
{

    string questLevel;
    public GameObject[] prefabs;
    public int x, y;
    private GenFunx genFunx;
    private GameObject goPanelTebrik;
    public bool questComplete = false;
    float time = 90;
    public bool isCounting = true, isPaused = false, isGameOver = false, isFinished = false;


    void Start()
    {
        questLevel = PlayerPrefs.GetString("currentQuest");
        Text lblQuest = GameObject.Find("LastTouch").GetComponent<Text>();
        switch (questLevel)
        {
            case "Bölüm 1":
                lblQuest.text = "66 Puan Topla";
                break;
            case "Bölüm 2":
                lblQuest.text = "132 Puan Topla";
                break;
            case "Bölüm 3":
                lblQuest.text = "10 Tane 6 Yok Et";
                break;
            case "Bölüm 4":
                lblQuest.text = "30 Tane 6 Yok Et";
                break;
            case "Bölüm 5":
                lblQuest.text = "50 Parça Yok Et";
                break;
            default:
                break;
        }
        goPanelTebrik = GameObject.Find("FinishScreen");
        Uret(prefabs, x, y);
        genFunx = GameObject.Find("Main Camera").GetComponent<GenFunx>();
        PlayerPrefs.SetInt("tempSixCount", 0);
        PlayerPrefs.SetInt("tempDestroyedCount", 0);

        genFunx.goPanelGameOver.SetActive(false);
        genFunx.goPanelDurdur.SetActive(false);
        goPanelTebrik.SetActive(false);

    }
    void Update()
    {
        UpdateUI();

        GameObject.Find("lblTime").GetComponent<Text>().text = Mathf.Round(time).ToString();

        isPaused = genFunx.isPaused;
        isFinished = genFunx.isFinished;
        isGameOver = genFunx.isGameOver;

        if (!isPaused)
        {
            if (time > 0) time -= Time.deltaTime;
            else genFunx.GameOver();
        }
    }
    public void UpdateUI()
    {
        GameObject.Find("lblPuan").GetComponent<Text>().text = genFunx.skor.ToString();
    }
    public void Uret(GameObject[] prefabs, int cols, int rows)
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector2 tempPos = new Vector2((i * 120.0F) - 240, (j * 120.0F) - 240);
                GameObject tile = Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length)], tempPos, Quaternion.identity, this.transform);
                tile.name = string.Format("({0},{1})", i, j);
            }
        }
    }
    public void FinishQuest()
    {
        genFunx.goPanelDurdur.SetActive(false);
        genFunx.goPanelGameOver.SetActive(false);
        genFunx.goPanelOyun.SetActive(false);
        goPanelTebrik.SetActive(true);
        isPaused = true;
        //Oyun bitince yapılacak diğer işler
    }
}

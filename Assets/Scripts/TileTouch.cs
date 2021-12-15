using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TileTouch : MonoBehaviour
{
    QuestedGame qGame;
    EndlessGame eGame;
    GenFunx genFunx;

    void Start()
    {
        genFunx = GameObject.Find("Main Camera").GetComponent<GenFunx>();
        qGame = GameObject.Find("Playground").GetComponent<QuestedGame>();
        eGame = GameObject.Find("Playground").GetComponent<EndlessGame>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        GameObject.Find("TapSound").GetComponent<AudioSource>().Play();
        if (!genFunx.selectedTiles.Contains(gameObject))
        {
            genFunx.selectedTiles.Add(gameObject);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

            if (genFunx.selectedTiles.Count == 1)
                GameObject.Find("LastTouch").GetComponent<Text>().text = "";

            GameObject.Find("LastTouch").GetComponent<Text>().text += genFunx.ButtonNameToChar(gameObject.GetComponent<SpriteRenderer>().sprite.name) + " ";

            if (genFunx.selectedTiles.Count == 3)
            {
                if (genFunx.ControlMatch(genFunx.selectedTiles))
                {
                    //Puan ekle
                    int puan = Convert.ToByte(genFunx.selectedTiles[0].GetComponent<SpriteRenderer>().sprite.name) + Convert.ToByte(genFunx.selectedTiles[2].GetComponent<SpriteRenderer>().sprite.name);
                    genFunx.skor += puan;
                    //En yüksek pauna 
                    if (genFunx.skor > PlayerPrefs.GetInt("highScore", 0))
                        PlayerPrefs.SetInt("highScore", genFunx.skor);
                    GameObject.Find("LastTouch").GetComponent<Text>().text += " Doğru! +" + puan.ToString();

                    if (genFunx.AmIAtThisScene("QuestedGame"))
                    {

                        string currentQuest = PlayerPrefs.GetString("currentQuest");
                        switch (currentQuest)
                        {
                            case "Bölüm 1":
                                if (genFunx.skor >= 66)
                                    qGame.questComplete = true;
                                break;
                            case "Bölüm 2":
                                if (genFunx.skor >= 132)
                                    qGame.questComplete = true;
                                break;
                            case "Bölüm 3":
                                if (PlayerPrefs.GetInt("tempSixCount", 0) > 9)
                                    qGame.questComplete = true;
                                break;
                            case "Bölüm 4":
                                if (PlayerPrefs.GetInt("tempSixCount", 0) > 9)
                                    qGame.questComplete = true;
                                break;
                            case "Bölüm 5":
                                if (PlayerPrefs.GetInt("tempDestroyedCount", 0) > 9)
                                    qGame.questComplete = true;
                                break;
                            default:
                                genFunx.SwitchScreen("MainScene");
                                break;
                        }

                        if (qGame.questComplete)
                            qGame.FinishQuest();

                    }
                }
                else
                {
                    //Titret
                    int puan = genFunx.skor * 30 / 100;
                    GameObject.Find("LastTouch").GetComponent<Text>().text = "Yanlış Eşleşme! -" + puan.ToString();
                    genFunx.skor -= puan;
                }
                //Prefabları al
                GameObject[] prefabs;
                if (genFunx.AmIAtThisScene("QuestedGame"))
                    prefabs = qGame.prefabs;
                else
                    prefabs = eGame.prefabs;


                //Yok et ve yenilerini ver
                float tempY = 260;

                if (genFunx.goPanelOyun.activeInHierarchy)
                {
                    foreach (GameObject tile in genFunx.selectedTiles)
                    {
                        string spriteName = tile.GetComponent<SpriteRenderer>().sprite.name;
                        //Yok edilenler sayacı
                        PlayerPrefs.SetInt("destroyedCount", PlayerPrefs.GetInt("destroyedCount", 0) + 1);

                        //6 Sayacı
                        if (spriteName == "6")
                            PlayerPrefs.SetInt("sixCount", PlayerPrefs.GetInt("sixCount", 0) + 1);

                        if (genFunx.AmIAtThisScene("QuestedGame"))
                        {
                            if (spriteName == "6")
                                PlayerPrefs.SetInt("tempSixCount", PlayerPrefs.GetInt("tempSixCount", 0) + 1);

                            PlayerPrefs.SetInt("tempDestroyedCount", PlayerPrefs.GetInt("tempDestroyedCount", 0) + 1);
                        }

                        Vector2 tempPos = tile.transform.position;
                        tempPos.y = tempY;

                        GameObject newTile = Instantiate(prefabs[UnityEngine.Random.Range(0, prefabs.Length)], tempPos, Quaternion.identity, genFunx.goPanelOyun.GetComponent<Transform>());
                        newTile.name = tile.name;

                        Destroy(tile);
                        tempY += 30;
                    }
                }
                genFunx.selectedTiles.Clear();
            }
        }
        else
        {
            genFunx.selectedTiles.Remove(gameObject);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            GameObject.Find("LastTouch").GetComponent<Text>().text = "";
            foreach (GameObject tile in genFunx.selectedTiles)
            {
                GameObject.Find("LastTouch").GetComponent<Text>().text += genFunx.ButtonNameToChar(tile.GetComponent<SpriteRenderer>().sprite.name) + " ";
            }
        }
    }
}

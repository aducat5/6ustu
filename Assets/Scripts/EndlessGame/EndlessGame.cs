using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessGame : MonoBehaviour
{

    public GameObject[] prefabs;
    public int x, y;
    private GenFunx genFunx;

    void Start()
    {
        Uret(prefabs, x, y);
        genFunx = GameObject.Find("Main Camera").GetComponent<GenFunx>();
    }
    
    void Update()
    {
        UpdateUI();
        if (genFunx.AmIAtThisScene("EndlessGame"))
        {
            int unevenCount = 0;
            foreach (Transform tile in gameObject.GetComponent<Transform>())
            {
                if (tile.GetComponent<SpriteRenderer>().sprite.name.Length > 1)
                    unevenCount++;
            }
            if (unevenCount == 0)
                genFunx.GameOver();
        }
    }

    public void UpdateUI()
    {
        GameObject.Find("lblPuan").GetComponent<Text>().text = genFunx.skor.ToString();
        GameObject.Find("lblBest").GetComponent<Text>().text = PlayerPrefs.GetInt("highScore", 0).ToString();

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
}


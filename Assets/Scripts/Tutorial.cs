using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject[] prefabs;
    public int x, y;
    private GenFunx genFunx;

    public void ResmiGizleGoster(bool durum, string objAdi)
    {
        GameObject go = GameObject.Find(objAdi);
        Image resimComp = go.GetComponent<Image>();
        resimComp.enabled = durum;
    }

    public void Start()
    {
        Uret(prefabs, x, y);
        genFunx = GameObject.Find("Main Camera").GetComponent<GenFunx>();

        //ResmiGizleGoster(false, "tutorial2");
        //ResmiGizleGoster(false, "tutorial3");
        //ResmiGizleGoster(false, "tutorial4");
        //ResmiGizleGoster(true, "tutorial1");
    }

    public void Uret(GameObject[] prefabs, int cols, int rows)
    {
        //for (int i = 0; i < cols; i++)
        //{
        //    for (int j = 0; j < rows; j++)
        //    {
        //        Vector2 tempPos = new Vector2((i * 120.0F) - 240, (j * 120.0F) /- /240);
        //        GameObject tile = Instantiate(prefabs[UnityEngine.Random.Range/(0, /prefabs.Length)], tempPos, Quaternion.identity, this.transform);
        //        tile.name = string.Format("({0},{1})", i, j);
        //    }
        //}


        //First Tile
        Vector2 firstTilePos = new Vector2((3 * 120.0F) - 240, (5 * 120.0F) - 240);
        GameObject firstTile = Instantiate(prefabs[1], firstTilePos, Quaternion.identity, this.transform);
        firstTile.name = string.Format("({0},{1})", 2, 0);

        //Second Tile
        Vector2 secondTilePos = new Vector2((4 * 120.0F) - 240, (5 * 120.0F) - 240);
        GameObject secondTile = Instantiate(prefabs[2], firstTilePos, Quaternion.identity, this.transform);
        firstTile.name = string.Format("({0},{1})", 3, 0);

    }

    public void UpdateUI()
    {
        GameObject.Find("lblPuan").GetComponent<Text>().text = genFunx.skor.ToString();
        GameObject.Find("lblBest").GetComponent<Text>().text = PlayerPrefs.GetInt("highScore", 0).ToString();

    }

    void Update()
    {
        UpdateUI();
        if (genFunx.AmIAtThisScene("Tutorial"))
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

    public void SonrakineGec()
    {
        Image resim = GameObject.Find("TutorialImage").GetComponent<Image>();

        switch (resim.sprite.name)
        {
            case "tutorial1":
                resim.sprite = Resources.Load<Sprite>("TutorialImages/tutorial2");
                break;
            case "tutorial2":
                resim.sprite = Resources.Load<Sprite>("TutorialImages/tutorial3");
                break;
            case "tutorial3":
                resim.sprite = Resources.Load<Sprite>("TutorialImages/tutorial4");
                Image homeButton = GameObject.Find("NextButton").GetComponent<Image>();
                homeButton.sprite = Resources.Load<Sprite>("Buttons/homeButtonv2");
                break;
            case "tutorial4":
                SceneManager.LoadScene("MainScene");
                break;
            default:
                break;
        }
    }
}

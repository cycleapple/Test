using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Use this for initialization
    private void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void CreateLevel()
    {
        string[] mapData = ReadLevelText();

        //計算地圖的X
        int mapXsize = mapData[0].ToCharArray().Length;

        //計算地圖的Y
        int mapYsize = mapData.Length;

        //計算世界起點aka遊戲鏡頭的醉左上角
        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        Debug.Log(worldStartPosition);
        for (int y = 0; y < mapYsize; y++) //Y的位置
        {
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapXsize; x++) //X的位置
            {
                PlaceTile(newTiles[x].ToString(), x, y, worldStartPosition);
            }
        }
    }

    //PlaceTile Function 建立mapTile的prefab 以及讓他並排
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStartPosition)
    {
        int tileIndex = int.Parse(tileType);

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStartPosition.x + (TileSize * x), worldStartPosition.y - (TileSize * y), 0);
    }

    private string[] ReadLevelText()
    {
        //TextAsset bindData = Resources.Load("Level") as TextAsset;
        //string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        //return data.Split('-');
        string path = @Application.dataPath + "/Config/Level.txt".Replace(Environment.NewLine, string.Empty);
        string data = System.IO.File.ReadAllText(path);
        return data.Split('-');
    }
}
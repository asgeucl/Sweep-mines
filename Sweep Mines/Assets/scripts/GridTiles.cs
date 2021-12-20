using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTiles : MonoBehaviour
{
    public Sprite sprite;
    public int AmountOfMines;
    public string[,] Grid;
    int Vertical, Horizontal, Columns, Rows;
    public List<Mine> Mines = new List<Mine>(); // List with the Mines
    // Start is called before the first frame update
    void Start()
    {
        Vertical = (int)Camera.main.orthographicSize;
        Horizontal = Vertical * (Screen.width / Screen.height);
        Columns = Horizontal * 2;
        Rows = Vertical * 2;
        Grid = new string[Columns, Rows];
        for (int i = 0; i < Columns; i++) // generate grid
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, j] = i.ToString() + j.ToString();
            }
        }
        GenerateMines(Grid);
        CheckAdjacent(Grid);
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                SpawnTile(i, j, Grid[i, j]);
            }
        }
    }
    private void SpawnTile(int x, int y, string value) // spawn the tileset 
                                                       // TODO : let the user set the amount of tiles and fit them accordingly to the screen size
    {
        GameObject g = new GameObject(value);
        g.transform.position = new Vector3(x - (Horizontal - 0.5f), y - (Vertical - 0.5f));
        var c = g.AddComponent<SpriteRenderer>();
        if (value == "Mine") c.color = new Color(255, 0, 0);
        c.sprite = sprite;



    }
    private void GenerateMines(string[,] grid) // generate the mines based on the amount of tiles
    {
        int numberOfTiles = 0; // this might be deprecated at some point when the user can type in the amount of tiles
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                numberOfTiles += 1;

            }
        }
        AmountOfMines = numberOfTiles / 10;
        AmountOfMines = 1;
        // if (AmountOfMines <= 3) AmountOfMines = 5; // no grid too small

        for (int i = 0; i < AmountOfMines; i++)
        {
            Mine mine = new Mine();
            var row = Random.Range(0, Rows);
            var column = Random.Range(0, Columns);
            Grid[column, row] = "Mine";
        }


    }
    private void CheckAdjacent(string[,] grid)
    { // check adjacent cells for numbers
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                int count = 0;
                if (x != 0 && grid[x, y - 1] == "Mine")
                { // up
                    count += 1;
                }
                if (x != 0 && grid[x + 1, y - 1] == "Mine")
                { // up right
                    count += 1;
                }
                if (grid[x + 1, y] == "Mine")
                { // right
                    count += 1;
                }
                if (grid.GetLength(0) != 0 && grid[x + 1, y + 1] == "Mine")
                { // right down
                    count += 1;
                }
                if (grid.GetLength(0) != 0 && grid[x, y + 1] == "Mine")
                { //down
                    count += 1;
                }
                if (y == 0 && grid.GetLength(0) != 0 && grid[x - 1, y + 1] == "Mine")
                { // left down
                    count += 1;
                }
                if (y == 0 && grid[x - 1, y] == "Mine")
                { // left
                    count += 1;
                }
                if (x != 0 && y == 0 && grid[x - 1, y - 1] == "Mine")
                { // left up
                    count += 1;
                }
                Grid[x, y] = count.ToString();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

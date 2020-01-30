using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_WaterSpout : MonoBehaviour
{
    public class Tile
    {
        public Tile(int id) { ID = id; }
        public bool isEmpty = false;
        public int ID;
        public Tile up = null;
        public Tile down = null;
        public Tile left = null;
        public Tile right = null;
    }

    public bool isClear = false;
    public int emptyTileID = 3;
    public Image[] imagies;
    public Tile[,] tiles = new Tile[4,4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                tiles[i, j] = new Tile(i * 4 + j);
                if (tiles[i, j].ID == emptyTileID) tiles[i, j].isEmpty = true;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
               // Debug.Log(tiles[i, j].ID + "===============");
                if(i > 0)
                {
                    tiles[i,j].up =  tiles[i - 1, j];
                   // Debug.Log(tiles[i, j].up.ID);
                }
                if(i < 3)
                {
                    tiles[i, j].down = tiles[i + 1, j];
                  //  Debug.Log(tiles[i, j].down.ID);
                }
                if (j > 0)
                {
                    tiles[i, j].left = tiles[i, j - 1];
                  //  Debug.Log(tiles[i, j].left.ID);
                }
                if (j < 3)
                {
                    tiles[i, j].right = tiles[i, j + 1];
                  //  Debug.Log(tiles[i, j].right.ID);
                }
                Tile tempTile = tiles[i, j];
                imagies[i * 4 + j].GetComponent<Button>().onClick.AddListener(() => ChangeTile(tempTile));
            }
        }
    }

    public void ChangeTile(Tile tile)
    {
        Tile emptyTile = null;
        if(tile.up != null && tile.up.isEmpty)
        {
            emptyTile = tile.up;
        }
        if (tile.down != null && tile.down.isEmpty)
        {
            emptyTile = tile.down;
        }
        if (tile.left != null && tile.left.isEmpty)
        {
            emptyTile = tile.left;
        }
        if (tile.right != null && tile.right.isEmpty)
        {
            emptyTile = tile.right;
        }
        //Debug.Log(tile.ID);
        if (emptyTile != null)
        {
            Sprite tempImg = imagies[tile.ID].sprite;
            imagies[tile.ID].sprite = imagies[emptyTile.ID].sprite;
            imagies[emptyTile.ID].sprite = tempImg;

            //int tempID = tile.ID;
            //tile.ID = emptyTile.ID;
            //emptyTile.ID = tempID;

            tile.isEmpty = true;
            emptyTile.isEmpty = false;
            
        }
        
    }
}

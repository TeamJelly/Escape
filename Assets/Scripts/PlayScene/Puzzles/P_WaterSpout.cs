using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_WaterSpout : MonoBehaviour
{
    public class TileBox
    {
        public TileBox(int id) { ID = id; }
        public bool isEmpty = false;
        public int ID;
        public TileBox up = null;
        public TileBox down = null;
        public TileBox left = null;
        public TileBox right = null;
    }
    public class Tile
    {
        
    }
    public bool isClear = false;
    public int emptyTileBoxID = 3;
    public Image[] imagies;
    public TileBox[,] TileBoxies = new TileBox[4,4];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                TileBoxies[i, j] = new TileBox(i * 4 + j);
                if (TileBoxies[i, j].ID == emptyTileBoxID) TileBoxies[i, j].isEmpty = true;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
               // Debug.Log(TileBoxs[i, j].ID + "===============");
                if(i > 0)
                {
                    TileBoxies[i,j].up = TileBoxies[i - 1, j];
                   // Debug.Log(TileBoxs[i, j].up.ID);
                }
                if(i < 3)
                {
                    TileBoxies[i, j].down = TileBoxies[i + 1, j];
                  //  Debug.Log(TileBoxs[i, j].down.ID);
                }
                if (j > 0)
                {
                    TileBoxies[i, j].left = TileBoxies[i, j - 1];
                  //  Debug.Log(TileBoxs[i, j].left.ID);
                }
                if (j < 3)
                {
                    TileBoxies[i, j].right = TileBoxies[i, j + 1];
                  //  Debug.Log(TileBoxs[i, j].right.ID);
                }
                TileBox tempTileBox = TileBoxies[i, j];
                imagies[i * 4 + j].GetComponent<Button>().onClick.AddListener(() => ChangeTileBox(tempTileBox));
            }
        }
    }

    public void ChangeTileBox(TileBox TileBox)
    {
        TileBox emptyTileBox = null;
        if(TileBox.up != null && TileBox.up.isEmpty)
        {
            emptyTileBox = TileBox.up;
        }
        if (TileBox.down != null && TileBox.down.isEmpty)
        {
            emptyTileBox = TileBox.down;
        }
        if (TileBox.left != null && TileBox.left.isEmpty)
        {
            emptyTileBox = TileBox.left;
        }
        if (TileBox.right != null && TileBox.right.isEmpty)
        {
            emptyTileBox = TileBox.right;
        }
        //Debug.Log(TileBox.ID);
        if (emptyTileBox != null)
        {
            Sprite tempImg = imagies[TileBox.ID].sprite;
            imagies[TileBox.ID].sprite = imagies[emptyTileBox.ID].sprite;
            imagies[emptyTileBox.ID].sprite = tempImg;

            //int tempID = TileBox.ID;
            //TileBox.ID = emptyTileBox.ID;
            //emptyTileBox.ID = tempID;

            TileBox.isEmpty = true;
            emptyTileBox.isEmpty = false;
            
        }
        
    }
}

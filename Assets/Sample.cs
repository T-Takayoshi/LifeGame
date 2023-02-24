
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//using TMPro;

public class Sample : MonoBehaviour, IPointerClickHandler
{


    //時間を図る
    //private float time = 0.0f;
    //時間を表示する
    //public TMP_Text timerText;

    [SerializeField]
    private int Size = 10000;

    private GameObject[,] _cells;
    private int[,] _cells_collor;
    private int[,] _cells_AD;


    //[SerializeField]
    //private int _row = 5;

    //[SerializeField]
    //private int _column = 5;


    //private float point_x = 0, point_y = 0;

    void Start()
    {
        //timerText を所得
        //timerText = TMP_Text.Find("timerText");

        //Sizeで確認した数だけの行数のセルを作る
        _cells = new GameObject[Size, Size];
        _cells_collor = new int[Size, Size];
        _cells_AD = new int[Size, Size];
        for (var r = 0; r < _cells.GetLength(0); r++)
        {
            for (var c = 0; c < _cells.GetLength(1); c++)
            {
                var cell = new GameObject($"Cell({r},{c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();
                _cells[r, c] = cell;

                _cells_collor[r, c] = 0;
                _cells_AD[r, c] = 0;

            }
        }

    }


    //　ポーズした時に表示するUIのプレハブ
    private int p = 0;

    //　ポーズUIのインスタンス
    private GameObject pauseUIInstance;

    float Dtime = 0;
    float Set_time = 1;
    void Update()
    {

        int m = 0;
        if (Input.GetKeyDown("q"))
        {
            if (p==1)
            {
                
                Time.timeScale = 0f;
                p=0;
            }
            else
            {
                
                

                Time.timeScale = 1f;
                p = 1;
            }
        }

        for (var r = 0; r < Size; r++)
        {
            for (var c = 0; c < Size; c++)
            {
                var image2 = _cells[r, c].GetComponent<Image>();

                if (image2.color == Color.red)
                {
                    //以下に各セルで行いたい処理を行う
                    Debug.Log("cell" + r + c + "soart");
                    if (_cells_collor[r, c] == 1)
                    {
                        Debug.Log("cell" + r + c + "reset white");
                        image2.color = Color.white;
                        _cells_collor[r, c] = 0;

                    }
                    else if (_cells_collor[r, c] == 0)
                    {
                        Debug.Log("cell" + r + c + "reset black");
                        image2.color = Color.black;
                        _cells_collor[r, c] = 1;

                    }

                }

            }
        }

        if (Input.GetKeyDown("r"))
        {
            for (var r = 0; r < Size; r++)
            {
                for (var c = 0; c < Size; c++)
                {

                    int rand = Random.Range(0, 3);
                    _cells_AD[r, c] = rand;

                    Debug.Log("cell" + r + c + "="+ _cells_AD[r, c]);
                    if (_cells_AD[r, c] == 1)
                    {

                        var image2 = _cells[r, c].GetComponent<Image>();

                        image2.color = Color.black;
                        _cells_collor[r, c] = 1;
                    }
                    else if (_cells_AD[r, c] == 0)
                    {
                        var image2 = _cells[r, c].GetComponent<Image>();

                        image2.color = Color.white;
                        _cells_collor[r, c] = 0;
                    }
                    else { }
                }
            }
        }

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        //時間が止められているときはこれより下を動作させない


        Dtime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(Set_time>0.1f)
            Set_time -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Set_time > 1f)
                Set_time += 0.1f;
        }

        if (Dtime > Set_time)
        {

            for (var r = 0; r < Size; r++)
            {
                for (var c = 0; c < Size; c++)
                {

                    int life = 0;
                    if (r > 0)
                    {
                        var root_image = _cells[r - 1, c].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (r > 0&&c>0)
                    {
                        var root_image = _cells[r - 1, c-1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (c > 0)
                    {
                        var root_image = _cells[r, c-1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (c > 0&&r<Size-1)
                    {
                        var root_image = _cells[r+1, c-1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (r<Size-1)
                    {
                        var root_image = _cells[r+1, c].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (r<Size-1&&c<Size-1)
                    {
                        var root_image = _cells[r+1, c+1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (c < Size-1)
                    {
                        var root_image = _cells[r, c + 1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }
                    if (c<Size-1&&r>0)
                    {
                        var root_image = _cells[r-1, c+1].GetComponent<Image>();
                        if (root_image.color == Color.black)
                            life++;
                    }

                    if(life>0)
                        Debug.Log("cell" + r + c + "="+life);

                    if (life == 3)
                    {

                        _cells_AD[r, c] = 1;
                        ////
                    }else if (life >= 4 || life <= 1)
                    {

                        _cells_AD[r, c] = 0;
                        ////
                    }
                    else
                    {
                        _cells_AD[r, c] = 2;
                    }

                }

            }

            for (var r = 0; r < Size; r++)
            {
                for (var c = 0; c < Size; c++)
                {
                    if (_cells_AD[r, c] == 1)
                    {

                        var image2 = _cells[r, c].GetComponent<Image>();

                        image2.color = Color.black;
                        _cells_collor[r, c] = 1;
                    }
                    else if(_cells_AD[r, c] == 0)
                    {
                        var image2 = _cells[r, c].GetComponent<Image>();

                        image2.color = Color.white;
                        _cells_collor[r, c] = 0;
                    }
                    else { }
                }
            }
            Dtime = 0;
        }



        if (m == 0)
        {
            
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var image = cell.GetComponent<Image>();


        image.color = Color.red;



        for (var r = 0; r < Size; r++)
        {
            for (var c = 0; c < Size; c++)
            {
                var image2 = _cells[r, c].GetComponent<Image>();

                /*
                if (image2.color == Color.red)
                {
                    if (_cells_collor[r, c] == 0)
                    {
                        Debug.Log("cell" + r + c + "=" + _cells_collor[r, c]);

                    }
                    else if (_cells_collor[r, c] == 1)
                    {
                        Debug.Log("cell" + r + c + "=" + _cells_collor[r, c]);

                    }
                    if (r > 0)
                    {
                        image2 = _cells[r - 1, c].GetComponent<Image>();
                        image2.color = Color.blue;
                    }
                    if (r < Size - 1)
                    {
                        image2 = _cells[r + 1, c].GetComponent<Image>();
                        image2.color = Color.blue;
                    }
                    if (c > 0)
                    {
                        image2 = _cells[r, c - 1].GetComponent<Image>();
                        image2.color = Color.blue;
                    }
                    if (c < Size - 1)
                    {
                        image2 = _cells[r, c + 1].GetComponent<Image>();
                        image2.color = Color.blue;
                    }

                }
                */
            }
        }
    }

}
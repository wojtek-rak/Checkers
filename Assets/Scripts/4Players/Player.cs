﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public enum PlayerPosition { Bottom, Upper, Left, Right }

    public class Player : MonoBehaviour
    {

        public Object pwanPrefab;
        public List<PawnController> pawns = new List<PawnController>();

        //protected GameObject pawn => (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
        protected PlayerPosition playerPosition;

        private BoardController board;
        private float fieldSize;
        private float width;


        protected virtual void Start()
        {
            var tempBoard = GameObject.FindGameObjectWithTag("Board");
            var corner = GameObject.FindGameObjectWithTag("Corner").transform;
            board = tempBoard.GetComponent<BoardController>();
            width = 500f;// to do much better same as Board // tempBoard.GetComponent<RectTransform>().rect.width;
            fieldSize = width / 16f;
            pwanPrefab = Resources.Load("Pawn4");
            for (int i = 0; i < 12; i++)
            {
                var pawn = (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
                pawns.Add(pawn.GetComponent<PawnController>());
                pawns[i].playerPosition = playerPosition;
                pawn.transform.parent = corner;
            }
            SetUpPawns();
            SetUpColorPawns();

        }


        private void SetUpColorPawns()
        {
            Sprite sprite;
            switch (playerPosition)
            {
                case PlayerPosition.Upper:
                    sprite = Resources.Load<Sprite>("player_2");

                    foreach (var pawn in pawns)
                    {
                        pawn.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    break;
                case PlayerPosition.Bottom:
                    sprite = Resources.Load<Sprite>("player_1");
                    foreach (var pawn in pawns)
                    {
                        pawn.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    break;
                case PlayerPosition.Right:
                    Debug.Log("PLAYER_ TODO");
                    break;
                case PlayerPosition.Left:
                    Debug.Log("PLAYER_ TODO");
                    break;
            }
        }
        private void SetUpPawns()
        {
            var row = 0;
            var col = 0;
            var count = 0;
            switch (playerPosition)
            {
                case PlayerPosition.Upper:
                    row = 0;
                    col = 4;
                    while (count < pawns.Count)
                    {
                        var pawn = pawns[count];
                        if (board.fields[col, row].Free)
                        {
                            board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                            board.fields[col, row].playerPosition = PlayerPosition.Upper;
                            board.fields[col, row].Free = false;
                            pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f, width - row * fieldSize - fieldSize / 2f, 10f);
                            if (++col >= 12)
                            {
                                col = 4;
                                row += 1;
                            }
                            count += 1;
                        }
                        else
                        {
                            if (++col >= 12)
                            {
                                col = 4;
                                row += 1;
                            }
                        }
                    }
                    break;
                case PlayerPosition.Bottom:
                    row = 15;
                    col = 11;
                    while (count < pawns.Count)
                    {
                        var pawn = pawns[count];
                        if (board.fields[col, row].Free)
                        {
                            board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                            board.fields[col, row].Free = false;
                            board.fields[col, row].playerPosition = PlayerPosition.Bottom;
                            pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f, width - row * fieldSize - fieldSize / 2f, 10f);
                            if (--col <= 3)
                            {
                                col = 11;
                                row -= 1;
                            }
                            count += 1;
                        }
                        else
                        {
                            if (--col <= 3)
                            {
                                col = 11;
                                row -= 1;
                            }
                        }
                    }
                    break;
                case PlayerPosition.Right:
                    Debug.Log("PLAYER_ TODO");
                    break;
                case PlayerPosition.Left:
                    Debug.Log("PLAYER_ TODO");
                    break;
            }

        }

        ///// <summary>
        ///// testing
        ///// </summary>
        //private void Update()
        //{
        //    //<TESTING>
        //    if (!xd)
        //    {
        //        SetupEarlyQeenForSomeFun();
        //        xd = true;
        //    }

        //    //</TESTING?
        //}
        //private bool xd = false;
        ///// <summary>
        ///// TESTING METOD
        ///// </summary>
        //public void SetupEarlyQeenForSomeFun()
        //{
        //    if (playerPosition == PlayerPosition.Upper)
        //    {
        //        Debug.Log("upper done");
        //        board.fields[2, 2].PawnController.TransformToQueen();
        //        board.fields[2, 2].PawnController.state = State.Queen;
        //    }
        //    else
        //    {
        //        Debug.Log("bottom done");
        //        board.fields[1, 5].PawnController.TransformToQueen();
        //        board.fields[1, 5].PawnController.state = State.Queen;
        //    }

        //}
    }
}
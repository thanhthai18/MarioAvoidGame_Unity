using MarioGame.GUI;
using MarioGame.Player;
using MarioGame.TurtleController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioGame.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public Mario marioPrefab;
        public Turtle[] turtlePrefabs;

        public float spawnTime;
        int score;
        bool isGameover;
        bool isGamebegun;
        Mario marioClone;

        public int Score { get => score; set => score = value; }
        public bool IsGameover { get => isGameover; set => isGameover = value; }
        public bool IsGamebegun { get => isGamebegun; }

        public override void Awake()
        {
            MakeSingleton(false);
        }

        public override void Start()
        {
            GameGUIManager.Ins.ShowGameGui(false);
        }

        public void PlayGame()
        {
            if (marioPrefab)
                marioClone = Instantiate(marioPrefab, new Vector3(0f, -2.7f, 0f), Quaternion.identity);

            StartCoroutine(Spawn());

            GameGUIManager.Ins.ShowGameGui(true);
        }


        IEnumerator Spawn()
        {
            isGamebegun = true;
            yield return new WaitForSeconds(2f);
            if (turtlePrefabs != null && turtlePrefabs.Length > 0)
            {
                while (!IsGameover)
                {
                    int randIdx = Random.Range(0, turtlePrefabs.Length);
                    if(turtlePrefabs[randIdx] != null)
                    {
                        Instantiate(turtlePrefabs[randIdx], new Vector3((marioClone.transform.position.x), Random.Range(4f, 8f), 0f), Quaternion.identity);
                    }

                    yield return new WaitForSeconds(spawnTime);
                }
            }
            yield return null;
        }
    }
}


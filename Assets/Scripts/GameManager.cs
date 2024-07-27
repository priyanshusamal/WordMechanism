using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WordRunner
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public string currentWord;
        public Transform spelledWord;

        public GameObject bottomL1Obj;
        public GameObject bottomL2Obj;
        public GameObject bottomL3Obj;
        public GameObject bottomL4Obj;

        public GameObject[] Tile;

        public List<string> availLetter1 = new List<string>() { "W" };
        public List<string> availLetter2 = new List<string>() { "I" };
        public List<string> availLetter3 = new List<string>() { "N" };
        public List<string> availLetter4 = new List<string>() { "E" };

        public KeyCode RMB;
        public int wordLen;
        public string word1L = "WIN";
        public string word2L = "WINE";
        public string word3L = "WINNE";
        public List<string> selectLetter = new List<string>() { "", "", "", "", "", "", "", "" };
        public int letterNum = 0;
        public GameObject WinScreen;
        public GameObject WinObject;
        // Start is called before the first frame update
        void Start()
        {
            bottomL1Obj.GetComponent<TextMesh>().text = availLetter1[0];
            bottomL2Obj.GetComponent<TextMesh>().text = availLetter2[0];
            bottomL3Obj.GetComponent<TextMesh>().text = availLetter3[0];
            bottomL4Obj.GetComponent<TextMesh>().text = availLetter4[0];

        }

        // Update is called once per frame
        void Update()
        {
            spelledWord.GetComponent<TextMesh>().text = currentWord;
            if (Input.GetMouseButtonDown(0))
            {

                wordLen = currentWord.Length;

            }
            if (Input.GetMouseButtonUp(0))
            {


                if (wordLen == 4)
                {
                    if (currentWord == word2L)
                    {
                        currentWord = "";
                        Tile[0].SetActive(true);
                        Tile[1].SetActive(true);
                        Tile[2].SetActive(true);
                        Tile[3].SetActive(true);

                        bottomL1Obj.SetActive(false);
                        bottomL2Obj.SetActive(false);
                        bottomL3Obj.SetActive(false);
                        bottomL4Obj.SetActive(false);


                    }

                    else
                    {
                        Debug.Log("Wrong 4 letter word");
                    }

                }
            }
        }
    }

}

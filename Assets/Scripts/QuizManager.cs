using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;
    [SerializeField]
    private QuizDataScriptable questionData;

    [SerializeField]private GameObject Wall;

    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private float StartingTime=20f;
    private float Timer;

    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject[] Targets;

    [SerializeField]
    private WordData[] answerData;
    [SerializeField]
    private WordData[] optionData;

    bool QuizStatus = true;


    private int CurrentQuestionIndex;
    private int CurrentAnsIndex;
    private GameStatus gameStatus = GameStatus.Playing;
    [HideInInspector]public char[] charArray = new char[0];
    private bool correctAnswer = true;
    


    private void Awake()
    {
        progressBar.maxValue = StartingTime;
        Timer = StartingTime;
        if(instance == null) instance = this;
        else{Destroy(gameObject);}

        SetQuestion();                                      // Sets the Question word and shuffles the char characters of that word for buttons

        for (int i = 0; i < players.Length; i++)            // Sets the target GameObject of every character in game so that they move towards eachother
        {
            players[i].GetComponent<PlayerMovement>().target = enemies[i];
            players[i].GetComponent<PlayerMovement>().target1 = Targets[i];


            enemies[i].GetComponent<PlayerMovement>().target = players[i];
            enemies[i].GetComponent<PlayerMovement>().target1 = Targets[i];
        }
    }
    public void SetQuestion()
    {
        
        charArray = new char[questionData.questions[CurrentQuestionIndex].answer.Length];
        Debug.Log(questionData.questions[CurrentQuestionIndex].answer);
        Debug.Log(charArray);
        ResetQuestion();
        for (int i = 0; i < questionData.questions[CurrentQuestionIndex].answer.Length; i++)
        {
            optionData[i].gameObject.SetActive(true);
            answerData[i].gameObject.SetActive(true);
            charArray[i] = char.ToUpper(questionData.questions[CurrentQuestionIndex].answer[i]);
        }
        
        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            optionData[i].SetChar(charArray[i]);
        }
        CurrentAnsIndex = 0;
        gameStatus =GameStatus.Playing;
    }
    public void ResetQuestion()
    {
        CurrentAnsIndex = 0;
        for (int i = 0; i < answerData.Length; i++)
        {
            answerData[i].gameObject.SetActive(true);
            
            answerData[i].SetChar(' ');
        }
        for (int i = questionData.questions[CurrentQuestionIndex].answer.Length; i < answerData.Length; i++)
        {
            optionData[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < questionData.questions[CurrentQuestionIndex].answer.Length; i++)
        {
            optionData[i].gameObject.SetActive(true);
            answerData[i].gameObject.SetActive(true);
            answerData[i].charText.color = Color.green;
            charArray[i] = char.ToUpper(questionData.questions[CurrentQuestionIndex].answer[i]);
        }
        for(int i = questionData.questions[CurrentQuestionIndex].answer.Length; i < optionData.Length; i++)
        {
            optionData[i].gameObject.SetActive(false);
            answerData[i].gameObject.SetActive(false);
        }
    }
    public void SelectedOption(WordData wordData)
    {
        if(gameStatus == GameStatus.Next || CurrentAnsIndex >= answerData.Length) return;

        answerData[CurrentAnsIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        CurrentAnsIndex++;

        if(CurrentAnsIndex >= questionData.questions[CurrentQuestionIndex].answer.Length)
        {
            correctAnswer = true;

            for (int i = 0; i < questionData.questions[CurrentQuestionIndex].answer.Length; i++)
            {
                if(char.ToUpper(questionData.questions[CurrentQuestionIndex].answer[i]) != char.ToUpper(answerData[i].charValue))
                {
                    correctAnswer = false;
                    break;
                }
            }
            if(correctAnswer)
            {
                //Debug.Log("Correct!"); 
                CurrentQuestionIndex++;
                gameStatus = GameStatus.Next;
                if(CurrentQuestionIndex < questionData.questions.Count)
                {
                    Invoke("SetQuestion", 0.5f);
                    Timer += StartingTime*0.08f;
                }
                else
                {
                    Wall.GetComponent<Animator>().SetTrigger("Open");
                    Invoke("Win",1f); 
                    QuizStatus=false;
                }
                
            }
            else if(!correctAnswer)
            {
                //Debug.Log("Wrong!");
                for(int i = 0; i <= questionData.questions[CurrentQuestionIndex].answer.Length; i++)
                {
                   answerData[i].charText.color = Color.red;
                   Invoke("ResetQuestion",0.4f); 
                   Timer -= StartingTime*0.02f;
                }
            }
        }
    }


    void Win()
    {
        for (int i = 0; i <= Targets.Length-1; i++)
        {
            Targets[i].transform.position = new Vector3(Targets[i].transform.position.x,Targets[i].transform.position.y,45f);
        }
        for(int i = 0; i <= players.Length-1; i++)
        {
            players[i].GetComponent<PlayerMovement>().enabled = true;
        }
        for(int i = 0; i <= questionData.questions[CurrentQuestionIndex-1].answer.Length; i++)
        {
            answerData[i].gameObject.SetActive(false);
        }
    }
    void Lose()
    {
        for (int i = 0; i <= Targets.Length-1; i++)
        {
            Targets[i].transform.position = new Vector3(Targets[i].transform.position.x,Targets[i].transform.position.y,55f);
        }
        for(int y = 0; y <= players.Length-1; y++)
        {
            enemies[y].GetComponent<PlayerMovement>().enabled = true;
        }
        for(int i = 0; i <= questionData.questions[CurrentQuestionIndex].answer.Length; i++)
        {
            answerData[i].gameObject.SetActive(false);
            optionData[i].gameObject.SetActive(false);
        }

    }

    void Timerbar()
    {
        progressBar.value = Timer;
        if(Timer <= 0)
        {
            for(int i = 0; i <= questionData.questions[CurrentQuestionIndex].answer.Length; i++)
            {
                answerData[i].gameObject.SetActive(false);
                optionData[i].gameObject.SetActive(false);   
            }
            Wall.GetComponent<Animator>().SetTrigger("Open");
            Invoke("Lose",1f);
        }
        else{Timer -= Time.deltaTime;}
    }
    private void Update(){if(QuizStatus){Timerbar();}else{return;}}

}

public enum GameStatus 
{
    Playing,
    Next
}

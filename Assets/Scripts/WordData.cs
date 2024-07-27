using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordData : MonoBehaviour
{
    
    public Text charText;
    
    public char charValue;
    [SerializeField]
    private Button buttonObj;

    private void Awake() {
        buttonObj = GetComponent<Button>();
        if(buttonObj != null)
        {
            buttonObj.onClick.AddListener(() => CharSelected());
        }
    }
    
    public void SetChar(char value)
    {
        charText.text = value + "";
        charValue = value;
    }

    // Update is called once per frame
    private void CharSelected()
    {
        QuizManager.instance.SelectedOption(this);
    }
    
}

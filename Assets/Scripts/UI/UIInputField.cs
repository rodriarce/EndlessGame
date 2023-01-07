using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BotaLab;
public class UIInputField : MonoBehaviour
{
    public TMP_InputField inputField;
    private TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        inputField = GetComponent<TMP_InputField>();
        TMP_InputField.SelectionEvent selectEvent = new TMP_InputField.SelectionEvent();
        selectEvent.AddListener(OnSelectInput);
        TMP_InputField.SelectionEvent deselectEvent = new TMP_InputField.SelectionEvent();
        deselectEvent.AddListener(OnDeselectInput);
        inputField.onSelect = selectEvent;
    }

    public void OnSelectInput(string inputData)
    {
        //keyboard = TouchScreenKeyboard.Open("");
        Keyboard.instance.ShowKeyboard();
        Keyboard.instance.currentInput = inputField;
    }
    public void OnDeselectInput(string inputData)
    {
        Keyboard.instance.HideKeyboard();
        Keyboard.instance.currentInput = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

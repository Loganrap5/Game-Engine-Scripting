using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float prevInput;

    private bool clearPrevInput;
    

    //TODO: Leave this alone
    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {
        
        if (clearPrevInput == true)
        {
            text.text = string.Empty;
            clearPrevInput = false;
        }

        text.text += input;
        
    }

    //attach next 4 methods to each button
    public void SetEquationAsAdd()
    {
        
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    
    public void SetEquationAsSubtract()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }

    
    public void SetEquationAsMultiply()
    {
        prevInput = float.Parse (text.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }

    
    public void SetEquationAsDivide()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }

    //Add, Subtract, Multiply, Divide functions
    public void Add()
    {
        
        float currentInput = float.Parse(text.text);
        float result = prevInput + currentInput;
        text.text = result.ToString();
        
    }

    
    public void Subtract()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput - currentInput;
        text.text = result.ToString();


    }

    
    public void Multiply()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput * currentInput;
        text.text = result.ToString();
    }


    
    public void Divide()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput / currentInput;
        text.text = result.ToString();
    }

    //clear button
    public void Clear()
    {
        
        text.text = "0";
        clearPrevInput = true;
        prevInput = 0f;

        //TODO: Leave this alone
        equationType = EquationType.None;        
    }

    public void Calculate()
    {
        //TODO: Check if equationTypep is Add/Subtract/Multiply/Divide and call the correct function
        if (equationType == EquationType.ADD) Add();
        else if (equationType == EquationType.SUBTRACT) Subtract();
        else if (equationType == EquationType.MULTIPLY) Multiply();
        else if (equationType == EquationType.DIVIDE) Divide();
    }

    //TODO: Leave this alone
    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}

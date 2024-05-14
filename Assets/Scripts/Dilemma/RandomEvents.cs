using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "NewVariable", menuName = "Variables/Variable")]
public class Variable : ScriptableObject
{
    public string Name;
    public string Color;
    public string Size;
}

public class VariablePicker : MonoBehaviour
{
    public List<Variable> Variables;

    private void Start()
    {
        // Example usage
        string property = "Color";
        string value = "Blue";
        Variable randomVariable = PickRandomVariable(property, value);

        if (randomVariable != null)
        {
            Debug.Log($"Random variable with {property} = {value}: {randomVariable.Name}");
        }
        else
        {
            Debug.Log($"No variable found with {property} = {value}");
        }
    }

    public Variable PickRandomVariable(string property, string value)
    {
        // Filter variables based on the specified property and value
        var eligibleVariables = Variables.Where(v => v.Color == value).ToList();

        if (eligibleVariables.Count == 0)
        {
            // If no variables meet the criteria, return null
            return null;
        }

        // Pick a random variable from the filtered list
        int index = Random.Range(0, eligibleVariables.Count);
        return eligibleVariables[index];
    }
}

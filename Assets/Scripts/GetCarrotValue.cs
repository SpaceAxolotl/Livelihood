using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GetCarrotValue : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI totalCarrotsCollectedText;
    public CarrotPickup carrotPickup;

    // Update is called once per frame
    private void OnEnable()
    {
        CarrotPickup.OnCarrotCollected += UpdateCarrotCount;
    }

    private void OnDisable()
    {
        CarrotPickup.OnCarrotCollected -= UpdateCarrotCount;
    }

    private void UpdateCarrotCount()
    {
        totalCarrotsCollectedText.text = CarrotPickup.GetCollectedCarrotAmount().ToString();
    }
}

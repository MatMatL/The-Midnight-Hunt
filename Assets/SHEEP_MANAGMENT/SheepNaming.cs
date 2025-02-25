using TMPro;
using UnityEngine;

public class SheepNaming : MonoBehaviour
{
    private string[] SheepName = {
        "Rayou",
        "Rynou",
        "Rayanou",
        "Rynounet",
        "Rynouille",
        "Rynito",
        "Rynounours",
        "Rian",
        "Ryana",
        "Ryan"
    };

    void Start()
    {
        int randomName = Random.Range(0, 9);
        GetComponent<TMP_Text>().text = SheepName[randomName];
    }
}

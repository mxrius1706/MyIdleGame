using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject damageTextprefab;
    public GameObject healTextPrefab;

    public Canvas gameCanvas;

    public void tookDamage(int dmgReceived, GameObject character)
    {
        //Create text at character hit
        //Takes Position fromm the Camera and converts it to a screen position
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        //Creates an instance of the prefab at the spawn position
        TMP_Text tmpText = Instantiate(damageTextprefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text =  "-" + dmgReceived.ToString();
    }

    public void healed(int healReceived, GameObject character)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = "+" + healReceived.ToString();

    }

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();


    }

    private void OnEnable()
    {  //Events will run as soon as they methods are awoked
        CharacterEvents.characterDamaged += tookDamage;
        CharacterEvents.characterHealed += healed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= tookDamage;
        CharacterEvents.characterHealed -= healed;
    }
}

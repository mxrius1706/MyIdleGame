using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    // Start is called before the first frame update

    //Pixel pro Sekunde
    public Vector3 moveSpeed = new Vector3 (0,75,0);
    public float timeToFade = 1f;	

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
   
    private Color startColor;

    private float timeElapsed = 0f;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;

        if(timeElapsed < timeToFade)
        {
            float alpha = Mathf.Lerp(startColor.a, 0f, timeElapsed / timeToFade);

            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
       
        else
        {
            Destroy(gameObject);
        }
    }
}

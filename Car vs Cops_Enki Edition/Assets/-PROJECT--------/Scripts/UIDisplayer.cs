using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayer : MonoBehaviour
{
    public Health playerHealth;

    public Text scoreText;
    public Text coinsText;

    public Image[] hearths;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GlobalManager.instance.currentScore.ToString();
        coinsText.text = GlobalManager.instance.coins.ToString();

        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i].enabled = i < playerHealth.currentHitPoints;
        }
    }
}

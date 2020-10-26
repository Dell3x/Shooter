using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private static int score;

    private Text label;

    public static int Score { get => score; set => score = value; }



    // Start is called before the first frame update
    void Start()
    {

        label = gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        label.text = score.ToString();


    }
}

using System.IO.Ports;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Arduino : MonoBehaviour
{
    //port
    SerialPort arduinoPort = new SerialPort("COM3", 9600);

    //pancake sprites
    public GameObject startPancake;
    public GameObject underPancake;
    public GameObject perfectPancake;
    public GameObject overPancake;

    //passed from arduino
    private bool flipped;

    //first and second flip for each pancake
    private bool isFlipped = false;
    private PancakeState firstState = PancakeState.Start;
    private PancakeState secondState = PancakeState.Start;

    //previous state
    private bool lastFlipState = false;

    //score
    private int goodReviews = 0;
    private int badReviews = 0;
    public TextMeshProUGUI goodReviewsText;
    public TextMeshProUGUI badReviewsText;

    //timer
    public float timer = 10f;
    public TextMeshProUGUI timerText;

    //game over
    public TextMeshProUGUI firedText;
    
    //sets everything up
    void Start()
    {
        SpawnNewPancake();

        goodReviewsText.text = "Good Reviews: 0";
        badReviewsText.text = "Bad Reviews: 0";
        timerText.text = "Timer: 10";
        firedText.text = "";

        arduinoPort.Open();

        Time.timeScale = 1;
    }

    void Update()
    {
        //runs while you have less than 3 bad reviews
        if (badReviews < 3)
        {
            //gets data from arduino
            try
            {
                ReadData();
            }
            catch { }

            //checks if a flip occured and if it didn't happen right before as well
            if (flipped && !lastFlipState)
            {
                Flip();
            }

            lastFlipState = flipped;

            //timer for each side of the pancake
            timer -= Time.deltaTime;
            timerText.text = "Timer: " + timer;

            //checks if timer is less than 0
            if (timer <= 0f)
            {
                //checks if first side is overcooked
                if (!isFlipped)
                {
                    startPancake.SetActive(false);
                    overPancake.SetActive(true);

                    firstState = PancakeState.Over;

                    isFlipped = true;
                    timer = 10f;
                }
                else
                {
                    startPancake.SetActive(false);
                    underPancake.SetActive(false);
                    perfectPancake.SetActive(false);
                    overPancake.SetActive(true);

                    secondState = PancakeState.Over;

                    CalcScore();
                    SpawnNewPancake();
                }
            }
        }
        else
        {
            Time.timeScale = 0;
            firedText.text = "You're Fired";
        }

    }

    private void ReadData()
    {
        //makes sure the port is open and has data to read
        if (arduinoPort.IsOpen && arduinoPort.BytesToRead > 0)
        {
            //seperates the data
            string message = arduinoPort.ReadLine().Trim();
            string[] values = message.Split(',');

            //checks if it's all the data
            if (values.Length >= 2)
            {
                //gets the flipped status
                flipped = values[0] == "1";
                int xValue = int.Parse(values[1]);

                //print info
                Debug.Log("Flipped: " + flipped + ", X: " + xValue);
            }
        }
    }

    private void Flip()
    {
        //checks if its the first or second flip
        if(!isFlipped)
        {
            isFlipped = true;
            //checks when it was flipped
            if(timer >= 7f)
            {
                startPancake.SetActive(false);
                underPancake.SetActive(true);
                firstState = PancakeState.Under;
            }
            else if (timer >= 3f)
            {
                startPancake.SetActive(false);
                perfectPancake.SetActive(true);
                firstState = PancakeState.Perfect;
            }
            else
            {
                startPancake.SetActive(false);
                overPancake.SetActive(true);
                firstState = PancakeState.Over;

            }
        }
        else
        {
            isFlipped = false;

            //checks when it was flipped
            if (timer >= 7f)
            {
                startPancake.SetActive(false);
                underPancake.SetActive(true);
                perfectPancake.SetActive(false);
                overPancake.SetActive(false);

                secondState = PancakeState.Under;
            }
            else if (timer >= 3f)
            {
                startPancake.SetActive(false);
                underPancake.SetActive(false);
                perfectPancake.SetActive(true);
                overPancake.SetActive(false);

                secondState = PancakeState.Perfect;
            }
            else
            {
                startPancake.SetActive(false);
                underPancake.SetActive(false);
                perfectPancake.SetActive(false);
                overPancake.SetActive(true);

                secondState = PancakeState.Over;
            }

            CalcScore();
            SpawnNewPancake();
        }
    }


    private void UpdateText()
    {
        //updates score text
        goodReviewsText.text = "Good Reviews: " + goodReviews;
        badReviewsText.text = "Bad Reviews: " + badReviews;
    }
    private void CalcScore()
    {   
        //calculates whether the player gets a good or bad review
        if ((int)firstState + (int)(secondState) >= 1)
        {
            goodReviews++;
        }
        else
        {
            badReviews++;
        }
        UpdateText();
    }

    private void SpawnNewPancake()
    {
        //spawns a new pancake once one is finished
        isFlipped = false;
        firstState = PancakeState.Start;
        secondState = PancakeState.Start;

        startPancake.SetActive(true);
        underPancake.SetActive(false);
        perfectPancake.SetActive(false);
        overPancake.SetActive(false);

        timer = 10f;
    }

    private void OnApplicationQuit()
    {
        //closes the connection if the game stops
        if (arduinoPort.IsOpen)
        {
            arduinoPort.Close();
        }
    }
}

//enum for the state of each side of the pancake
public enum PancakeState
{
    Start = -1,
    Under = 0,
    Perfect = 1,
    Over = 0
}



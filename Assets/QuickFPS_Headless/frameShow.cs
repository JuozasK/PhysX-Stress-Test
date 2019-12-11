using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class frameShow : MonoBehaviour
{
    List<float> frames = new List<float>();
    UnityEngine.UI.Text text;
    float average = 0;
    float low = 0;
    int frameAmount = 20;
    int frameCounter = 0;
    
    // Use this for initialization
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frames.Count < frameAmount)
        {
            frames.Add(1 / Time.deltaTime);
        }
        else
        {
            frames[frameCounter] = 1 / Time.deltaTime;
        }

        average = 0;
        low = frames[0];
        for (int i = 0; i < frames.Count; i++)
        {
            if (frames[i] < low)
                low = frames[i];
            average += frames[i];
        }
        average /= frames.Count;
        text.text = "SCENE: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " \n"; 
        text.text += "AVG: " + average.ToString("F2") + "FPS \n";
        text.text += "LOW: " + low.ToString("F2") + "FPS \n";
        for (int i = 0; i < frames.Count; i++)
        {
            text.text += frames[i].ToString("F2") + " | ";
        }
        frameCounter++;
        if (frameCounter > frameAmount - 1)
            frameCounter = 0;
    }
}
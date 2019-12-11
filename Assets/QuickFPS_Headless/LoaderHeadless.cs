using System.Collections;
using UnityEngine;

public class LoaderHeadless : MonoBehaviour
{
    private float totalTime = 0;
    int counter = 0;
    float tempSum = 0;
    private bool done = false;
    private string finalResult = "ZZRES>> ";

    [SerializeField]
    [Tooltip("How much time (in seconds) to wait before recording frames")]
    float preTestTime = 5;
    [SerializeField]
    [Tooltip("How much time (in seconds) the test should take")]
    float testDuration = 15;

    public float ballAmount = 0;

    Coroutine test;

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 1000;                 // For wherever it's relevant, set a very large target to strive for
        Debug.Log("[AutoRunner] Graphics API = " + SystemInfo.graphicsDeviceType);
        Time.maximumDeltaTime = 0.5f;                       // Setting a higher value than default of 0.1 to increase fidelity of results
        DontDestroyOnLoad(gameObject);
        test = StartCoroutine(RunTest());
        Debug.Log("[AutoRunner] APP_STARTED");
    }

    void Update()
    {

        totalTime += Time.deltaTime;
    }

    public void FinishTest(float results)
    {
        finalResult += string.Format("SCENE: {0} | AVERAGE: {1} | BALLS: {2} ---- ", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, results, ballAmount);
        if (NextScene())
        {
            Debug.Log("[AutoRunner] Starting next scene");
        } 
        else
        {
            Debug.Log(finalResult);
            Debug.Log("[AutoRunner] Finish test");
            Application.Quit();
            StopAllCoroutines(); //In the case when running on editor
        }
        //Debug.LogFormat("ZZRES>> SCENE: {0} | RESULT: {1} ---- " , UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, results);

    }

    bool NextScene()
    {
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        int currScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currScene < sceneCount - 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currScene + 1,UnityEngine.SceneManagement.LoadSceneMode.Single);
            ResetTest();
            test = StartCoroutine(RunTest());
            return true;
        }
        return false;
    }

    void ResetTest()
    {
        tempSum = 0;
        counter = 0;
        totalTime = 0;
        done = false;
        ballAmount = 0;
    }

    public IEnumerator RunTest()
    {
        yield return new WaitForSeconds(preTestTime);
        while (true)
        {
            if (totalTime < preTestTime + testDuration)
            {
                tempSum += Time.deltaTime;
                counter++;
            }
            else
            {
                done = true;
            }

            if (done)
            {
                float avg = (tempSum / counter) * 1000;
                FinishTest(avg);
                StopCoroutine(test);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

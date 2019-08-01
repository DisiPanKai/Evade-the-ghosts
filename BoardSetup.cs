using UnityEngine;


public class BoardSetup : MonoBehaviour
{
    public GameObject optionsButton;
    public GameObject presentButton;
    public GameObject skinButton;
    public GameObject arrows;
    public GameObject player;
    public GameObject music;


    // Forces a different code path in the BinaryFormatter that doesn't rely on run-time code generation (which would break on iOS).
    //Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

    void Start()
    {
        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        LevelSetup();
        gameController.FirstGameSet();
        //SaveLoad.Load();
    }


    public void LevelSetup()//установка игрового уровня
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
            Instantiate(player);
        else
        {
            playerObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            PlayerController playerController = playerObj.GetComponent<PlayerController>();
            playerController.gameOver = false;
            playerController.playerSpeed = 200f;
            playerController.ResetPlayerMovement();
            
        }
        GameObject carpet1Obj = GameObject.Find("Carpet1");
        carpet1Obj.GetComponent<Animator>().Play("WelcomeOn", -1, 0f);
        carpet1Obj.AddComponent<WelcomeScript>();
        GameObject.Find("Carpet2").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Carpet3").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Carpet4").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Carpet5").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Carpet6").GetComponent<Animator>().Play("DeadState", -1, 0f);
        GameObject.Find("Carpet7").GetComponent<Animator>().Play("DeadState", -1, 0f);
        if (GameObject.Find("Arrows(Clone)") == null)
            Instantiate(arrows);
        if (GameObject.Find("SkinButton(Clone)") == null)
            Instantiate(skinButton);
        if (GameObject.Find("BonusButton(Clone)") == null)
            Instantiate(presentButton);
        if (GameObject.Find("OptionsButton(Clone)") == null)
            Instantiate(optionsButton);
        if (GameObject.FindGameObjectWithTag("Music") == null)
            Instantiate(music);
    }
}

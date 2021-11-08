using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //public Text Tutorialtext;
    public Image WinCat;
    public Image WinButter;
    public Image WinNone;
    public Image Tutorial;
    public Button ButtonButter;
    public Button ButtonCat;
    //public Button ButtonPlay;
    private Touch theTouch;
    private float timeTouchEnded;
    private float horizontalmove;
    private float speed = 2;
    private float gravity = 0.04f;
    //private float Anglespeed = 45;
    private float degreesPerSecond = 11000;
    public bool gameOver = false;
    //private float jumpForce = 700;
    //private float gravityModifier = 1.5f;
    private bool isOnGround = false;
    //private Animator playerAnim;
    //private Rigidbody2D playerRb;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    private bool isFalling = false;
    float cat_pos = -180f;
    float butter_pos = 180;
    public bool Fplayerbutton, Splayerbutton;

    // Start is called before the first frame update
    void Start()
    {
        //Text tutorialtext = Tutorialtext.GetComponent<Text>();
        //Tutorialtext.enabled = true;
        Image catwin = WinCat.GetComponent<Image>();
        catwin.enabled = false;
        Image butterwin = WinButter.GetComponent<Image>();
        butterwin.enabled = false;
        Image winnone = WinNone.GetComponent<Image>();
        winnone.enabled = false;
        Image tut = Tutorial.GetComponent<Image>();
        tut.enabled = true;
        Button btnbut = ButtonButter.GetComponent<Button>();
        btnbut.onClick.AddListener(ButterMovement);
        Button btncat = ButtonCat.GetComponent<Button>();
        btncat.onClick.AddListener(CatMovement);
        //Button btnplay = ButtonPlay.GetComponent<Button>();
        //btncat.onClick.AddListener(Update);
        //подключение плагинов
        StartCoroutine(Test());
        //playerAnim = GetComponent<Animator>();
        //playerRb = GetComponent<Rigidbody2D>();
        //Physics.gravity *= gravityModifier;
        //playerAudio = GetComponent<AudioSource>();
        //playerRb.constraints = RigidbodyConstraints2D.FreezePositionY;// | RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonUp(0))
        {
            Fplayerbutton = false;
            Splayerbutton = false;
        }*/
        float actualPlayerPos = gameObject.transform.eulerAngles.z;
        //управление БУТЕРА второго игрока игрока, хождение по x и повороты
        //CatMovement();
        //ButterMovement();
        //отправление вниз
        if(isFalling && !gameOver)
            transform.Translate(new Vector2(0, -gravity), Space.World);

        /*if (gameObject.transform.rotation.z > 8)
            gameObject.transform.position = new Vector2(1.8f, gameObject.transform.position.y);
        else if (gameObject.transform.position.x < -1.8f)
            gameObject.transform.position = new Vector2(-1.8f, gameObject.transform.position.y);*/

        //чтобы не уходил за край карты
        if (transform.position.x > 1.8f) 
            transform.position = new Vector2(1.8f, transform.position.y);
        else if (transform.position.x < -1.8f)
            transform.position = new Vector2(-1.8f, transform.position.y);

        /*float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        gameObject.velocity = new Vector2(moveBy, playerRb.velocity.y);*/
        //horizontalmove = Input.GetAxisRaw("Horizontal") * Anglespeed;
    }
    //ожидание 3 секунды, после этого игрок начинает падение
    IEnumerator Test()
    {
        yield return new WaitForSeconds(15);
        isFalling = true;
        //gameObject.constraints = RigidbodyConstraints2D.None;
        Debug.Log("It Work");
    }

    /*public void CheckIsTrueCat()
    {
        Fplayerbutton = true;
    }*/

    /*public void CheckIsTrueButter()
    {
        Splayerbutton = true;
    }*/

    public void CatMovement()
    {
        //управление КОТА первый игрок, хождение по x и повороты
        if (!gameOver)//Input.GetKeyDown(KeyCode.LeftControl) &&  && actualPlayerPos <= 360)// && gameObject.transform.position.x < 3 && gameObject.transform.position.x > -3)//&& isOnGround == false && !gameOver)
        {
            //Tutorialtext.enabled = false;
            Tutorial.enabled = false;
            //перемещение персонажа по x
            transform.Translate(new Vector2(-speed, 0), Space.World);
            //transform.Translate(new Vector2(-1, 0));
            var angles = transform.eulerAngles;
            //angles.z -= 90;//degreesPerSecond * Time.deltaTime;// * Anglespeed;
            if (angles.z > 180)
                angles.z -= 360;
            //if (actualPlayerPos >= butter_pos)
            angles.z += 45;
            transform.eulerAngles = angles;
            //transform.Rotate(new Vector3(0, 0, -degreesPerSecond) * Time.deltaTime * Anglespeed);
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    public void ButterMovement()
    {
        if (!gameOver)// Input.GetKeyDown(KeyCode.RightControl) && actualPlayerPos >= -360) //&& gameObject.transform.rotation.z < -180)// && gameObject.transform.position.x < 3 && gameObject.transform.position.x > -3)//&& isOnGround == false && !gameOver)
        {
            //Tutorialtext.enabled = false;
            Tutorial.enabled = false;
            transform.Translate(new Vector2(speed, 0), Space.World);
            var angles = transform.eulerAngles;
            //degreesPerSecond * Time.deltaTime;// * Anglespeed;
            if (angles.z > 180)
                angles.z -= 360;
            //if (actualPlayerPos >= butter_pos)
            angles.z -= 45;
            transform.eulerAngles = angles;
            //transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime * Anglespeed);
            dirtParticle.Stop();
            if (jumpSound == null)
                Debug.Log("ll");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    //детектирование колизий с землей и препятствиями в воздухе
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            explosionParticle.gameObject.SetActive(true);
            playerAudio.PlayOneShot(crashSound, 1.0f);
            WinNone.enabled = true;
            //WinNone.enabled = true;
            Debug.Log("winnone");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            //определение победителя
            var rotationZ = gameObject.transform.eulerAngles.z;
            Debug.Log(rotationZ);
            bool CatPlayerWinCondition = rotationZ >= -5 && rotationZ <= 5;
            bool ButterPlayerWinCondition = rotationZ <= 185 && rotationZ >= 175;
            //условия победы
            if (CatPlayerWinCondition)//CatPlayerWinCondition)
            {
                Debug.Log("cat Win");
                //WinCat.enabled = true;
                WinCat.enabled = true;
            }
            else if (ButterPlayerWinCondition)//ButterPlayerWinCondition)
            {
                //WinButter.enabled = true;
                Debug.Log("buter win");
                WinButter.enabled = true;
            }
            else
            {
                WinNone.enabled = true;
                //WinNone.enabled = true;
                Debug.Log("winnone");
            }
            isFalling = false;
            gameOver = true;
            //остановка игры
            //Time.timeScale = 0;
            Debug.Log("Ground");
            isOnGround = true;
            explosionParticle.gameObject.SetActive(true);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

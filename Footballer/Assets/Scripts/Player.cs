using UnityEngine;
using UnityEngine.Audio;


public class Player : MonoBehaviour {


    Rigidbody2D myRigidbody;
    Animator anim;
    Transform playerTransform;
    GameObject objectDestination;
    GameObject objectOrgin;
    AudioSource audioJump;

    public RuntimeAnimatorController[] selectedAnim;


    public int IndexOfPlayerSkin {get; set;}

    public static Player Instace;

    bool jump;

    float speed = 3f;
    bool toDestination = true;

    public float minSpeed = 3f,
            maxSpeed = 3.6f;

    float turnTime = 0;
    float delayTime = 0.2f;
    bool turned = false;
    bool prevSpeed = false;
    float speedDealy = 2f;


    private void Awake()
    {
        Instace = this;
        objectDestination = GameObject.FindGameObjectWithTag("Destination");
        objectOrgin = GameObject.FindGameObjectWithTag("Orgin");
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        audioJump = GetComponent<AudioSource>();

        //Set Skin
        IndexOfPlayerSkin = SecurePlayerPrefs.GetInt("SelectedPlayer");
        anim.runtimeAnimatorController = selectedAnim[IndexOfPlayerSkin];

        speed = minSpeed;
       
        turnTime = 0;
    }

    void Update()
    {
        Move();

        DelayTurn();
        
        #region Computer Input
        if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeDestination();
            }


            if ((Input.GetKeyDown(KeyCode.Space)) && (jump))
            {

                if (UIManager.uiManagerInstance.paused == false)
                {
                    Jump();

                }
            }
            else
            {
                anim.SetBool("Jump", false);

            }
        #endregion

    }

    #region Collision
    private void OnCollisionEnter2D(Collision2D collisionPlayer)
    {
        if (collisionPlayer.gameObject.tag == "Grass")
            jump = true;

    }
    #endregion

    #region Move
    public void Move()
    {

        if (myRigidbody.transform.position.x >= objectDestination.transform.position.x)
        {
            toDestination = false;
        }
        else if (myRigidbody.transform.position.x <= objectOrgin.transform.position.x)
        {
            toDestination = true;
        }


        if (toDestination)
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);

            playerTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);

            if (LevelSelection.index == 3)
            {
                Flashlight.Instance.RotationRight();
                HeadLight.Instance.RotationRight();
            }

        }
        else if (!toDestination)
        {
            myRigidbody.velocity = new Vector2((speed * -1), myRigidbody.velocity.y);

            playerTransform.transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);

            if (LevelSelection.index == 3)
            {
                Flashlight.Instance.RotationLeft();
                HeadLight.Instance.RotationLeft();
            }
        }


    }
    #endregion

    #region Change Destination
    public void ChangeDestination()
    {
        if (!UIManager.uiManagerInstance.paused)
        {
            if (toDestination)
            {
                toDestination = false;
                turned = true;

            }
            else
            {
                toDestination = true;
                turned = true;
            }
        } 
    }
    #endregion

    #region Jump
    public void Jump()
    {
        if (!UIManager.uiManagerInstance.paused)
        {
            if (jump)
            {
                anim.SetBool("Jump", true);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 12);

                if (!Mute.muteSounds)
                {
                    audioJump.Play();
                }

                jump = false;
            }
        }

    }
    #endregion


    public void Speed()
    {
        if (speed <= maxSpeed)
        {
            speed += 0.1f;

            //Debug.Log(speed);
        }
    }
    public void ResetSpeed()
    {
        speed = minSpeed;
    }

    void DelayTurn()
    {
        turnTime += Time.deltaTime;

        if (turned)
        {
            speed -= speedDealy;
            turned = false;
            prevSpeed = true;

        }

        if (prevSpeed == true)
        {
            if (turnTime >= delayTime)
            {
                speed += speedDealy;

            }
            prevSpeed = false;
        }
    }
}


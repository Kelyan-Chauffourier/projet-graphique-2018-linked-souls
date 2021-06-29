using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private float timerOfOneSecond;

    [SyncVar]
    public float lifePoints;
    [SyncVar]
    public float manaPoints;
    [SyncVar]
    public float expPoints;

    public int lifePointsMax, expPointsMax, manaPointsMax;

    [SyncVar]
    public int levelOfThePlayer;

    private float speed;
    private Rigidbody2D rb2d;

    public GameObject fireball;
    public GameObject groundSmash;
    public GameObject dash;

	private Vector3 tailleJoueur;
	public GameObject playerOrientation;
    public GameObject swordRotatePoint;
    private Quaternion swordRotatePointStart;
    private bool isAttackingWithSword;

	bool gauche, droite, haut, bas;
    bool isLeft, isRight;

    private Animator animator;
    private GameObject inventory;

    void Awake()
    {
        Application.targetFrameRate = 60;

        expPoints = 500;
        speed = 10f;
        lifePoints = 50;
        lifePointsMax = 100;
        manaPoints = 50;
        manaPointsMax = 100;

        isAttackingWithSword = false;

        swordRotatePointStart = swordRotatePoint.transform.localRotation;
		swordRotatePoint.SetActive (false);

		tailleJoueur = transform.localScale;
		animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        inventory = GameObject.Find("Inventory");
        Spawn();
    }

    public override void OnStartAuthority()
    {
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        //GameObject floatingHealthBar = GameObject.Find("CanvasFloatingHealthBar");
        //if (floatingHealthBar)
        //{
        //    Destroy(floatingHealthBar.gameObject);
        //    Debug.Log(name + "has been destroyed.");
        //}
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = (movement * speed);

        // send to the server my new position.
        CmdMove(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Trigger")
        {
            collision.gameObject.GetComponent<Trigger>().OnEnter();
            Debug.Log("Collision avec un trigger");
        }
        else */
		if (collision.tag == "Ennemy")
        {
            Debug.Log("Collision avec ennemi");
            if(isServer)
                lifePoints -= 10;
        }
        if (collision.tag == "Item")
        {
            Debug.Log("Mastersword added to inventory");
            inventory.GetComponent<Inventory>().AddItem(0);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (collision.tag == "Trigger")
        {
            collision.gameObject.GetComponent<Trigger>().OnExit();
            Debug.Log("Sortie d'une collision avec un trigger");
        }*/
    }

    [Command]
    void CmdMove(Vector2 position)
    {
        //transform.position = position;
        transform.position = Vector2.Lerp(transform.position, position, (10f / Vector2.Distance(transform.position, position)) * Time.fixedDeltaTime);
        SetDirtyBit(1u);
    }

    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
        writer.Write(transform.position);
        return true;
    }

    public override void OnDeserialize(NetworkReader reader, bool initialState)
    {
        if (isLocalPlayer)
        {
            return;
        }

        transform.position = reader.ReadVector2();
    }

    public override void OnStartLocalPlayer()
    {
        //Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
        Camera[] cameras = Camera.allCameras;
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<CameraFollow>().setTarget(gameObject.transform);
        }
    }

    void Spawn() {
        if (hasAuthority)
        {
            GameObject spawn2 = GameObject.Find("SpawnPoint2");
            gameObject.transform.position = spawn2.transform.position;
        }
        else
        {
            GameObject spawn = GameObject.Find("SpawnPoint");
            gameObject.transform.position = spawn.transform.position;
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && manaPoints >= 10)
        {
            CmdFireballSpell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			GroundSpell();
		}

		if (Input.GetMouseButtonDown(0))
        {
            if (swordRotatePoint.activeSelf)
            {
                return;
            }
            swordRotatePoint.transform.localRotation = swordRotatePointStart;
            swordRotatePoint.SetActive(true);
            isAttackingWithSword = true;

            if (hasAuthority)
            {
                animator.SetTrigger("sword");

                if (playerOrientation.transform.localPosition.y < 0f)
                    animator.SetFloat("direction", 0f);

                else if (playerOrientation.transform.localPosition.y > 0f)
                    animator.SetFloat("direction", 0.33f);

                else if (playerOrientation.transform.localPosition.x < 0f)
                    animator.SetFloat("direction", 0.66f);

                else if (playerOrientation.transform.localPosition.x > 0f)
                    animator.SetFloat("direction", 1f);
            }
        }

        if (hasAuthority)
        {
            haut = Input.GetKey(KeyCode.Z);
            gauche = Input.GetKey(KeyCode.Q);
            bas = Input.GetKey(KeyCode.S);
            droite = Input.GetKey(KeyCode.D);

            if (!haut && !bas && !gauche && !droite)
            {
                animator.SetTrigger("idle");

                if (playerOrientation.transform.localPosition.y < 0)
                {
                    animator.SetFloat("direction", 0f);
                }

                else if (playerOrientation.transform.localPosition.y > 0)
                {
                    animator.SetFloat("direction", 0.66f);
                }

                else
                {
                    animator.SetFloat("direction", 0.33f);
                }
            }

            else if (haut || bas || gauche || droite)
            {
                animator.SetTrigger("walking");

                if (haut)
                {
                    playerOrientation.transform.localPosition = new Vector3(0, 0.001f, 0);
                    playerOrientation.transform.localRotation = Quaternion.Euler(0, 0, 180);
                    animator.SetFloat("direction", 0.66f);
                    isLeft = false;
                    isRight = false;
                }

                else if (bas)
                {
                    playerOrientation.transform.localPosition = new Vector3(0, -0.001f, 0);
                    playerOrientation.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    animator.SetFloat("direction", 0f);
                    isLeft = false;
                    isRight = false;
                }

                else if (gauche && !isLeft)
                {
                    playerOrientation.transform.localPosition = new Vector3(-0.001f, 0, 0);
                    playerOrientation.transform.localRotation = Quaternion.Euler(0, 0, 270);
                    animator.SetFloat("direction", 0.33f);
                    tailleJoueur.x = 5;
                    this.transform.localScale = tailleJoueur;
                    isLeft = true;
                    isRight = false;
                }

                else if (droite && !isRight)
                {
                    playerOrientation.transform.localPosition = new Vector3(-0.001f, 0, 0);
                    playerOrientation.transform.localRotation = Quaternion.Euler(0, 0, 270);
                    animator.SetFloat("direction", 1f);
                    tailleJoueur.x = -5;
                    this.transform.localScale = tailleJoueur;
                    isRight = true;
                    isLeft = false;
                }
            }
        }
       
        if (isServer)
        {
            timerOfOneSecond += Time.deltaTime;

            if (timerOfOneSecond >= 1f)
            {
                GetLifePoint();
                GetManaPoint();

                timerOfOneSecond -= 1f;
            }
        }

        if (isAttackingWithSword == true)
        {
            swordRotatePoint.transform.Rotate(Vector3.forward * 360 * Time.deltaTime, Space.Self);
            if (swordRotatePoint.transform.localRotation.eulerAngles.z < 270f || swordRotatePoint.transform.localRotation.eulerAngles.z > 360f)
            {
                isAttackingWithSword = false;
                swordRotatePoint.SetActive(false);
            }
        }
    }

    [Command]
    private void CmdFireballSpell(Vector2 destination)
    {
        if(manaPoints >= 10)
        {
            manaPoints -= 10;
            RpcFireBall(destination);
        }
    }

    [ClientRpc]
    void RpcFireBall(Vector2 dest)
    {
        GameObject fire = Instantiate(fireball, this.transform.position, Quaternion.Euler(new Vector3(dest.x, dest.y, 0) - this.transform.position));
        fire.GetComponent<FireballPhysics>().destination = dest;
        NetworkServer.Spawn(fire);

        if(hasAuthority)
        {
            animator.SetTrigger("fire");
            if (playerOrientation.transform.localPosition.y < 0f)
                animator.SetFloat("direction", 0f);

            else if (playerOrientation.transform.localPosition.y > 0f)
                animator.SetFloat("direction", 0.66f);

            else if (playerOrientation.transform.localPosition.x < 0f)
                animator.SetFloat("direction", 0.33f);

            else if (playerOrientation.transform.localPosition.x < 0f)
                animator.SetFloat("direction", 1f);
        }
	}

	private void GroundSpell()
	{
        if (hasAuthority)
            animator.SetTrigger("groundSpell");
	}

	private void GetManaPoint()
	{
		if(manaPoints < manaPointsMax)
			manaPoints += 1;
	}

	private void GetLifePoint()
	{
		if(lifePoints < lifePointsMax)
			lifePoints += 1;
	}


}

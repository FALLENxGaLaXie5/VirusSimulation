// Player Mobility
// Author -Zane Gittins
// Top down movement script for Unity3D. 
// Needs to have a lot of code cut out so that it can be used for the current project. 
// Can cut out all the charge, and slash functionality. 
using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour
{

    public GameObject katana;
    public GameObject wallCheck;
    public GameObject naginata;
    public GameObject upperTorso;

    public float speed;

    public float zeroSpeed = 0;
    public float actualSpeed;
    private float speedHorRight;
    private float speedHorLeft;
    private float speedVertUp;
    private float speedVertDown;

    public bool againstWallVertUp = false;
    public bool againstWallVertDown = false;
    public bool againstWallHorRight= false;
    public bool againstWallHorLeft = false;

    public bool katanaEnabled = false;
    public bool naginataEnabled = false;

    bool wait = false;
    bool wait2 = false;
    bool waitToSlash = true;


    bool chargeRight;
    bool chargeLeft;
    bool chargeUp;
    bool chargeDown;
    bool correctRotation = false;
    bool waitToCharge = true;

    private Animator myAnim;

    bool waitAnim = false;
    bool animRunning = false;

	private int animFlip = 1;

    // Use this for initialization
    void Start ()
    {
        wallCheck = GameObject.FindGameObjectWithTag("wallCheck");
        actualSpeed = speed;
        katana.SetActive(false);

        speedHorRight = speed;
        speedHorLeft = speed;
        speedVertUp = speed;
        speedVertDown = speed;

        myAnim = upperTorso.GetComponent<Animator>();
        myAnim.SetInteger("anims", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Sets appropriate speed to zero if against a wall. 
        checkRay();
        if (againstWallVertUp)
        {
            speedVertUp = zeroSpeed; 
        }
        else
        {
            speedVertUp = actualSpeed;
        }

        if (againstWallVertDown)
        {
            speedVertDown = zeroSpeed;
        }
        else
        {
            speedVertDown = actualSpeed;
        }

        if (againstWallHorLeft)
        {
            speedHorLeft = zeroSpeed; 
        }
        else
        {
            speedHorLeft = actualSpeed;
        }

        if (againstWallHorRight)
        {
            speedHorRight = zeroSpeed;
        }
        else
        {
          speedHorRight = actualSpeed;
        }

       



        if (wait == false)
        {
            katana.SetActive(false);
            if (wait2 != true)
                naginata.SetActive(false);
        }

        if(wait2 == false)
        {
            naginata.SetActive(false);
            rotatePlayer rot = upperTorso.GetComponent<rotatePlayer>();
            rot.shouldRotate = true;
            correctRotation = false;
        }

        if(wait2)
        {
            Charge();
            if (correctRotation) naginata.SetActive(true);
        }

        if(wait2 == false && correctRotation == false)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {

                var moveHorRight = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                transform.position += moveHorRight * speedHorRight * Time.deltaTime;
                chargeRight = true;
                chargeLeft = false;
                chargeDown = false;
                chargeUp = false;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {

                var moveHorLeft = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                transform.position += moveHorLeft * speedHorLeft * Time.deltaTime;
                chargeRight = false;
                chargeLeft = true;
                chargeDown = false;
                chargeUp = false;
            }

            if (Input.GetAxis("Vertical") > 0)
            {

                var moveVertUp = new Vector3(0, Input.GetAxis("Vertical"), 0);
                transform.position += moveVertUp * speedVertUp * Time.deltaTime;
                chargeRight = false;
                chargeLeft = false;
                chargeDown = false;
                chargeUp = true;
            }

            if (Input.GetAxis("Vertical") < 0)
            {

                var moveVertDown = new Vector3(0, Input.GetAxis("Vertical"), 0);
                transform.position += moveVertDown * speedVertDown * Time.deltaTime;
                chargeRight = false;
                chargeLeft = false;
                chargeDown = true;
                chargeUp = false;
            }

        }
       

        if (Input.GetButtonDown("Fire1") && katanaEnabled == true && waitToSlash && waitAnim == false)
        {
            myAnim.SetInteger("anims", 1);
            katana.SetActive(true);
            

            StartCoroutine(waitToRespond());
            waitToSlash = false;
            StartCoroutine(waitToSlashFunc());
            if(animRunning == false)
                StartCoroutine(waitToAnimate());
        }

		if (Input.GetButtonDown("Fire1") && naginataEnabled == true && waitToSlash && wait2 == false && waitAnim == false)
        {
			Naginata nS = naginata.GetComponent<Naginata> ();
			nS.deSpear ();

			if (animFlip == 1) 
			{
				myAnim.SetInteger("anims", 7);
			}
			else if (animFlip == -1) 
			{
				myAnim.SetInteger ("anims", 8);
			}



            naginata.SetActive(true);

            StartCoroutine(waitToRespond());
            waitToSlash = false;
            StartCoroutine(waitToSlashFunc());
			if(animRunning == false)
				StartCoroutine(waitToAnimate());
        }

        if(Input.GetMouseButtonDown(2) && naginataEnabled == true && waitToCharge)
        {
            StartCoroutine(waitToStop());
            waitToCharge = false;
            StartCoroutine(waitToChargeFunc());
        }

        if(waitAnim)
        {
			if(katanaEnabled)
            	myAnim.SetInteger("anims", 0);
			if (naginataEnabled) 
			{
				if (animFlip == 1) 
				{
					myAnim.SetInteger ("anims", 6);
				} 

				if(animFlip == -1)
				{
					myAnim.SetInteger ("anims", 5);
				}

			}
				
            waitAnim = false;
			animFlip *= -1;
        }

    }

    IEnumerator waitToRespond()
    {
        
        wait = true;
        yield return new WaitForSeconds(0.15f);
        wait = false;
    }

    IEnumerator waitToAnimate()
    {
            animRunning= true;
            yield return new WaitForSeconds(0.1f);
            waitAnim = true;
            animRunning = false;
    }
    IEnumerator waitToStop()
    {
        wait2 = true;
        yield return new WaitForSeconds(0.25f);
        wait2 = false;
		Naginata nS = naginata.GetComponent<Naginata> ();

		if (nS.speared == false) 
		{
			if (animFlip == 1) 
			{
				myAnim.SetInteger ("anims", 6);
			} 

			if(animFlip == -1)
			{
				myAnim.SetInteger ("anims", 5);
			}
		}


		nS.canSpear = false;
    }

    IEnumerator waitToSlashFunc()
    {
        
        yield return new WaitForSeconds(0.25f);
        waitToSlash = true;      
    }

    IEnumerator waitToChargeFunc()
    {
        yield return new WaitForSeconds(3f);
        waitToCharge = true;
    }


    void checkRay()
    {
        checkWall cW = wallCheck.GetComponent<checkWall>();
        if (cW.againstWallVertUp == true) againstWallVertUp = true;
        else againstWallVertUp = false;

        if (cW.againstWallVertDown == true) againstWallVertDown = true;
        else againstWallVertDown = false;

        if (cW.againstWallHorLeft == true) againstWallHorLeft = true;
        else againstWallHorLeft = false;


        if (cW.againstWallHorRight == true) againstWallHorRight = true;
        else againstWallHorRight = false;
    }

    public void Charge()
    {
        if(wait2)
        {
			myAnim.SetInteger("anims", 9);
			Naginata nS = naginata.GetComponent<Naginata> ();
			nS.canSpear = true;
            correctRotation = false;
            rotatePlayer rot = upperTorso.GetComponent<rotatePlayer>();
            rot.shouldRotate = false;
            Vector3 bodyRot = upperTorso.transform.rotation.eulerAngles;

            if(chargeRight)
            {
                             
                if (bodyRot.z > 225 && bodyRot.z <= 315)
                {
                    correctRotation = true;
                    var moveHorRight = new Vector3(1, 0, 0);
                    transform.position += moveHorRight * (speedHorRight + 3) * Time.deltaTime;
                }
            }
            if (chargeLeft)
            {
                
                if (bodyRot.z > 15 && bodyRot.z <= 135)
                {
                    correctRotation = true;
                    var moveHorLeft = new Vector3(-1, 0, 0);
                    transform.position += moveHorLeft * (speedHorLeft + 3) * Time.deltaTime;
                }                 
            }
            if (chargeUp)
            {
                
                if (bodyRot.z > 315 || bodyRot.z <= 45 || (bodyRot.z > 315 && bodyRot.z <360))
                {
                    correctRotation = true;
                    var moveVertUp = new Vector3(0, 1, 0);
                    transform.position += moveVertUp * (speedVertUp + 3) * Time.deltaTime;
                }             
            }
            if (chargeDown)
            {
                
                if (bodyRot.z > 135 && bodyRot.z <= 225)
                {
                    correctRotation = true;
                    var moveVertDown = new Vector3(0, -1, 0);
                    transform.position += moveVertDown * (speedVertDown + 3) * Time.deltaTime;
                }
                    
            }
        }
    }

}

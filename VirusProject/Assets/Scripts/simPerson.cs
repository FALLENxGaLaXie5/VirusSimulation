using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class simPerson : MonoBehaviour
{
    GameManager gameManagerInstance;
	// Use this for initialization
	public Transform target;
    private GameObject gO;
    public float speed;
    public int destCapacity;

    public enum Destination
    {
        Club, Gamestop, Frys, Food, Church, Coffee, Home, School
    };
    public enum PeopleType
    {
        Nerd, WorkAHolic, Parent, Child, HabitualEater, Partier
    };
    public enum State
    {
        Walking, Waiting
    };


    public List<DestSlot> myDestinations = new List<DestSlot>();
    public GameObject[] patrolPoints;
    public string wayPointTrack;

    //Used for patrol
    private int wayPointNumber;
    public bool oncePerCycle = true;
    public int myType;

    public class DestSlot : object
    {
        public int Dest { get; set; }
        public int WaitTime { get; set; }
        public DestSlot(int dest, int waitTime)
        {
            Dest = dest;
            WaitTime = waitTime;
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            //if parameter cannot be cast return false.
            DestSlot d = obj as DestSlot;
            if(d == null)
            {
                return false;
            }
            return (Dest == d.Dest) && (WaitTime == d.WaitTime);
        }

        public bool Equals(DestSlot des)
        {
            if((object)des == null)
            {
                return false;
            }

            return (Dest == des.Dest) && (WaitTime == des.WaitTime);
        }

        public override string ToString()
        {
            return "DestSlot: " + Dest + " - Wait Time: " + WaitTime;
        }

        public override int GetHashCode()
        {
            return Dest ^ WaitTime;
        }
    }


    public class MyState
    {
        public string Message { get; set; }
        public int State { get; set; }
        public MyState(string message, int state)
        {
            Message = message;
            State = state;
        }
    }


    void Start()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag(wayPointTrack);
        wayPointNumber = Random.Range(0, patrolPoints.Length);
        myDestinations.Clear();
    }
    void Awake()
    {
        gameManagerInstance = GameManager.instance;
        myType = Random.Range((int)PeopleType.Nerd, (int)PeopleType.Partier);
        //test function
        //myType = 0;
        Debug.Log("My type is: " + myType);
        destCapacity = 6;
    }

    void Update()
    {
        AILerp lerp = gameObject.GetComponent<AILerp>();
        lerp.enabled = true;
        if(myDestinations.Count < 6)
        {
            allocateDestinations();
        }
        Patrol();
     }

    public void Patrol()
	{
		//Classic Patrol Behaviour
        //if(myDestinations.Peek() != null)
        //{
        //    DestSlot nextDest = myDestinations.Dequeue();
        //}
		GameObject currentWaypoint = patrolPoints [wayPointNumber];
		AILerp lerp = gameObject.GetComponent<AILerp> ();
		lerp.speed = speed;
		lerp.rotationSpeed = 30;
		lerp.target = currentWaypoint.transform;

		if (Vector3.Distance (transform.position, currentWaypoint.transform.position) <= 0.5) 
		{
			wayPointNumber++;
			if (wayPointNumber >= patrolPoints.Length) 
			{
					wayPointNumber = 0;
			}

		}
	}

    //Nerd, WorkAHolic, Parent, Child, HabitualEater, Partier
    void allocateDestinations()
    {
        switch(myType)
        {
            case 0:
                nerdDestiny();
                Debug.Log("Allocating for " + PeopleType.Nerd);
                break;
            case 1:
                workerDestiny();
                Debug.Log("Allocating for " + PeopleType.WorkAHolic);
                break;
            case 3:
                childDestiny();
                Debug.Log("Allocating for " + PeopleType.Child);
                break;
            case 2:
                parentDestiny();
                Debug.Log("Allocating for " + PeopleType.Parent);
                break;
            case 4:
                eaterDestiny();
                Debug.Log("Allocating for " + PeopleType.HabitualEater);
                break;
            case 5:
                partyDestiny();
                Debug.Log("Allocating for " + PeopleType.Partier);
                break;
            default:

                break;
        }
    }

    void checkToAddDest(DestSlot newDest)
    {
        if (myDestinations.Count != 0)
        {
            if (myDestinations[myDestinations.Count - 1].Equals(newDest))
            {
                return;
            }
            else
            {
                myDestinations.Add(newDest);
                Debug.Log("My Destination is: " + newDest.ToString());
            }
        }
        else
        {
            myDestinations.Add(newDest);
            Debug.Log("My Destination is: " + newDest.ToString());
        }
    }
    
    void nerdDestiny()
    {
            int newDestNum = Random.Range(0, 22);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0: case 1: case 2: case 12:case 13:case 14: 
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 3: case 4: case 5: case 15: case 16: case 17:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 6: case 7: case 8: case 9: case 10: case 11:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 15;
                    break;
                case 18:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 5;
                    break;
                case 19: case 20:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 7;
                    break;
                case 21:
                    newDestNum = (int)Destination.School;
                    waitTime = 30;
                    break;
            }
            checkToAddDest(new DestSlot(newDestNum, waitTime));
    }

    
    void workerDestiny()
    {
        int newDestNum = Random.Range(0, 22);
        int waitTime = 0;
        switch (newDestNum)
        {
            case 0:case 1:case 2:case 12:case 13:case 14:
                newDestNum = (int)Destination.Frys;
                waitTime = 40;
                break;
            case 3:case 4:case 5:case 15:case 16:case 17:
                newDestNum = (int)Destination.Coffee;
                waitTime = 8;
                break;
            case 6:case 7:case 8:case 9:case 10:case 11:
                newDestNum = (int)Destination.Home;
                waitTime = 15;
                break;
            case 18:
                newDestNum = (int)Destination.Coffee;
                waitTime = 5;
                break;
            case 19:case 20:
                newDestNum = (int)Destination.Food;
                waitTime = 7;
                break;
            case 21:
                newDestNum = (int)Destination.School;
                waitTime = 30;
                break;
        }
        checkToAddDest(new DestSlot(newDestNum, waitTime));
    }


    void childDestiny()
    {
        int newDestNum = Random.Range(0, 22);
        int waitTime = 0;
        switch (newDestNum)
        {
            case 0:case 1:case 2:case 12:case 13:case 14:
                newDestNum = (int)Destination.School;
                waitTime = 40;
                break;
            case 3:case 4:case 5:case 15:case 16:case 17:
                newDestNum = (int)Destination.Home;
                waitTime = 20;
                break;
            case 6:case 7:case 8:case 9:case 10:case 11:
                newDestNum = (int)Destination.Food;
                waitTime = 15;
                break;
            case 18:
                newDestNum = (int)Destination.Gamestop;
                waitTime = 5;
                break;
            case 19:case 20:
                newDestNum = (int)Destination.Club;
                waitTime = 7;
                break;
            case 21:
                newDestNum = (int)Destination.School;
                waitTime = 30;
                break;
        }
        checkToAddDest(new DestSlot(newDestNum, waitTime));
    }
    void parentDestiny()
    {
        int newDestNum = Random.Range(0, 22);
        int waitTime = 0;
        switch (newDestNum)
        {
            case 0:case 1:case 2:case 12:case 13:case 14:
                newDestNum = (int)Destination.Frys;
                waitTime = 40;
                break;
            case 3:case 4:case 5:case 15:case 16:case 17:
                newDestNum = (int)Destination.Home;
                waitTime = 20;
                break;
            case 6:case 7:case 8:case 9:case 10:case 11:
                newDestNum = (int)Destination.Food;
                waitTime = 15;
                break;
            case 18:
                newDestNum = (int)Destination.Coffee;
                waitTime = 5;
                break;
            case 19:case 20:
                newDestNum = (int)Destination.Church;
                waitTime = 7;
                break;
            case 21:
                newDestNum = (int)Destination.School;
                waitTime = 30;
                break;
        }
        checkToAddDest(new DestSlot(newDestNum, waitTime));
    }
    void eaterDestiny()
    {
        int newDestNum = Random.Range(0, 22);
        int waitTime = 0;
        switch (newDestNum)
        {
            case 0:case 1:case 2:case 12:case 13:case 14:
                newDestNum = (int)Destination.Food;
                waitTime = 40;
                break;
            case 3:case 4:case 5:case 15:case 16:case 17:
                newDestNum = (int)Destination.Home;
                waitTime = 40;
                break;
            case 6:case 7:case 8:case 9:case 10:case 11:
                newDestNum = (int)Destination.Food;
                waitTime = 40;
                break;
            case 18:
                newDestNum = (int)Destination.Coffee;
                waitTime = 5;
                break;
            case 19:case 20:
                newDestNum = (int)Destination.Food;
                waitTime = 40;
                break;
            case 21:
                newDestNum = (int)Destination.School;
                waitTime = 30;
                break;
        }
        checkToAddDest(new DestSlot(newDestNum, waitTime));
    }
    void partyDestiny()
    {
        int newDestNum = Random.Range(0, 22);
        int waitTime = 0;
        switch (newDestNum)
        {
            case 0:case 1:case 2:case 12:case 13:case 14:
                newDestNum = (int)Destination.Food;
                waitTime = 40;
                break;
            case 3:case 4:case 5:case 15:case 16:case 17:
                newDestNum = (int)Destination.Home;
                waitTime = 40;
                break;
            case 6:case 7:case 8:case 9:case 10:case 11:
                newDestNum = (int)Destination.Club;
                waitTime = 40;
                break;
            case 18:
                newDestNum = (int)Destination.Coffee;
                waitTime = 5;
                break;
            case 19:case 20:
                newDestNum = (int)Destination.School;
                waitTime = 10;
                break;
            case 21:
                newDestNum = (int)Destination.Club;
                waitTime = 40;
                break;
        }
        checkToAddDest(new DestSlot(newDestNum, waitTime));
    }
    
}
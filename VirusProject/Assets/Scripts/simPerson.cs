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


    public Queue<DestSlot> myDestinations = new Queue<DestSlot>();
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
    }
    void Awake()
    {
        gameManagerInstance = GameManager.instance;
        myType = Random.Range((int)PeopleType.Nerd, (int)PeopleType.Partier);
        myDestinations.Clear();
        destCapacity = 6;
    }

    void Update()
    {
        AILerp lerp = gameObject.GetComponent<AILerp>();
        lerp.enabled = true;
        allocateDestinations();
        Patrol();
     }

    public void Patrol()
	{
		//Classic Patrol Behaviour
        if(myDestinations.Peek() != null)
        {
            DestSlot nextDest = myDestinations.Dequeue();
        }
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

    void allocateDestinations()
    {
        switch(myType)
        {
            case (int)PeopleType.Nerd:
                nerdDestiny();
                break;
            case (int)PeopleType.WorkAHolic:
                workerDestiny();
                break;
            case (int)PeopleType.Child:
                childDestiny();
                break;
            case (int)PeopleType.Parent:
                parentDestiny();
                break;
            case (int)PeopleType.HabitualEater:
                eaterDestiny();
                break;
            case (int)PeopleType.Partier:
                partyDestiny();
                break;
            default:

                break;
        }
    }

    //Club, Gamestop, Frys, Food, Church, Coffee, Home
    void nerdDestiny()
    {
        if(myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch(newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 4;
                    break;
                case 1:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 5;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while(myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 6);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 4:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 4;
                    break;
                case 5:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 5;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
    void workerDestiny()
    {
        if (myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while (myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 6);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 4:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 40;
                    break;
                case 5:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 40;
                    break;
                case 6:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 50;
                    break;
                case 7:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 50;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
    void childDestiny()
    {
        if (myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 3;
                    break;
                case 1:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 2;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while (myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 8);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 4:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 3;
                    break;
                case 5:
                    newDestNum = (int)Destination.Church;
                    waitTime = 5;
                    break;
                case 6:
                    newDestNum = (int)Destination.School;
                    waitTime = 40;
                    break;
                case 7:
                    newDestNum = (int)Destination.School;
                    waitTime = 40;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
    void parentDestiny()
    {
        if (myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Church;
                    waitTime = 3;
                    break;
                case 1:
                    newDestNum = (int)Destination.Frys;
                    waitTime = 2;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while (myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 8);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 4:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 3;
                    break;
                case 5:
                    newDestNum = (int)Destination.Church;
                    waitTime = 3;
                    break;
                case 6:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 20;
                    break;
                case 7:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 20;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
    void eaterDestiny()
    {
        if (myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Food;
                    waitTime = 10;
                    break;
                case 1:
                    newDestNum = (int)Destination.Food;
                    waitTime = 10;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 10;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while (myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 8);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 10;
                    break;
                case 3:
                    newDestNum = (int)Destination.Food;
                    waitTime = 10;
                    break;
                case 4:
                    newDestNum = (int)Destination.Gamestop;
                    waitTime = 3;
                    break;
                case 5:
                    newDestNum = (int)Destination.Church;
                    waitTime = 3;
                    break;
                case 6:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 20;
                    break;
                case 7:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 20;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
    void partyDestiny()
    {
        if (myDestinations.Count < 1)
        {
            int newDestNum = Random.Range(0, 4);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Club;
                    waitTime = 15;
                    break;
                case 1:
                    newDestNum = (int)Destination.Club;
                    waitTime = 15;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
            }
            myDestinations.Enqueue(new DestSlot(newDestNum, waitTime));
        }
        while (myDestinations.Count < 6)
        {
            int newDestNum = Random.Range(0, 8);
            int waitTime = 0;
            switch (newDestNum)
            {
                case 0:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 1:
                    newDestNum = (int)Destination.Home;
                    waitTime = 40;
                    break;
                case 2:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 3:
                    newDestNum = (int)Destination.Food;
                    waitTime = 8;
                    break;
                case 4:
                    newDestNum = (int)Destination.Club;
                    waitTime = 15;
                    break;
                case 5:
                    newDestNum = (int)Destination.Club;
                    waitTime = 15;
                    break;
                case 6:
                    newDestNum = (int)Destination.Coffee;
                    waitTime = 20;
                    break;
                case 7:
                    newDestNum = (int)Destination.Club;
                    waitTime = 15;
                    break;
            }
            DestSlot newDest = new DestSlot(newDestNum, waitTime);
            if (!myDestinations.Contains(newDest))
            {
                myDestinations.Enqueue(newDest);
            }
        }
    }
}
/**
    Virus Simulation Project - Software Engineering Comp 350
    simPerson.cs
    Purpose: Holds information about the AI, including destination queues and patrol points, 
        along with animation data.

    @author Joshua Steward
    @version 1.0 11/7/2016
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class simPerson : MonoBehaviour
{

    //references instance of Game Manager
    GameManager gameManagerInstance;
    
    //references target position
	public Transform target;

    //go object
    private GameObject gO;

    //speed of this AI object
    public float speed;

    //capacity of destination queue
    public int destCapacity;

    //next destination in queue
    public DestSlot nextDest;

    //current waypoint - used in patrol
    public GameObject currentWaypoint;

    //Enum - Destination enum
    public enum Destination
    {
        Club, Gamestop, Frys, Food, Church, Coffee, Home, School
    };

    //Enum - people type enum
    public enum PeopleType
    {
        Nerd, WorkAHolic, Parent, Child, HabitualEater, Partier
    };

    //Enum - current state enum
    public enum State
    {
        Walking, Waiting, Detection
    };

    //current State
    public State myState;

    //queue of destinations
    public List<DestSlot> myDestinations = new List<DestSlot>();

    //array of current patrolPoints
    public int[] patrolPoints;

    //wayPoint string literals
    public string wayPointTrack;

    //Used for patrol
    private int wayPointNumber;

    //cyclic indicator
    public bool oncePerCycle = true;

    //person type (need to re-implement as enum state)
    public int myType;


    /**
        Virus Simulation Project - Software Engineering Comp 350
        simPerson.cs
        Purpose: Holds information about the destination slots used to queue patrol points for AI.
            Including Dest as an int and the waitTime for each destination.

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    public class DestSlot : object
    {
        //dest int
        public int Dest { get; set; }
        //wait time at destination
        public int WaitTime { get; set; }

        /**
            Constructor for this object

            @param dest The destination indicator
            @param waitTime The waitTime for this destination
        */
        public DestSlot(int dest, int waitTime)
        {
            Dest = dest;
            WaitTime = waitTime;
        }

        /**
            Will compare two destination slots for equality
            
            @param obj The obj to compare for equality
            @return bool Equality
        */
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

        /**
            Will compare two destination slots for equality (overrider)
            
            @param des The obj to compare for equality
            @return bool Equality
        */
        public bool Equals(DestSlot des)
        {
            if((object)des == null)
            {
                return false;
            }

            return (Dest == des.Dest) && (WaitTime == des.WaitTime);
        }

        /**
            Converts object to string form
            
            @return string String form of DestSlot
        */
        public override string ToString()
        {
            return "DestSlot: " + Dest + " - Wait Time: " + WaitTime;
        }


        /**
            Will compare two destination slots for equality
            
            @return hash The hashed slaying hasher of the hash code
        */
        public override int GetHashCode()
        {
            return Dest ^ WaitTime;
        }
    }



    /**
        Virus Simulation Project - Software Engineering Comp 350
        simPerson.cs
        Purpose: Holds information about the state of the AI

        @author Joshua Steward
        @version 1.0 11/7/2016
    */
    public class MyState
    {
        //message to be sent
        public string Message { get; set; }
        //state
        public int State { get; set; }

        /**
            Constructor for this object

            @param message The string message to be sent
            @param state The state of the AI
        */
        public MyState(string message, int state)
        {
            Message = message;
            State = state;
        }
    }



    /**
    Initialization Function. Initializes global variables. Similar to constructors.
    */
    void Start()
    {
        patrolPoints = new int[6];
        gameManagerInstance = GameManager.instance;
        myType = UnityEngine.Random.Range((int)PeopleType.Nerd, (int)PeopleType.Partier);
        myDestinations.Clear();
        allocateDestinations();
        destCapacity = 6;
        nextDest = myDestinations[0];
        currentWaypoint = getDestinationWayPoint(nextDest);
        myState = State.Walking;
    }

    /**
    Update is called at the beginning of every frame at run time.
    This means that all runnable code is ran at one point or another from here.
    Similar to main or runnable with frame by frame implementation.
    Here, update will be used to copy in destination data for AI so it can be used for debugging. 
        Will then switch control flow based on current state; if walking, it will continue to allocate destinations and patrol appropriately.
        If detected player, will continue to allocate destinations for a runner and runAway.
    */
    void Update()
    {
        copyArrayTest();
        switch(myState)
        {
            case State.Walking:
                if (myDestinations.Count < destCapacity)
                {
                    allocateDestinations();
                }
                Patrol();
                break;
            case State.Detection:
                if(myDestinations.Count < 1)
                {
                    allocateForRunner();
                }
                runAway();
                break;
            case State.Waiting:
                break;
            default:
                break;
        }
     }

    /**
        Will add a new home destination to destination queue. Debugging info available.
    */
    void allocateForRunner()
    {
        myDestinations.Add(new DestSlot((int)Destination.Home, 40));
        currentWaypoint = getDestinationWayPoint(myDestinations[0]);
        //Debug.Log("I have " + myDestinations.Count + " destinations!");
    }

    /**
        Sets pathfinding class variable data, speeds , and compares to target position.
    */
    void runAway()
    {
        AILerp lerp = gameObject.GetComponent<AILerp>();
        lerp.enabled = true;
        lerp.speed = speed + 3.0f;
        lerp.rotationSpeed = 30;
        lerp.target = currentWaypoint.transform;


        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) <= 1.5)
        {
            myState = State.Walking;
            myDestinations.RemoveAt(0);
            allocateDestinations();
            nextDest = myDestinations[0];
            currentWaypoint = getDestinationWayPoint(nextDest);
            //Debug.Log("My new state is " + myState);
        }

    }

    /**
    Sets pathfinding class variables, speeds, and compares to destination. On quick transitions between destinations and states,
        will run a test to make sure there are destinations available.
    */
    public void Patrol()
	{
		AILerp lerp = gameObject.GetComponent<AILerp> ();
        lerp.enabled = true;
        lerp.speed = speed;
		lerp.rotationSpeed = 30;
		lerp.target = currentWaypoint.transform;

		if (Vector3.Distance (transform.position, currentWaypoint.transform.position) <= 1.5) 
		{
            myDestinations.RemoveAt(0);
            try
            {
                nextDest = myDestinations[0];
            }
            catch(ArgumentOutOfRangeException e)
            {
                Debug.Log("For some reason we dont have any destinations! Allocating.");
                allocateDestinations();
            }
            currentWaypoint = getDestinationWayPoint(nextDest);
        }
	}

    /**
        Copies destinations array for debugging.
    */
    void copyArrayTest()
    {
        patrolPoints = new int[myDestinations.Count];
        for(int i = 0; i < myDestinations.Count; i++)
        {
            patrolPoints[i] = myDestinations[i].Dest;
        }
        
    }

    /**
        Will search for the waypoint associated with the next destination

        @param nextDest the next destination to find waypoint for
        @return waypoint for the next destination
    */
    GameObject getDestinationWayPoint(DestSlot nextDest)
    {
        int destNum = nextDest.Dest;
        //Debug.Log("This destinations number is: " + destNum);
        foreach(GameObject currBuild in gameManagerInstance.destBuildings)
        {
            switch(destNum)
            {
                case 0:
                    foreach(Transform t in currBuild.transform)
                    {
                        if(t.tag == "clubTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);        
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach(Transform n in currBuild.transform)
                            {
                                if(n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "gameStopTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 2:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "frysElectronicsTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 3:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "foodTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 4:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "churchTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 5:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "coffeeTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 6:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "homeTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                case 7:
                    foreach (Transform t in currBuild.transform)
                    {
                        if (t.tag == "schoolTag")
                        {
                            currentWaypoint = t.gameObject;
                            gameManagerInstance.destBuildings.Remove(currBuild);
                            gameManagerInstance.destBuildings.Add(currBuild);
                            foreach (Transform n in currBuild.transform)
                            {
                                if (n.tag == "A_Points")
                                {
                                    return n.gameObject;
                                }
                            }
                        }
                    }
                    break;
                default:
                    break; ;
            }
        }
        return null;
    }

    /**
        Will allocate destinations based on person type
    */
    void allocateDestinations()
    {
        switch(myType)
        {
            case 0:
                nerdDestiny();
                //Debug.Log("Allocating for " + PeopleType.Nerd);
                break;
            case 1:
                workerDestiny();
                //Debug.Log("Allocating for " + PeopleType.WorkAHolic);
                break;
            case 3:
                childDestiny();
                //Debug.Log("Allocating for " + PeopleType.Child);
                break;
            case 2:
                parentDestiny();
                //Debug.Log("Allocating for " + PeopleType.Parent);
                break;
            case 4:
                eaterDestiny();
                //Debug.Log("Allocating for " + PeopleType.HabitualEater);
                break;
            case 5:
                partyDestiny();
                //Debug.Log("Allocating for " + PeopleType.Partier);
                break;
            default:

                break;
        }
    }

    /**
        Will check if destination is already in queue and add or deallocate accordingly.

        @param newDest destination to be checked against
    */
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
                //Debug.Log("My Destination is: " + newDest.ToString());
            }
        }
        else
        {
            myDestinations.Add(newDest);
            //Debug.Log("My Destination is: " + newDest.ToString());
        }
    }
    
    /**
        Allocates destinations for nerd person type
    */
    void nerdDestiny()
    {
            int newDestNum = UnityEngine.Random.Range(0, 22);
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

    /**
        Allocates destinations for worker person type
    */
    void workerDestiny()
    {
        int newDestNum = UnityEngine.Random.Range(0, 22);
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

    /**
        Allocates destinations for child person type
    */
    void childDestiny()
    {
        int newDestNum = UnityEngine.Random.Range(0, 22);
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

    /**
        Allocates destinations for parent person type
    */
    void parentDestiny()
    {
        int newDestNum = UnityEngine.Random.Range(0, 22);
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

    /**
        Allocates destinations for eater person type
    */
    void eaterDestiny()
    {
        int newDestNum = UnityEngine.Random.Range(0, 22);
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

    /**
        Allocates destinations for partier person type
    */
    void partyDestiny()
    {
        int newDestNum = UnityEngine.Random.Range(0, 22);
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
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

/* THIS SCRIPT SHOULD BE PLACED ON THE ENEMY
 * ------------------------------------------
    TO ADD AN ITEM: 
       Step 1.) edit public int[] table (make sure the numbers add up to 100%

        Example:
------------------------------------
        public int[] table = 
    { 
        20, //Bow
        20, //Crossbow
        20, //Dagger
        20, //Healthpot
        20, //Crack Cocaine
    }; 
--------------------------------------
^ In the above example, each item has a 20% drop rate. 

    Step 2.) Look for the line of code that starts with "Object[] subListObjects" in void Start(). The part you are looking for
    is the "Loot/commonTable1" or something similar. This is what tells the game where to look for the items in question! Make sure
    it is pointing torwards the correct folder in the project Hierchy.
    
    Step 3.) Look at step 1, do you see how Bow is above Crossbow, and crack Cocaine is below everything? Make sure the Project Hierchy in
    the editor looks exactly the same as the list in step 1! this will ensure everything is the correct drop rate

    and that's it! 
    */
public class commonLootTable1 : MonoBehaviour
{
    public static List <GameObject> commonItems = new List<GameObject>();
    public int[] table = 
    { 
        60, //Bow
        30, //Crossbow
        10  //Dagger
    };

    public int total; //This variable handles the total % chance. This should always add up to 100.

    public int randomNumber;
    void Start()
    {

        Object[] subListObjects = Resources.LoadAll("Loot/commonTable1", typeof(GameObject)); //Loads the gameobjects in "Assets/Resources/Loot/commonTable1"

        foreach (GameObject subListObject in subListObjects) //foreach object in subListObjects (declared right above this)
        {
            GameObject lo = (GameObject)subListObject; 
            commonItems.Add(lo); //This is the line that adds the game objects from "Loot/loottable" to the commonItems list
        }

        foreach (var item in table) // This foreach loop adds the drop chance int[] which is declared before void start
        {
            total += item; //This adds up all the integers declared before void start. It should equal 100!
            if(total != 100)
            {
                Debug.LogError("Total percentage of drop chance does not equal 100% in this drop table!");
            }
        }


    }

    void SpawnItem() //This function handles spawning the item
    {
        randomNumber = Random.Range(0, total); //Picks a number between 0 and the var total (which should be 100!) 

        for (int i = 0; i < table.Length; i++) //for every item in the table (declared before void start) do...
        {
            if (randomNumber <= table[i]) //If the Random number <= drop chance, spawn item
            {
                GameObject myObj = Instantiate(commonItems[i]) as GameObject; //Instatiates the object into the game world
                myObj.transform.position = transform.position; //This is where the item will be spawned
                Debug.Log("Spawning done! Spawned " + commonItems[i].ToString()); 
                return; //This tells the loop to stop. If you remove this it will spawn 2-3 items. 
            }
            else //Otherwise, subtract random number from the drop chance. For example, if Random number pulled 92 but the highest drop chance is 60 it will perform 80-60 = 20, where it will try to see if the next drop chance (30 for example) is less or equal to. So in this example 20 is <= 30 so the 2nd item is dropped.
            {
                randomNumber -= table[i];
            }
        }
    }

    private void Update()
    {
         if (Input.GetKeyDown("space"))
         {
             SpawnItem();
             
         } 
        
    }

}

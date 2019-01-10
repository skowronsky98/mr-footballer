using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    private GameObject[] levelList;
    public static int index;


    public static LevelSelection Instance;


    private void Start()
    {
        SecurePlayerPrefs.Init();

        BGScrollSnap.unlocked[0] = true;
        levelList = new GameObject[transform.childCount];

        SecurePlayerPrefs.SetBool("Level" + 0.ToString(), BGScrollSnap.unlocked[0]);

        for (int i = 1; i < BGScrollSnap.unlocked.Length; i++)
        {
            BGScrollSnap.unlocked[i] = SecurePlayerPrefs.GetBool("Level" + i.ToString(), BGScrollSnap.unlocked[i]);
        }


        //Fill the array with GO
        for (int i = 0; i < transform.childCount; i++)
        {
            levelList[i] = transform.GetChild(i).gameObject;
        }

        //Turn off objects
        foreach (GameObject go in levelList)
            go.SetActive(false);

        index = SecurePlayerPrefs.GetInt("SelectedBG");
        //Show first object
        if (levelList[index])
            levelList[index].SetActive(true);
        
    }

    public void ArrowRightButton()
    {
        levelList[index].SetActive(false);
       
        do{ 
            index++;

            if (index > levelList.Length - 1)
                index = 0;
                SecurePlayerPrefs.SetInt("SelectedBG", index);


        } while (!BGScrollSnap.unlocked[index]);
        
        levelList[index].SetActive(true);

    }

    public void ArrowLeftButton()
    {
        levelList[index].SetActive(false);
        do {
            index--;


            if (index < 0)
                index = levelList.Length - 1;
                SecurePlayerPrefs.SetInt("SelectedBG", index);

        } while (!BGScrollSnap.unlocked[index]);

        levelList[index].SetActive(true);
    }

}

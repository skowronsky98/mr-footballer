using UnityEngine;

public class PlayerSkinSelection : MonoBehaviour {

    private GameObject[] playerSkinslist;
    private int indexOfPlayerSkin = 0;

    public static PlayerSkinSelection Instance;

	void Start ()
    {
        SecurePlayerPrefs.Init();

        PlayerScrollSnap.unlocked[0] = true;

        SecurePlayerPrefs.SetBool("PlayerSkin" + 0.ToString(), PlayerScrollSnap.unlocked[0]);

        for (int i = 1; i < PlayerScrollSnap.unlocked.Length; i++)
        {
            PlayerScrollSnap.unlocked[i] = SecurePlayerPrefs.GetBool("PlayerSkin" + i.ToString(), PlayerScrollSnap.unlocked[i]);
        }

        playerSkinslist = new GameObject[transform.childCount];

        //Fill the array with GO
        for (int i = 0; i < transform.childCount; i++)
        {
            playerSkinslist[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in playerSkinslist)
            go.SetActive(false);

        indexOfPlayerSkin = SecurePlayerPrefs.GetInt("SelectedPlayer");

        if (playerSkinslist[indexOfPlayerSkin])
            playerSkinslist[indexOfPlayerSkin].SetActive(true);

    }

    public void ArrowRightButton()
    {
        playerSkinslist[indexOfPlayerSkin].SetActive(false);
        do { 
            indexOfPlayerSkin++;

            if (indexOfPlayerSkin > playerSkinslist.Length - 1)
                indexOfPlayerSkin = 0;
                SecurePlayerPrefs.SetInt("SelectedPlayer", indexOfPlayerSkin);

        } while(!PlayerScrollSnap.unlocked[indexOfPlayerSkin]);

        playerSkinslist[indexOfPlayerSkin].SetActive(true);

    }

    public void ArrowLeftButton()
    {

        playerSkinslist[indexOfPlayerSkin].SetActive(false);

        do {
            indexOfPlayerSkin--;


            if (indexOfPlayerSkin < 0)
                indexOfPlayerSkin = playerSkinslist.Length - 1;
                SecurePlayerPrefs.SetInt("SelectedPlayer", indexOfPlayerSkin);


        } while (!PlayerScrollSnap.unlocked[indexOfPlayerSkin]);

        playerSkinslist[indexOfPlayerSkin].SetActive(true);
    }

}

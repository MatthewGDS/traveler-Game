using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageMent : MonoBehaviour
{
    public void LoadLevel() {
	    SceneManager.LoadScene("Level2");
        
    }

    public void LoadExit()
    {
        SceneManager.LoadScene("SampleScene");

    }
}

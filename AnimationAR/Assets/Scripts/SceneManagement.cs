using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartLoadingARScene(){
        gameObject.GetComponent<Animator>().SetBool("isStartClicked",true);
        
    }

    public void StartARScene(){
        SceneManager.LoadScene("ARScene", LoadSceneMode.Single);
    }
}

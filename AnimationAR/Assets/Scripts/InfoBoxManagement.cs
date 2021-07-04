using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoxManagement : MonoBehaviour
{
    private GameObject arPanelInfoMsgBox;

    void Start(){
        arPanelInfoMsgBox = GameObject.FindGameObjectWithTag("ARInfoPanel");
        arPanelInfoMsgBox.SetActive(false);
    }
    public void OpenInfoBox(){
        arPanelInfoMsgBox.SetActive(true);
    }

    public void CloseInfoBox(){
        arPanelInfoMsgBox.SetActive(false);
    }

}

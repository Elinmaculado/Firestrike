using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelChange : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject otherPanel;
    // Start is called before the first frame update
    public void ChangePanel()
    {
        otherPanel.SetActive(true);
        currentPanel.SetActive(false);
    }
}

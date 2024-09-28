using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTable : MonoBehaviour
{
    [SerializeField] HighScoreTable scoreTable;
    private void Start()
    {
        scoreTable.GenerateTable();
    }
}

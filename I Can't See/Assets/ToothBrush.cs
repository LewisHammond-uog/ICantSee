using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : ToothPaste
{
    [SerializeField]
    private GameObject toothPasteGO;
    private ToothPaste toothPaste;
    [SerializeField]
    private Transform pasteSpawn;
    [SerializeField]
    private GameObject tpPaste;
    private Vector3 squirtLocation;

    void Start()
    {
        toothPaste = toothPasteGO.GetComponent<ToothPaste>();
    }

    // Update is called once per frame
    void Update()
    {
        while(toothPaste.isDPadPressed || Input.GetMouseButtonDown(0))
        {
            squirtLocation = new Vector3(pasteSpawn.position.x, pasteSpawn.position.y, pasteSpawn.position.z);
            Instantiate(tpPaste, squirtLocation, Quaternion.identity);

            //Different idea below:
            //Check if raycast is pointing at toothbrush
            //If so, change colour or spawn something toothpaste like on the toothbrush
        }
    }
}


//Rhys Wareham
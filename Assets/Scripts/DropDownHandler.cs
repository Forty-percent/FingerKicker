using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class DropDownHandler : MonoBehaviour
{
    private Dropdown dropdown;
    public static string portName;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = transform.GetComponent<Dropdown>();

        dropdown.ClearOptions();

        foreach (var item in SerialPort.GetPortNames())
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        PortSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { PortSelected(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PortSelected(UnityEngine.UI.Dropdown dropdown)
    {
        int index = dropdown.value;
        portName = dropdown.options[index].text;

        Debug.Log(portName);
    }
}

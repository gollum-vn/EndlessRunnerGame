using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagedText : MonoBehaviour
{
    public TextMeshProUGUI damagedText;
    public int damaged;
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        damagedText.text = "-" + damaged;
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Setup();
    }
    void Setup()
    {
        transform.Translate(new Vector3(0, -0.5f, 0.5f) * Time.deltaTime * speed);
    }
}

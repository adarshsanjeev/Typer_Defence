using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Typer : MonoBehaviour {

    public string msg = "Paladinsoldier";
    private Text textComp;
    private float startDelay = 0.01f;
    private float typeDelay = 0.01f;
    
    // Use this for initialization
    void Start () {
        StartCoroutine("TypeIn");
    }
	void Awake()
    {
        textComp = GetComponent<Text>();

    }

    public IEnumerator TypeIn()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i =0;i <= msg.Length; i++)
        {
            textComp.text = msg.Substring(0,i);
            yield return new WaitForSeconds(typeDelay);
        }
    }
    public IEnumerator TypeOff()
    {
        for (int i = msg.Length;i >=0; i--)
        {
            textComp.text = msg.Substring(0, i);
            yield return new WaitForSeconds(typeDelay);
        }
    }
        // Update is called once per frame
    void Update () {
        //transform.Rotate(0, 1f, 0);
    }
}

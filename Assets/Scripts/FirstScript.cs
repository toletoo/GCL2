using UnityEngine;

public class FirstScript : MonoBehaviour
{
    private string name;
    private int met;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            var randomInt = Random.Range(0, 10);
            met = randomInt;
            for (int i = 0; i < 10; i++) 
            {
                if (met == i)
                {
                    switch (i)
                    {
                        case 0:
                            name = "botak";
                            break;
                        case 1:
                            name = "goon";
                            break;
                        case 2:
                            name = "gcl";
                            break;
                        case 3:
                            name = "sebastian";
                            break;
                        case 4:
                            name = "jarel";
                            break;
                        case 5:
                            name = "ivan";
                            break;
                        case 6:
                            name = "zhi";
                            break;
                        case 7:
                            name = "jerial";
                            break;
                        case 8:
                            name = "goat";
                            break;
                        case 9:
                            name = "wee";
                            break;
                        case 10:
                            name = "dfdff";
                            break;
                    }
                } else
                {
                    i++;
                }
            }
        }
        print(name);
    }
}

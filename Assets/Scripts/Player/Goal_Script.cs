using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal_Script : MonoBehaviour {

    public GameObject car;
    public Text objectiveText;
    bool stageClear = false;

	
	// Update is called once per frame
	void Update ()
    {
        if (!stageClear)
        {
            var temp = FindObjectsOfType<basicAI>();

            if (temp.Length > 0)
            {
                objectiveText.text = "Enemies remaining : " + temp.Length;
            }
            else
            {
                objectiveText.text = "Vengeance accomplished. Go back to the car.";
                stageClear = true;
                car.GetComponent<Car_Handler>().openCar();
            }
        }

	}

    public void levelComplete()
    {
        gameObject.GetComponent<Pause_menu>().activateTransion();
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//This script is used to switch states 

public class Car_Handler : MonoBehaviour {

    public GameObject carOpened;
    public GameObject carClosed;
    public GameObject transitionScreen;
    bool leaving = false;

    void LateUpdate()
    {
        if (leaving)
        {
            var pos = gameObject.transform.position;
            pos.x += Time.deltaTime * 2;
            gameObject.transform.position = pos;
        }
    }

    public void openCar()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        var temp = Instantiate(carOpened);
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
    }

    public void leave()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
        var temp = Instantiate(carClosed);
        temp.transform.SetParent(gameObject.transform);
        temp.transform.localPosition = Vector3.zero;
        leaving = true;

        GameObject.FindObjectOfType<Goal_Script>().levelComplete();
        GameObject.FindObjectOfType<FollowPlayer>().stopFollow();

        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<ScreenFader>().showScreen();

        StartCoroutine(gotoNextLevel());
    }

    IEnumerator gotoNextLevel()
    {
        yield return new WaitForSeconds(2.5f);

        var id = SceneManager.GetActiveScene().buildIndex;
        if (id >= (SceneManager.sceneCountInBuildSettings-1))
            SceneManager.LoadScene("Credits");
        else
            SceneManager.LoadScene(id + 1);
    }

}

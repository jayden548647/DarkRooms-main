using UnityEngine;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Transform Door1;
    public Transform Door2;
    public Transform Door3;
    int collected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + new Vector3(0, 1, 0);
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == ("Collectables"))
        {
            other.gameObject.SetActive(false);
            collected = collected + 1;
        }

        if (other.gameObject.tag == ("Exit1") && collected == 4)
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == ("Exit2") && collected == 10)
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == ("Exit3") && collected == 11)
        {
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.tag == ("Enemy"))
        {
            animator.SetBool("Lost", true);
        }
        if(other.gameObject.tag  == ("DidAWin"))
        {
            ChangeScene("win");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Enemy"))
        {
            animator.SetBool("Lost", true);
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

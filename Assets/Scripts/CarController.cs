using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGainPerSecond = 1f;
    [SerializeField] private float turnSpeed = 200f;

    private int stear;

    private void Update()
    {
        transform.Rotate(0, stear * Time.deltaTime * turnSpeed, 0);
        speed += speedGainPerSecond * Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    public void Stearing(int value)
    {
        stear = value;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }
    }
}

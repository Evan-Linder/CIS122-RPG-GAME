// written by Evan Linder

using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject objectToFollow;
    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        // checks for the position of the object it is instructoreed to follow and moves the camera depending on it.
        Vector3 position = this.transform.position;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);

        this.transform.position = position;
    }


}

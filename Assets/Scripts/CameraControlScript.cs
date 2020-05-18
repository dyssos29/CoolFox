using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public GameObject avatar;
    public float cameraPositionOffset;
    public float cameraSmoothnessOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (avatar.transform.localScale.x >0f)
            transform.position = Vector3.Lerp(transform.position, new Vector3(avatar.transform.position.x + cameraPositionOffset
                                            , transform.position.y
                                            , transform.position.z)
                                            , cameraSmoothnessOffset * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(avatar.transform.position.x - cameraPositionOffset
                                            , transform.position.y
                                            , transform.position.z)
                                            , cameraSmoothnessOffset * Time.deltaTime);
    }
}

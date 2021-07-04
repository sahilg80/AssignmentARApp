using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioTouchInteration : MonoBehaviour
{
    private float initialDistance;
    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if (hit.transform.gameObject.tag == "MarioPrefab")
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        transform.Rotate(0, -touch.deltaPosition.x * 0.4f, 0, Space.World);
                    }
                    print("mario hitted");

                    if (Input.touchCount == 2)
                    {
                        var touchZero = Input.GetTouch(0);
                        var touchOne = Input.GetTouch(1);
                        if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled ||
                        touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                        {
                            return;
                        }
                        else if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                        {
                            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                            initialScale = gameObject.transform.localScale;
                        }
                        else
                        {
                            if (Mathf.Approximately(initialDistance, 0))
                            {
                                return;
                            }
                            var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                            var factor = currentDistance / initialDistance;
                            gameObject.transform.localScale = initialScale * factor;
                        }
                    }
                }
            }
        }


    }

}

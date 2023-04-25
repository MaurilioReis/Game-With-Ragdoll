using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    Touch touchJoystic;
    Vector2 startPositionTouch;
    RectTransform centerJoystick;
    CanvasGroup alphaController;

    [Header("Configurations Joystic")]
    public float reguleDistance = 150;

    [Space(10)]

    public Vector2 directionJoystick;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        touchJoystic = new Touch { fingerId = -1 };

        centerJoystick = gameObject.transform.GetChild(0).GetComponent<RectTransform>();

        startPositionTouch = centerJoystick.position;

        alphaController = gameObject.GetComponent<CanvasGroup>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for(int numberTouch = 0; numberTouch < Input.touchCount; numberTouch++ )
            {
                if(touchJoystic.fingerId == -1)
                {
                    // Register touch
                    if (Input.GetTouch(numberTouch).position.y < Screen.height / 2)
                    {
                        touchJoystic = Input.GetTouch(numberTouch);

                        alphaController.alpha = 1;
                        startPositionTouch = touchJoystic.position;
                        transform.position = touchJoystic.position;
                    }
                }
                else 
                { 
                    if (Input.GetTouch(numberTouch).fingerId == touchJoystic.fingerId)
                    {
                        touchJoystic = Input.GetTouch(numberTouch);
                    }
                }
            }

            if (touchJoystic.fingerId != -1)
            {
                // Drop
                if (touchJoystic.phase == TouchPhase.Canceled || touchJoystic.phase == TouchPhase.Ended)
                {
                    touchJoystic = new Touch { fingerId = -1 };
                    directionJoystick = Vector2.zero;
                }
                else // Movement
                {
                    Vector2 direction = touchJoystic.position - startPositionTouch;
                    centerJoystick.position = startPositionTouch + Vector2.ClampMagnitude(direction, reguleDistance);

                    Vector2 distanceMoveJoystick = (Vector2)centerJoystick.position - (Vector2)gameObject.transform.position;

                    directionJoystick = new Vector2(distanceMoveJoystick.x / reguleDistance, distanceMoveJoystick.y / reguleDistance);
                }
            }
            else
            {
                directionJoystick = Vector2.zero;
            }
        }

        //Unselected
        if(alphaController.alpha > 0 && touchJoystic.fingerId == -1)
        {
            centerJoystick.position = Vector2.MoveTowards(centerJoystick.position, startPositionTouch, 10);
            alphaController.alpha -= Time.deltaTime;
        }
    }

}
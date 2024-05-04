using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    public float dodgeSpeed = 5.0f;

    public float rollSpeed = 5.0f;

    public enum MobileHorizMovement
    {
        Accelerometer,
        ScreenTouch
    }
    public MobileHorizMovement horizMovement = MobileHorizMovement.Accelerometer;

    [Header("Swipe Properties")]
    public float swipeMove = 2.0f;

    //distance player swipe before excute action
    public float minSwipeDistance = 0.25f;

    private float minSwipeDistancePixels;

    //starting position of mobile touch events
    private Vector2 touchStart;

    [Header("Scaling Properties")]
    public float minScale = 0.5f;
    public float maxScale = 3.0f;

    public float currentScale;

    [Header("Object References")]
    public TextMeshProUGUI scoreText;

    private float score = 0;

    public float Score { 
        get { return score; } 
        set {
            score = value;
            if (scoreText == null)
            {
                Debug.LogError("Score text is not set");
                return;
            }

            scoreText.text = string.Format("{0:0}", score);
        } 
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        minSwipeDistancePixels = minSwipeDistance * Screen.dpi;
        
        Score = 0;
    }

    void FixedUpdate()
    {
        if (PauseScreen.paused)
        {
            return;
        }

        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
                //horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;
            if (Input.GetMouseButton(0))
            {
                horizontalSpeed = CalculateMovement(Input.mousePosition);
            }
        #elif UNITY_IOS || UNITY_ANDROID
             if (horizMovement == MobileHorizMovement.Accelerometer)
             {
                horizontalSpeed = Input.acceleration.x * dodgeSpeed;
             }

             if (Input.touchCount > 0)
             {
                if (horizMovement == MobileHorizMovement.ScreenTouch)
                {
                Touch touch = Input.touches[0];
                horizontalSpeed = CalculateMovement(touch.position);
                }
             }           
        #endif

        rollSpeed += Time.deltaTime / 60;
        rb.AddForce(horizontalSpeed, 0 , rollSpeed);
    }

    private float CalculateMovement(Vector3 pixelPos)
    {
        var worldPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float xMove = 0;

        if (worldPos.x < 0.5f)
        {
            xMove = -1;
        }
        else
        {
            xMove = 1;
        }

        return xMove * dodgeSpeed;
    }

    void Update()
    {
        if (PauseScreen.paused)
        {
            return;
        }

        Score += Time.deltaTime;

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR

        // Rest of the Update function...

        /* If the mouse is tapped */
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            TouchObject(screenPos);
        }
#elif UNITY_IOS || UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            ScalePlayer();
            TouchObject(touch.position);
        }
#endif
    }

    private void ScalePlayer()
    {
        //if two touches
        if (Input.touchCount != 2) return;
        else
        {
            Touch touch0 = Input.touches[0];
            Touch touch1 = Input.touches[1];

            //position in the previous frame of each touch
            Vector2 touch0Prev = touch0.position - touch0.deltaPosition;
            Vector2 touch1Prev = touch1.position - touch1.deltaPosition;

            //distance between the touches in each frame
            float prevTouchDeltaMag = (touch0Prev - touch1Prev).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;
            
            //diffirent in distance between each frame
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float newScale = currentScale - (deltaMagnitudeDiff * Time.deltaTime);
            newScale = Mathf.Clamp(newScale, minScale, maxScale);

            //update player scale
            transform.localScale = Vector3.one * newScale;
            currentScale = newScale;
        }

    }

    //if touching game object so call
    public static void TouchObject(Vector2 screenPos)
    {
        Ray touchRay = Camera.main.ScreenPointToRay(screenPos);

        RaycastHit hit;

        int layerMark = ~0;

        if (Physics.Raycast(touchRay, out hit, Mathf.Infinity, layerMark,
            QueryTriggerInteraction.Ignore))
        {
            hit.transform.SendMessage("PlayerTouch", SendMessageOptions.DontRequireReceiver);
        }
    }
}

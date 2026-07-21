using UnityEngine;
using UnityEngine.UI;

public class BrokenLadder : MonoBehaviour
{
    [SerializeField] GameObject brokenLadder;
    [SerializeField] GameObject completeLadder;
    public bool canRepair = false;
    [SerializeField] float repairTime = 1.5f;
    private float currentRepairTime = 0f;

    public bool isBroken = true;
    [SerializeField] GameObject sliderObj;
    Slider repairSlider;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        repairSlider = sliderObj.GetComponent<Slider>();
        repairSlider.maxValue = repairTime;
        repairSlider.value = currentRepairTime;
        sliderObj.SetActive(false);
        brokenLadder.SetActive(true);
        completeLadder.SetActive(false);
        canRepair = false;
        currentRepairTime = 0f;
        isBroken = true;
        

    }

    // Update is called once per frame
    void Update()
    {

        bool movementPressed = Input.GetAxisRaw("Horizontal") != 0|| Input.GetKeyDown(KeyCode.Space); // check for input

        // hold leftshift to repair + progress bar appears as long as mario doesn't move
        if (canRepair && Input.GetKey(KeyCode.LeftShift) && !movementPressed) 
        {
            sliderObj.SetActive(true); // progress bar appears and starts to fill
            if (currentRepairTime < repairTime)
            {
                currentRepairTime += Time.deltaTime;
                repairSlider.value = currentRepairTime;
            }
            else // progress bar maxes out and ladder is fixed
            {
                canRepair = false;
                isBroken = false;
                brokenLadder.SetActive(false);
                completeLadder.SetActive(true);
                sliderObj.SetActive(false);
            }
        }
        else // if mario moves, progress bar resets
        {
            currentRepairTime = 0; 
            repairSlider.value = currentRepairTime;
            sliderObj.SetActive(false);
        }
    }

}

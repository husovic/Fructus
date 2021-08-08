using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    private float lastClick = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetEnabled(false);
        if (Application.platform == RuntimePlatform.Android)
            Destroy(GetComponentInChildren<TextMesh>());
    }

    // Update is called once per frame
    void Update()
    {
        var mouseInScreen = Input.mousePosition;
        var mouseInWorldCoordinate = Camera.main.ScreenToWorldPoint(mouseInScreen);
        mouseInWorldCoordinate.z = 10;

        transform.position = mouseInWorldCoordinate;

        if (Input.GetMouseButtonDown(0))
        {
            lastClick = Time.time;
            SetEnabled(true);
        }
        else if (Input.GetMouseButtonUp(0))
            SetEnabled(false);



        if (Input.GetMouseButton(0) && Time.time - lastClick > .7f)
            SetEnabled(false);
    }

    private void SetEnabled(bool isEnabled)
    {
        GetComponent<CapsuleCollider2D>().enabled = isEnabled;
        if (GetComponentInChildren<TextMesh>() != null)
            GetComponentInChildren<TextMesh>().color = isEnabled
                ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 170);
        transform.GetChild(0).gameObject.SetActive(isEnabled);

        if (!isEnabled)
            FindObjectOfType<GameManager>().ResetCombo();
    }
}

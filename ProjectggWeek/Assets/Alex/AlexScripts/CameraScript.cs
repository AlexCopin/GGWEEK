using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public AnimationCurve zoomCam;
    public AnimationCurve dezoomCam;
    GameObject centerZoom1;
    float zoomForce1;
    float originalSize;
    public Vector3 startTransform;
    // Start is called before the first frame update
    void Start()
    {
        startTransform = this.transform.position;
        originalSize = GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void CamZoom(GameObject centerZoom, float zoomForce, float duration)
    {
        StartCoroutine(Zoom(centerZoom, zoomForce, duration));
    }
    IEnumerator Zoom(GameObject centerZoom, float zoomForce, float duration)
    {
        float elapsed = 0.0f;
        float posX = 0;
        float posY = 0;
        float ratio = 0.0f;
        centerZoom1 = centerZoom;
        zoomForce1 = zoomForce;
        while (elapsed < duration)
        {
            ratio = elapsed / duration;
            ratio = zoomCam.Evaluate(ratio);
            posX = Mathf.Lerp(startTransform.x, centerZoom1.transform.position.x, ratio);
            posY = Mathf.Lerp(startTransform.y, centerZoom1.transform.position.y, ratio);
            this.transform.position = new Vector3(posX, posY, -10);

            GetComponent<Camera>().orthographicSize = Mathf.Lerp(originalSize, zoomForce1, ratio);

            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0.0f;
        while (elapsed < duration)
        {
            ratio = elapsed / duration;
            ratio = dezoomCam.Evaluate(ratio);
            posX = Mathf.Lerp(centerZoom1.transform.position.x, startTransform.x, ratio);
            posY = Mathf.Lerp(centerZoom1.transform.position.y,startTransform.y, ratio);
            this.transform.position = new Vector3(posX, posY, -10);

            GetComponent<Camera>().orthographicSize = Mathf.Lerp(zoomForce1 , originalSize , ratio);

            elapsed += Time.deltaTime;
            yield return null;
        }
        //this.transform.position = startTransform;
        //GetComponent<Camera>().orthographicSize = originalSize;
    }
    public void CamShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }
        this.transform.position = startTransform;
        //GetComponent<Camera>().orthographicSize = sizeOrtho;
    }
}

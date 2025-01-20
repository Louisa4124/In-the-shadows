using System.Collections;
using UnityEngine;

public class IntroCinematicManager : MonoBehaviour
{
    [Header("Cinematic Elements")]
    public GameObject object1;
    public GameObject object2;
    public GameObject introTextObject;

    [Header("Cinematic Timings")]
    public float textDisplayDuration = 3f;
    public float transitionDuration = 1f;

    private Vector3 offScreenPosition = new Vector3(1000f, 1000f, 1000f);

    void Start()
    {
        if (object1 != null) object1.transform.position = offScreenPosition;
        if (object2 != null) object2.transform.position = offScreenPosition;
        if (introTextObject != null) introTextObject.transform.position = offScreenPosition;

        StartCoroutine(PlayIntroCinematic());
    }

    private IEnumerator PlayIntroCinematic()
    {
        if (introTextObject != null)
        {
            yield return StartCoroutine(MoveObjectIn(introTextObject, introTextObject.transform.position));
            yield return new WaitForSeconds(textDisplayDuration);
            yield return StartCoroutine(MoveObjectOut(introTextObject));
        }

        if (object1 != null)
        {
            yield return StartCoroutine(MoveObjectIn(object1, object1.transform.position));
        }
        if (object2 != null)
        {
            yield return StartCoroutine(MoveObjectIn(object2, object2.transform.position));
        }
    }

    private IEnumerator MoveObjectIn(GameObject obj, Vector3 endPosition)
    {
        Vector3 startPosition = obj.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        obj.transform.position = endPosition;
    }

    private IEnumerator MoveObjectOut(GameObject obj)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = offScreenPosition;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / transitionDuration);
            yield return null;
        }

        obj.transform.position = endPosition;
    }
}

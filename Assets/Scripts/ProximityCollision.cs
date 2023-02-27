using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ProximityCollision : MonoBehaviour
{
    private PostProcessVolume m_Volume;
    private Vignette m_Vignette;
    private List<GameObject> collidingObjects = new();

    private void Awake()
    {
        if (m_Volume == null)
        {
            m_Volume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessVolume>();
        }
    }
    private void Start()
    {
        // Create a List of Lasers.
        collidingObjects = new();
        // Create an instance of a vignette
        m_Vignette = ScriptableObject.CreateInstance<Vignette>();
        m_Vignette.enabled.Override(true);
        m_Vignette.intensity.Override(0f);
        m_Vignette.smoothness.Override(1f);
        m_Vignette.color.Override(new Color32(115, 18, 18, 255));
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            collidingObjects.Add(other.gameObject);
        }
    }

    private void Update()
    {
        if (collidingObjects.Count > 0)
        {
            float distance = 0.8f;

            foreach (GameObject collision in collidingObjects)
            {
                (Vector3, Vector3) lineCenter = (collision.GetNamedChild("Start").GetComponent<Transform>().position, collision.GetNamedChild("End").GetComponent<Transform>().position);
                float tempDistance = Vector3.Distance(gameObject.transform.position, FindNearestPointOnLine(lineCenter.Item1, lineCenter.Item2, gameObject.transform.position));
                distance = Mathf.Min(distance, tempDistance);

                if (distance < 0.15)
                {
                    Debug.Log("Dead");
                }
            }

            m_Vignette.intensity.value = 0.8f - distance;
        }
        else
        {
            m_Vignette.intensity.value = 0;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            collidingObjects.Remove(other.gameObject);
        }
    }
    public Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 end, Vector3 point)
    {
        //Get heading
        Vector3 heading = (end - origin);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        //Do projection from the point but clamp it
        Vector3 lhs = point - origin;
        float dotP = Vector3.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return origin + heading * dotP;
    }
}

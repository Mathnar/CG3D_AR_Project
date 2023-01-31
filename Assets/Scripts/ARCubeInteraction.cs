using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCubeInteraction : MonoBehaviour
{
    private ManoGestureContinuous grab;
    private ManoGestureContinuous pinch;
    private ManoGestureTrigger click;

    [SerializeField]
    private Material[] arCubeMaterial;
    [SerializeField]
    private GameObject smallCube;

    private string handTag = "Player";
    private Renderer cubeRenderer;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        grab = ManoGestureContinuous.CLOSED_HAND_GESTURE;
        pinch = ManoGestureContinuous.HOLD_GESTURE;
        click = ManoGestureTrigger.CLICK;
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.sharedMaterial = arCubeMaterial[0];
        cubeRenderer.material = arCubeMaterial[0];
    }

    private void OnTriggerStay(Collider other)
    {
        MoveWhenGrab(other);
        RotateWhenHolding(other);
        SpawnWhenClicking(other);
    }


    private void MoveWhenGrab(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == grab)
        {
            transform.parent = other.gameObject.transform;
        }

        else
        {
            transform.parent = null;
        }
    }


    private void RotateWhenHolding(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_continuous == pinch)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
        }
    }


    private void SpawnWhenClicking(Collider other)
    {
        if (ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info.mano_gesture_trigger == click)
        {
            Instantiate(smallCube, new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 1.5f, transform.position.z), Quaternion.identity);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == handTag)
        {
            cubeRenderer.sharedMaterial = arCubeMaterial[1];
            Handheld.Vibrate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cubeRenderer.sharedMaterial = arCubeMaterial[0];
    }
}
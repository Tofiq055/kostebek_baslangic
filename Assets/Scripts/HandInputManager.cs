using UnityEngine;

public class HandInputManager : MonoBehaviour
{
    [Header("MediaPipe Ayarları")]
    public string containerName = "Point List Annotation"; 
    public int fingerTipIndex = 8;

    [Header("Debug")]
    public bool debugMode = true;

    private Transform indexFingerTip;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Mouse ile Test (Her zaman çalışmalı)
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Tıklandı. Raycast atılıyor...");
            ShootRay(Input.mousePosition);
        }

        // 2. El Takibi
        if (indexFingerTip == null)
        {
            FindHandObject();
        }
        else if (indexFingerTip.gameObject.activeInHierarchy)
        {
            // El aktifse, parmak ucundan sürekli ateş et
            Vector3 screenPos = mainCamera.WorldToScreenPoint(indexFingerTip.position);
            ShootRay(screenPos);
        }
    }

    void FindHandObject()
    {
        // Önce normal ismi dene
        GameObject obj = GameObject.Find(containerName);
        // Bulamazsan Clone dene
        if (obj == null) obj = GameObject.Find(containerName + "(Clone)");
        
        if (obj != null)
        {
            if (obj.transform.childCount > fingerTipIndex)
            {
                indexFingerTip = obj.transform.GetChild(fingerTipIndex);
                Debug.Log("EL HEDEFİ BULUNDU: " + indexFingerTip.name);
            }
        }
    }

    void ShootRay(Vector3 screenPos)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red, 0.1f);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (debugMode) Debug.Log("Vurulan Obje: " + hit.collider.name + " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("Mole"))
            {
                MoleController mole = hit.collider.GetComponent<MoleController>();
                if (mole != null)
                {
                    mole.OnHit();
                    // Vuruş anını logla
                    Debug.Log("KÖSTEBEK VURULDU!");
                }
            }
        }
    }
}
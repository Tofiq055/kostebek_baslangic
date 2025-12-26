using UnityEngine;

public class HandInputManager : MonoBehaviour
{
    [Header("MediaPipe Ayarları")]
    public string containerName = "Point List Annotation"; 
    public int fingerTipIndex = 8;

    [Header("Debug")]
    public bool debugMode = true;
    public bool showDebugSphere = true;

    private Transform indexFingerTip;
    private Camera mainCamera;
    private GameObject debugSphere;
    
    // Titremeyi önlemek için hedef pozisyon takibi
    private Vector3 targetDebugPos; 

    void Start()
    {
        mainCamera = Camera.main;

        if (showDebugSphere)
        {
            debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            debugSphere.name = "DebugAimSphere";
            debugSphere.transform.localScale = Vector3.one * 0.1f; 
            
            if(debugSphere.GetComponent<Collider>()) 
                debugSphere.GetComponent<Collider>().enabled = false;
            
            Renderer ren = debugSphere.GetComponent<Renderer>();
            ren.material.color = Color.red;
            debugSphere.SetActive(false);
        }
    }

    void Update()
    {
        if (indexFingerTip == null) FindHandObject();
        
        bool isHandActive = (indexFingerTip != null && indexFingerTip.gameObject.activeInHierarchy);
        
        if (Input.GetMouseButton(0))
        {
             ShootRay(Input.mousePosition, true);
        }
        else if (isHandActive)
        {
             Vector3 screenPos = mainCamera.WorldToScreenPoint(indexFingerTip.position);
             


             ShootRay(screenPos, true);
        }
        else
        {
            if(debugSphere) debugSphere.SetActive(false);
        }

        if (debugSphere != null && debugSphere.activeSelf)
        {
            debugSphere.transform.position = Vector3.Lerp(debugSphere.transform.position, targetDebugPos, Time.deltaTime * 15f);
        }
    }

    void FindHandObject()
    {
        GameObject obj = GameObject.Find(containerName);
        if (obj == null) obj = GameObject.Find(containerName + "(Clone)");
        
        if (obj != null && obj.transform.childCount > fingerTipIndex)
        {
             indexFingerTip = obj.transform.GetChild(fingerTipIndex);
        }
    }

    void ShootRay(Vector3 screenPos, bool showSphere)
    {
        if (debugSphere) debugSphere.SetActive(showSphere);

        Ray ray = mainCamera.ScreenPointToRay(screenPos);
        
        // Debug Topu Hedef Pozisyonu
        Vector3 desiredPos = ray.GetPoint(0.5f);

        // HITBOX GENİŞLETME (SphereCast)
        // Raycast yerine "Kalın Işın" atıyoruz. 
        // 0.05f yarıçapında bir küre göndererek ıskalamayı zorlaştırıyoruz.
        float hitRadius = 0.05f; 

        if (Physics.SphereCast(ray, hitRadius, out RaycastHit hit))
        {
            // Çarpma noktasına doğru çek
            desiredPos = hit.point - (ray.direction * 0.1f);

            if (hit.collider.CompareTag("Mole"))
            {
                MoleController mole = hit.collider.GetComponent<MoleController>();
                if (mole != null && mole.isActive)
                {
                    mole.OnHit();
                    if(debugMode) Debug.Log("KÖSTEBEK VURULDU!");
                }
            }
        }
        
        targetDebugPos = desiredPos;
    }
}
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARSubsystems; // AR Kütüphanesi

public class LibraryFixer : Editor
{
    // Bu script Unity'nin üst menüsüne yeni bir buton ekler
    [MenuItem("Tools/Force Create Reference Library")]
    public static void CreateLib()
    {
        // Kütüphaneyi hafızada oluştur
        var library = ScriptableObject.CreateInstance<XRReferenceImageLibrary>();
        
        // Dosya olarak Assets klasörüne kaydet
        AssetDatabase.CreateAsset(library, "Assets/MoleImageLibrary.asset");
        AssetDatabase.SaveAssets();
        
        // Dosyayı seçili hale getir ve odaklan
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = library;
        
        Debug.Log("✅ MoleImageLibrary başarıyla oluşturuldu!");
    }
}
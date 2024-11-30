using System.Collections;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameObject floatingTextPrefab; // Floating Text için prefab
    public Transform textSpawnPoint; // Yazýnýn çýkacaðý nokta
    public float textLifetime = 2f; // Yazýnýn ne kadar süre ekranda kalacaðý
    public float floatSpeed = 1f; // Yukarý doðru hareket hýzý


    public void ShowFloatingText(string message, Color textColor)
    {
        if (floatingTextPrefab == null) return;

        // Prefab'ý oluþtur
        GameObject floatingText = Instantiate(floatingTextPrefab, textSpawnPoint.position, Quaternion.identity, transform);

        // Yazý ayarlarý
        TextMeshPro textMesh = floatingText.GetComponent<TextMeshPro>();
        if (textMesh != null)
        {
            Debug.Log("Text mesh null deðil yazý deðiþme");
            textMesh.text = message;
            textMesh.color = textColor;
        }

        StartCoroutine(FloatTextUpwards(floatingText));
    }

    private IEnumerator FloatTextUpwards(GameObject floatingText)
    {
        float elapsedTime = 0;

        while (elapsedTime < textLifetime)
        {
            floatingText.transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e geç

        }

        Destroy(floatingText);

    }

    public void ShowDamageText(float damage)
    {
        string message = $"-{damage} Taken";
        Color damageColor = Color.red; // Kýrmýzý renk

        ShowFloatingText(message, damageColor);
    }

    // Stunned Yazýsý Gösterici
    public void ShowStunnedText()
    {
        string message = "Stunned!";
        Color stunnedColor = Color.blue; // Mavi renk

        ShowFloatingText(message, stunnedColor);
    }
}

using UnityEngine;

public class ChangeOpacity : MonoBehaviour
{
    public Renderer objectRenderer; // Le Renderer de l'objet
    public float targetOpacity = 0.5f; // Valeur d'opacité cible (0 = transparent, 1 = opaque)

    void Start()
    {
        if (objectRenderer != null)
        {
            Material material = objectRenderer.material;
            Color color = material.color;
            color.a = targetOpacity; // Modifier l'alpha
            material.color = color;  // Appliquer la couleur avec la nouvelle opacité
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class RadialExperienceBar : MonoBehaviour
{
    private Image radialImage; // Referencia al componente Image en el botón
    private int experiencePoints = 0; // Puntos actuales de experiencia
    private int maxPoints = 15; // Puntos máximos de experiencia
    Ball ball;
    public bool clickable = false;

    private void Start()
    {
        // Obtener el componente Image en el mismo GameObject
        radialImage = GetComponent<Image>();

        ball = FindObjectOfType<Ball>();
    }

    // Método para agregar experiencia
    public void AddExperiencePoint()
    {
        if (ball.experiencePoints <= maxPoints)
        {
            float fillPercentage = (float)ball.experiencePoints / maxPoints; // Calcula el porcentaje llenado
            radialImage.fillAmount = 1 - fillPercentage; // Para que sea a contrarreloj, se resta del máximo

            Debug.Log("Fill Percentage: " + fillPercentage);
            if((ball.experiencePoints == maxPoints))
            {
                clickable = true;
                Debug.Log("clickable ");
            }

        }
        if (ball.experiencePoints <= 0)
        {
            radialImage.fillAmount = 1;
        }
    }

  
}
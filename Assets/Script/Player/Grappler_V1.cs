using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler_V1 : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public float grappleSpeed = 5f;
    private bool isGrappling = false;

    void Start()
    {
     // Desactiver le lien entre la ligne et le perso au debut du jeu
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // Vérifie si le joueur appuie sur la souris ou non et s'il grappling déjà sinon pas grappin 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isGrappling)
        {
            Grapple();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && isGrappling)
        {
            ReleaseGrapple();
        }
    }

    void FixedUpdate()
    {
        // Si le grappin utilisé -> Grappling
        if (isGrappling)
        {
            HandleGrapple();
        }
    }

    void Grapple()
    {
        // RAYCAST pour detecter la plateforme avec la souris 
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // Si le raycast touche une plateforme et que cette plateforme est marquee avec le tag "Plateforme"
        if (hit.collider != null && hit.collider.CompareTag("Plateforme"))
        {
            // Configurer le tracé (grappin) 
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint.connectedAnchor = mousePos; // Definir le point d'ancrage du grappin (= souris)
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
            isGrappling = true;
        }
    }

    // Relacher la souris = tout faux :)
    void ReleaseGrapple()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
        isGrappling = false;
    }

    void HandleGrapple()
    {
        // Calculer la direction du grappin
        Vector2 grappleDir = (Vector2)transform.position - distanceJoint.connectedAnchor;
        float distance = grappleDir.magnitude; // Calculer la distance entre le personnage et le point d'ancrage du grappin

        // Deplacer le personnage vers le point d'ancrage du grappin à une certaine vitesse
        transform.position = Vector2.MoveTowards(transform.position, distanceJoint.connectedAnchor, grappleSpeed * Time.fixedDeltaTime);
        lineRenderer.SetPosition(1, transform.position);

        if (distance < 0.1f)
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            isGrappling = false; 
        }
    }
}

using UnityEngine;
using System.Collections; // Necesario para IEnumerator / coroutines

public class SonicMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed     = 10f;
    public float jumpForce = 12f;
    public float gravity   = -30f;

    [Header("Efectos")]
    public ParticleSystem dustParticles;
    public ParticleSystem ringDust;
    public AudioClip      bounceSound;
    public AudioClip      sonicYell;

    // ── Componentes privados ──────────────────────────────────────────────────
    private Rigidbody  rb;
    private Animator   anim;
    private bool       isGrounded;
    private Vector3    moveDirection;

    // ─────────────────────────────────────────────────────────────────────────
    void Start()
    {
        rb   = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.useGravity = false; // Gravedad controlada manualmente
    }

    // ─────────────────────────────────────────────────────────────────────────
    void Update()
    {
        // Movimiento horizontal (WASD / flechas)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        moveDirection = new Vector3(h, 0f, v).normalized * speed;

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // ── Animaciones ──────────────────────────────────────────────────────
        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;
        anim.SetFloat("Speed",     horizontalSpeed);
        anim.SetBool ("IsJumping", !isGrounded);

        // Spin Dash: Shift + S
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
            anim.SetTrigger("SpinDash");
    }

    // ─────────────────────────────────────────────────────────────────────────
    void FixedUpdate()
    {
        // Aplica movimiento horizontal conservando velocidad vertical
        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);

        // Gravedad custom
        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // Loop vertical: invierte gravedad al chocar hacia adelante a alta velocidad
        if (rb.linearVelocity.magnitude > 15f &&
            Physics.Raycast(transform.position, transform.forward, 2f))
        {
            transform.Rotate(0f, 0f, 180f, Space.Self);
            gravity = -gravity;
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;

        isGrounded = true;
        gravity    = -30f; // Resetea gravedad normal

        float speed = rb.linearVelocity.magnitude;

        // ── Super bounce SA2-style (umbral épico) ────────────────────────────
        if (speed > 25f)
        {
            // Preserva momentum horizontal, resetea vertical
            rb.linearVelocity = new Vector3(rb.linearVelocity.x * 0.8f, 0f, rb.linearVelocity.z * 0.8f);

            // Rebote alto con algo de dirección
            Vector3 bounceVector = Vector3.up * 38f + rb.linearVelocity.normalized * 0.7f;
            rb.AddForce(bounceVector, ForceMode.Impulse);

            // Giro 360° en el aire
            StartCoroutine(Spin360());

            // Partículas en anillo
            if (ringDust != null)
            {
                ringDust.transform.position = transform.position;
                ringDust.Play();
            }

            // Sonido épico
            if (sonicYell != null)
                AudioSource.PlayClipAtPoint(sonicYell, transform.position, 1.2f);
        }
        // ── Bounce normal (umbral bajo) ──────────────────────────────────────
        else if (speed > 18f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            Vector3 bounceDir = Vector3.up + rb.linearVelocity.normalized * 0.4f;
            rb.AddForce(bounceDir * 25f, ForceMode.Impulse);

            if (dustParticles != null)
            {
                dustParticles.transform.position = transform.position + Vector3.down * 0.5f;
                dustParticles.Play();
            }

            if (bounceSound != null)
                AudioSource.PlayClipAtPoint(bounceSound, transform.position, 0.8f);
        }

        // ── Impulso de aterrizaje fuerte (momentum conservado) ───────────────
        if (rb.linearVelocity.y < -5f)
            rb.AddForce(rb.linearVelocity.normalized * 15f, ForceMode.Impulse);
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Loop trigger: rota y lanza al jugador hacia arriba
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.Rotate(0f, 0f, 180f, Space.Self);
        other.GetComponent<Rigidbody>()?.AddForce(Vector3.up * 20f, ForceMode.Impulse);
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Giro de 360° suave en el aire
    IEnumerator Spin360()
    {
        float      timer    = 0f;
        float      duration = 0.5f;
        Quaternion startRot = transform.rotation;
        Quaternion endRot   = startRot * Quaternion.Euler(0f, 0f, 360f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, endRot, timer / duration);
            yield return null;
        }
    }
}

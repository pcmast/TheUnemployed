using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [Header("Contadores")]
    public int paperBalls = 0;
    public int folders = 0;
    [Header("UI Turno terminado")]
    public GameObject canvasTurnoTerminado;
    public TMP_Text paperBallCounter; // TextMeshPro para bolitas
    public TMP_Text folderCounter;    // TextMeshPro para folders

    [Header("Objetivos")]
    public int bolitasParaTerminarTurno = 20;
    public int foldersParaTerminarTurno = 5;

    [HideInInspector]
    public bool turnoTerminado = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        UpdateUI();
    }

    void Update()
    {
        if (turnoTerminado && Input.GetKeyDown(KeyCode.Tab))
        {
            LiberarMouse();
            SceneManager.LoadScene(5);
        }
    }
    void LiberarMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Método para sumar una bolita
    public void AddPaperBall()
    {
        if (turnoTerminado) return;

        paperBalls++;
        UpdateUI();
        ComprobarObjetivos();
    }

    // Método para sumar una folder
    public void AddFolder()
    {
        if (turnoTerminado) return;

        folders++;
        UpdateUI();
        ComprobarObjetivos();
    }

    void ComprobarObjetivos()
    {
        if (paperBalls >= bolitasParaTerminarTurno && folders >= foldersParaTerminarTurno)
        {
            turnoTerminado = true;

            if (canvasTurnoTerminado != null)
                canvasTurnoTerminado.SetActive(true);
        }
    }

    void UpdateUI()
    {
        if (paperBallCounter != null)
            paperBallCounter.text = "Bolitas: " + paperBalls;

        if (folderCounter != null)
            folderCounter.text = "Folders: " + folders;
    }

    // Reinicia inventario (opcional)
    public void ResetInventory()
    {
        paperBalls = 0;
        folders = 0;
        turnoTerminado = false;
        UpdateUI();
    }
}
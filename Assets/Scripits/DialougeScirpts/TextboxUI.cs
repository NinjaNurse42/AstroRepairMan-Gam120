using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextboxUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject dialougeBox;
    [SerializeField] private TMP_Text textLabel;

    [Header("Optional: Initial Dialogue")]
    [SerializeField] private DialougeObject initialDialouge;

    [Header("Repair Dialogues")]
    [SerializeField] private List<RepairDialouge> repairDialouges;

    [Header("Death Dialogues")]
    [SerializeField] private List<DeathDialouge> deathDialouges;

    private int deathCount = 0;
    private HashSet<RepairPoint> countedRepairs = new HashSet<RepairPoint>();

    private bool spacePressed = false;
    private bool doubleTapDetected = false;

    private float lastSpaceTime = 0f;
    private float doubleTapThreshold = 0.3f;

    void Start()
    {
        if (initialDialouge != null)
            ShowDialouge(initialDialouge);
    }

    void Update()
    {
        CheckRepairs();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;

            if (Time.realtimeSinceStartup - lastSpaceTime <= doubleTapThreshold)
                doubleTapDetected = true;

            lastSpaceTime = Time.realtimeSinceStartup;
        }
    }


    private void CheckRepairs()
    {
        RepairPoint[] points = FindObjectsOfType<RepairPoint>(true);
        int totalRepaired = 0;

        foreach (RepairPoint point in points)
        {
            if (point.isRepaired)
            {
                totalRepaired++;
                countedRepairs.Add(point);
            }
        }

        foreach (var rd in repairDialouges)
        {
            if (!rd.hasTriggered && totalRepaired == rd.repairNumber)
            {
                rd.hasTriggered = true;
                ShowDialouge(rd.dialouge);
                return;
            }
        }
    }

    // =========================
    // 💀 DEATH SYSTEM (FIXED)
    // =========================
    public void OnPlayerDeath()
    {
        deathCount++;

        string reason = PlayerDamage.LastDeathReason;
        Debug.Log($"Death #{deathCount} | Reason: {reason}");

        DeathDialouge fallback = null;

        foreach (var dd in deathDialouges)
        {
            if (dd.hasTriggered) continue;
            if (dd.deathNumber != deathCount) continue;

            // Save fallback
            if (string.IsNullOrEmpty(dd.requiredDeathReason))
            {
                fallback = dd;
                continue;
            }

            // Match reason (case-insensitive)
            if (reason != null &&
                reason.ToLower().Contains(dd.requiredDeathReason.ToLower()))
            {
                TriggerDialogue(dd.dialouge);
                dd.hasTriggered = true;
                return;
            }
        }

        // fallback if no match
        if (fallback != null)
        {
            TriggerDialogue(fallback.dialouge);
            fallback.hasTriggered = true;
        }
    }

    // =========================
    // 💬 DIALOGUE CORE
    // =========================
    private void TriggerDialogue(DialougeObject dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("Dialogue object missing!");
            return;
        }

        ShowDialouge(dialogue);
    }

    public void ShowDialouge(DialougeObject dialogue)
    {
        StopAllCoroutines();

        dialougeBox.SetActive(true);
        Time.timeScale = 0f;

        StartCoroutine(RunDialogue(dialogue));
    }

    private IEnumerator RunDialogue(DialougeObject dialogue)
    {
        foreach (string line in dialogue.Dialouge)
        {
            yield return StartCoroutine(TypeLine(line));

            spacePressed = false;
            yield return new WaitForSecondsRealtime(0.1f);

            yield return new WaitUntil(() => spacePressed);
        }

        CloseDialogue();
    }

    private IEnumerator TypeLine(string line)
    {
        textLabel.text = "";
        doubleTapDetected = false;

        foreach (char c in line)
        {
            textLabel.text += c;

            if (doubleTapDetected)
            {
                textLabel.text = line;
                doubleTapDetected = false;
                break;
            }

            yield return new WaitForSecondsRealtime(0.035f);
        }
    }

    private void CloseDialogue()
    {
        dialougeBox.SetActive(false);
        textLabel.text = "";
        Time.timeScale = 1f;
    }

    // =========================
    // 📦 DATA CLASSES
    // =========================
    [System.Serializable]
    public class RepairDialouge
    {
        public int repairNumber;
        public DialougeObject dialouge;
        [HideInInspector] public bool hasTriggered = false;
    }

    [System.Serializable]
    public class DeathDialouge
    {
        public int deathNumber;

        [Tooltip("Optional. Leave empty = any death")]
        public string requiredDeathReason;

        public DialougeObject dialouge;

        [HideInInspector] public bool hasTriggered = false;
    }
}
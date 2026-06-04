using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Perfecttext;
    [SerializeField] private TextMeshProUGUI Greattext;
    [SerializeField] private TextMeshProUGUI Goodtext;
    [SerializeField] private TextMeshProUGUI Misstext;
    [SerializeField] private TextMeshProUGUI RankText;
    [SerializeField] private TextMeshProUGUI RankShadowText;

    [SerializeField] private AudioSource CountUPSE;

    //MemeÄ¶
    [SerializeField] private AudioSource MemeVoice;

    //NiceMeme
    [SerializeField] private Image NICEMan;
    [SerializeField] private Animator NICEAnim;
    [SerializeField] private AudioClip NICEVoice;
    private bool NICEStart;
    private float NICECount;

    //whoareyoumeme
    [SerializeField] private Image WhoImage;
    [SerializeField] private Animator WhoAnim;
    [SerializeField] private AudioClip WhoVoice;
    private bool WhoStart;
    private float WhoCount;

    //whoooomeme
    [SerializeField] private Image WhooooImage;
    [SerializeField] private Animator WhooooAnim;
    [SerializeField] private AudioClip WhooooVoice;
    private bool WhooooStart;
    private float WhooooCount;

    private string rank;

    private void Start()
    {
        StartCoroutine(ResultAnimation());
    }
    private void Update()
    {
        if(NICEStart)
        {
            NICECount += Time.deltaTime;
            if(NICECount >= 2.05f)
            {
                NICEAnimationEnd();
                NICEStart = false;
            }
        }

        if (WhoStart)
        {
            WhoCount += Time.deltaTime;
            if (WhoCount >= 5.06f)
            {
                WhoAnimationEnd();
                WhoStart = false;
            }
        }

        if (WhooooStart)
        {
            WhooooCount += Time.deltaTime;
            if (WhooooCount >= 3.06f)
            {
                MemeVoice.Stop();
                WhooooAnimationEnd();
                WhooooStart = false;
            }
        }
    }
    IEnumerator ResultAnimation()
    {
        yield return StartCoroutine(CountUp(Perfecttext, CheckNotes.Perfect, 1f));

        yield return StartCoroutine(CountUp(Greattext, CheckNotes.Great, 1f));

        yield return StartCoroutine(CountUp(Goodtext, CheckNotes.Good, 0.5f));

        yield return StartCoroutine(CountUp(Misstext, CheckNotes.MISS, 0.5f));
        RankCheck();

        if(rank == "A" || rank == "B" || rank == "C")
        {
            MemeVoice.clip = NICEVoice;
            MemeVoice.Play();

            yield return new WaitForSeconds(0.5f);

            NICEMan.gameObject.SetActive(true);
            NICEAnim.SetBool("NICE", true);
            NICEStart = true;
        }
        else if(rank == "D" || rank == "E")
        {
            MemeVoice.clip = WhoVoice;
            MemeVoice.Play();

            yield return new WaitForSeconds(0.2f);

            WhoImage.gameObject.SetActive(true);
            WhoAnim.SetBool("Who", true);
            WhoStart = true;
        }
        else if(rank == "S+" || rank == "S")
        {
            MemeVoice.clip = WhooooVoice;
            MemeVoice.Play();

            yield return new WaitForSeconds(0.2f);

            WhooooImage.gameObject.SetActive(true);
            WhooooAnim.SetBool("Whoooo", true);
            WhooooStart = true;
        }
    }

    IEnumerator CountUp(TextMeshProUGUI text, int target, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float t = time / duration;

            int value = Mathf.FloorToInt(Mathf.Lerp(0, target, t));

            text.text = value.ToString();

            yield return null;
        }

        text.text = target.ToString();
    }

    public void BackTitle()
    {
        SceneManager.LoadScene("Title Scene");
    }

    void RankCheck()
    {
        int perfect = CheckNotes.Perfect;
        int great = CheckNotes.Great;
        int good = CheckNotes.Good;
        int miss = CheckNotes.MISS;

        int total = perfect + great + good + miss;

        if (total <= 0)
        {
            RankText.text = "Rank : E";
            return;
        }

        float perfectRate = (float)perfect / total * 100f;
        float missRate = (float)miss / total * 100f;

        rank = "";

        // E
        if (missRate >= 100f)
        {
            rank = "E";

            RankText.color = Color.black;
            RankShadowText.color = Color.black;
        }
        // S+
        else if (perfectRate >= 100f)
        {
            rank = "S+";

            Color purple = new Color(0.7f, 0f, 1f);

            RankText.color = purple;
            RankShadowText.color = purple;
        }
        // S
        else if (perfectRate >= 80f)
        {
            rank = "S";

            RankText.color = Color.blue;
            RankShadowText.color = Color.blue;
        }
        // A
        else if (perfectRate >= 70f && missRate <= 10f)
        {
            rank = "A";

            RankText.color = Color.red;
            RankShadowText.color = Color.red;
        }
        // B
        else if (perfectRate >= 60f && missRate <= 7f)
        {
            rank = "B";

            Color orange = new Color(1f, 0.5f, 0f);

            RankText.color = orange;
            RankShadowText.color = orange;
        }
        // C
        else if (perfectRate >= 40f)
        {
            rank = "C";

            RankText.color = Color.yellow;
            RankShadowText.color = Color.yellow;
        }
        // D
        else
        {
            rank = "D";

            RankText.color = Color.green;
            RankShadowText.color = Color.green;
        }

        RankText.text =  rank;
    }

    public void NICEAnimationEnd()
    {
        NICEMan.gameObject.SetActive(false);
        NICEAnim.SetBool("NICE", false);
    }
    public void WhoAnimationEnd()
    {
        WhoImage.gameObject.SetActive(false);
        WhoAnim.SetBool("Who", false);
    }
    public void WhooooAnimationEnd()
    {
        WhooooImage.gameObject.SetActive(false);
        WhooooAnim.SetBool("Whoooo", false);
    }
}
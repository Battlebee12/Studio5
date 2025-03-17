using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI update;
    [SerializeField] private Transform scoreContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        update.SetText("0");
        containerInitPosition = scoreContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;
        
    }

    // Update is called once per frame
    public void UpdateScore(int score)
    {
        update.SetText($"{score}");
        scoreContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetContainer(score));
        
    }

    private IEnumerator ResetContainer(int score){
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        Vector3 localPosition = scoreContainer.localPosition;
        scoreContainer.localPosition = new Vector3(localPosition.x,containerInitPosition,localPosition.z);
    }
}

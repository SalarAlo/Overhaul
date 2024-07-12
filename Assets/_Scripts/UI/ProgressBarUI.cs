using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Transform progressScale;

    private const float MIN_FILL = .01f;
    private const float MAX_FILL = .01f;
    private float counter;
    private float duration;
    private bool progressing;

    public void StartProgressing(float duration) {
        progressing = true;
        this.duration = duration;
    }

    private void Update() {
        transform.LookAt(Camera.main.transform);

        if(!progressing) return;

        counter += Time.deltaTime;
        progressScale.localScale = new(Mathf.Lerp(MIN_FILL, MAX_FILL, counter / duration), 1, 1); 

        if(counter >= duration) {
            Destroy(gameObject);
        }

    }
}

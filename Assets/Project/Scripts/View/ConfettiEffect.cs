using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FindTheCat.View
{
    public class ConfettiEffect : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private RectTransform _parent;
        [SerializeField] private int _count = 60;
        [SerializeField] private float _spawnRadius = 400f;
        [SerializeField] private float _lifetime = 2.5f;
        [SerializeField] private float _speed = 300f;

        [Header("Visuals")]
        [SerializeField]
        private Color[] _colors = new Color[]
        {
            new Color(1f,    0.27f, 0.27f),
            new Color(1f,    0.78f, 0.18f),
            new Color(0.29f, 0.85f, 0.38f),
            new Color(0.26f, 0.63f, 1f),
            new Color(0.85f, 0.29f, 1f),
        };

        public void Play()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            for (int i = 0; i < _count; i++)
            {
                SpawnOne();
                yield return new WaitForSeconds(0.02f);
            }
        }

        private void SpawnOne()
        {
            var go = new GameObject("Confetti");
            var rect = go.AddComponent<RectTransform>();
            var img = go.AddComponent<Image>();

            rect.SetParent(_parent, false);

            rect.anchoredPosition = new Vector2(
                Random.Range(-_spawnRadius, _spawnRadius),
                Random.Range(200f, 500f));

            rect.sizeDelta = new Vector2(
                Random.Range(10f, 20f),
                Random.Range(8f, 16f));

            rect.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

            img.color = _colors[Random.Range(0, _colors.Length)];

            StartCoroutine(AnimateConfetti(rect, img));
        }

        private IEnumerator AnimateConfetti(RectTransform rect, Image img)
        {
            float elapsed = 0f;
            float rotateSpeed = Random.Range(-200f, 200f);
            var startColor = img.color;
            var velocity = new Vector2(Random.Range(-80f, 80f), -_speed);

            while (elapsed < _lifetime)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / _lifetime;

                rect.anchoredPosition += velocity * Time.deltaTime;
                rect.Rotate(0, 0, rotateSpeed * Time.deltaTime);

                var c = startColor;
                c.a = Mathf.Lerp(1f, 0f, t);
                img.color = c;

                yield return null;
            }

            Destroy(rect.gameObject);
        }
    }
}

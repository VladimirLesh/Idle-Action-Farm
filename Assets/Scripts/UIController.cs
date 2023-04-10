using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    [SerializeField] private RawImage _coin;
    [SerializeField] private Counter _counter;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private TMP_Text countText;
    [SerializeField] private CoinImg _coinImg;
    [SerializeField] private List<RawImage> _rawImagies;
    private int count = 0;
    private int price = 15;

    private void Start()
    {
        countText.text = count.ToString();
    }

    private RawImage SpawnCoin(Transform transform) => Instantiate(_coin, transform);

    private void IncreaseCount(RawImage rawImage)
    {
        _counter.ShakeImg();
        count += price;
        countText.text = count.ToString();
        Destroy(rawImage.gameObject);
    }

    public void MoveCoin()
    {
        RawImage coin = SpawnCoin(_spawnPos);
        coin.transform.localScale = new Vector3(1f, 1f, 1f);
        coin.transform.position = _spawnPos.position;
        coin.transform.DOMove(_targetPos.position, _speed * Time.deltaTime).OnComplete(() => IncreaseCount(coin));
    }
}

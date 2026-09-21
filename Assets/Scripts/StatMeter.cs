using System;
using UnityEngine;

/// <summary>
/// The player's single "shape" stat: pickups and gates push it up or down.
/// Character visuals/animation react to its current tier via OnValueChanged.
/// </summary>
public class StatMeter : MonoBehaviour
{
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 100f;
    [SerializeField] private float startValue = 30f;

    public float Value { get; private set; }
    public float Min => minValue;
    public float Max => maxValue;
    public float Normalized => Mathf.InverseLerp(minValue, maxValue, Value);

    /// <summary>Raised whenever the value changes: (newValue, normalized 0..1).</summary>
    public event Action<float, float> OnValueChanged;

    public static StatMeter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Value = startValue;
    }

    private void Start()
    {
        OnValueChanged?.Invoke(Value, Normalized);
    }

    public void Add(float amount)
    {
        SetValue(Value + amount);
    }

    public void Multiply(float factor)
    {
        SetValue(Value * factor);
    }

    public void SetValue(float newValue)
    {
        Value = Mathf.Clamp(newValue, minValue, maxValue);
        OnValueChanged?.Invoke(Value, Normalized);
    }

    public void ResetToStart()
    {
        SetValue(startValue);
    }
}

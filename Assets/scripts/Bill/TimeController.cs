using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    [SerializeField] private float timeMultiplier;
    [SerializeField] public float startHour;
    [SerializeField] private TextMeshProUGUI timeText;
    public DateTime CurrentTime;
    public float closeRestaurantTime;
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunriseHour;
    [SerializeField] public float sunsetHour;
    private TimeSpan sunriseTime, sunsetTime;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;
    [SerializeField] private float maxSunLightIntensity;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLightIntensity;

    //private float timeSpeed = 1f;

    void Start()
    {
        instance = this;
        CurrentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }
    void Update()
    {
        if (PauseManager.instance.gameState == PauseManager.GameStates.Resumed)
        {
            UpdateTimeOfDay();
            Turno();
            rotateSun();
            UpdateLightSettings();

        }
    }
    private void UpdateTimeOfDay()
    {

        CurrentTime = CurrentTime.AddSeconds(Time.deltaTime * timeMultiplier); //*timeSpeed

        if (timeText != null)
        {
            timeText.text = CurrentTime.ToString("HH:mm");
        }
    }

    private void rotateSun()
    {
        float sunLightRotation;
        double percentage;

        if (CurrentTime.TimeOfDay > sunriseTime && CurrentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, CurrentTime.TimeOfDay);

            percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, CurrentTime.TimeOfDay);

            percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        //Quaternion quad = Quaternion.Euler((float)percentage * 0.00416666666666667f, 0, 0);
        //transform.rotation *= quad;
    }

    private void UpdateLightSettings()
    {
        float dotPruduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotPruduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotPruduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotPruduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    public void SetTimeMultiplier(float valor)
    {
        Time.timeScale = valor;
    }

    public void Turno()
    {
        if (CurrentTime.TimeOfDay.Hours == startHour && CurrentTime.TimeOfDay.Minutes == 3)
        {
            EstatisticaController.Instance().ResetDespesas(); //verificar
            TurnoController.Instance().ShowHudInicial(true);
        }

        if (CurrentTime.TimeOfDay.Hours == closeRestaurantTime && CurrentTime.TimeOfDay.Minutes == 3)
        {
            //listas.instance.ResetClient();
            TurnoController.Instance().ShowHudFinal(true);
            Inventario.Instance().UpdateUI();

            for (int i = 0; i < Inventario.Instance().distribuidorasContratadas.Count; i++)
            {
                SistemaFinanceiro.instance.DecreaseMoney(Inventario.Instance().distribuidorasContratadas[i].price); //passar feedback 20/11
            }

            Cozinhero.instance.salarioChecker();
            Atendente.instance.salarioChecker();
            SistemaFinanceiro.instance.GameOver();
            NotificationController.instance.ContasParaPagar();
            BankController.Instance().MultiplicarLoan();

            Time.timeScale = 0;
        }


        PauseManager.instance.UnPauseGame();

        if (CurrentTime.TimeOfDay.Hours == (closeRestaurantTime - 1.0f) && CurrentTime.TimeOfDay.Minutes == 1.0f)
        {
            AlertManager.instance.SetGameAlert("Lembre-se de Pagar as contas de 3 em 3 dias!!!");

            for (int i = 0; i < 3; i++)
            {
                if (listas.instance.cozinheros[i] != null)
                {
                    listas.instance.cozinheros[i].GetComponent<Cozinhero>().salarioVFX.SetActive(false);
                    listas.instance.atendentes[i].GetComponent<Atendente>().salarioVFX.SetActive(false);
                }

            }
        }
        if (CurrentTime.TimeOfDay.Hours == (closeRestaurantTime - 1.0f) && CurrentTime.TimeOfDay.Minutes == 20.0f)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (listas.instance.cozinheros[i] != null)
                    {
                        listas.instance.cozinheros[i].GetComponent<Cozinhero>().salarioVFX.SetActive(true);
                        listas.instance.atendentes[i].GetComponent<Atendente>().salarioVFX.SetActive(true);
                    }

                }

                AlertManager.instance.SetGameAlert("Funcinários sendo pagos!!!");
        }

        if (CurrentTime.TimeOfDay.Days == 2 && CurrentTime.TimeOfDay.Hours == startHour && CurrentTime.TimeOfDay.Minutes == 20)
        {
            AlertManager.instance.SetGameAlert("Cuidado, não fique negativo por 3 dias seguidos!!!");
        }
        

    }
}

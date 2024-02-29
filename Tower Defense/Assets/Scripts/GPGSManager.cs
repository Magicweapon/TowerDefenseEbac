using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSManager : MonoBehaviour
{
	public Text GPGSText;
	public EnemySpawner enemySpawner;

	void Start()
	{
		PlayGamesPlatform.Activate();
		PlayGamesPlatform.Instance.Authenticate(AuthenticationProcessing);
	}

	void OnEnable()
	{
		enemySpawner.OnWaveDefeated += UnlockAchievement;
	}

	void OnDisable()
	{
		enemySpawner.OnWaveDefeated -= UnlockAchievement;
	}

	internal void AuthenticationProcessing(SignInStatus status)
	{
		if (status == SignInStatus.Success)
		{
			GPGSText.text = $"Good Auth \n {Social.localUser.userName} \n {Social.localUser.id}";
		}
		else
		{
			GPGSText.text = $"Bad Auth";
		}
	}

	internal void UnlockAchievement()
	{
		string mStatus;
		Social.ReportProgress(
		GPGSIds.achievement_primera_oleada_ganada,
		100.0f,
		(bool success) =>
		{
			mStatus = success ? "Achievement unlocked" : "Failed to unlock achievement";
			GPGSText.text = mStatus;
		});
	}
}

using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using TMPro;

public class UnityAuthentication : MonoBehaviour
{
	public TMP_InputField inputText;
	public TMP_Text text;
	async void Awake()
	{
		try
		{
			await UnityServices.InitializeAsync();

			Debug.Log(UnityServices.State);

			SetupEvents();

			await SignInAnonymouslyAsync();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

	// Setup authentication event handlers if desired
	void SetupEvents()
	{
		AuthenticationService.Instance.SignedIn += () => {
			// Shows how to get a playerID
			Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

			// Shows how to get an access token
			Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

		};

		AuthenticationService.Instance.SignInFailed += (err) => {
			Debug.LogError(err);
		};

		AuthenticationService.Instance.SignedOut += () => {
			Debug.Log("Player signed out.");
		};

		AuthenticationService.Instance.Expired += () =>
		{
			Debug.Log("Player session could not be refreshed and expired.");
		};
	}

	async Task SignInAnonymouslyAsync()
	{
		try
		{
			await AuthenticationService.Instance.SignInAnonymouslyAsync();
			Debug.Log("Sign in anonymously succeeded!");

			// Shows how to get the playerID
			Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

		}
		catch (AuthenticationException ex)
		{
			// Compare error code to AuthenticationErrorCodes
			// Notify the player with the proper error message
			Debug.LogException(ex);
		}
		catch (RequestFailedException ex)
		{
			// Compare error code to CommonErrorCodes
			// Notify the player with the proper error message
			Debug.LogException(ex);
		}
	}


	public async void Save()
    {
		await SaveService.Save("1", inputText.text);

	}


	public async void Load()
    {
		text.text = await SaveService.Load("1");
    }
}

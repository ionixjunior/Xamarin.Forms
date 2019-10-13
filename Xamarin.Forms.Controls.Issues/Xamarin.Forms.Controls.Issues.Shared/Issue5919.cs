using System;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

#if UITEST
using Xamarin.Forms.Core.UITests;
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 5919, "[Bug] Data binding an array item of type enum is incorrect", PlatformAffected.All)]
	public class Issue5919 : TestContentPage
	{
		protected override void Init()
		{
			BindingContext = new Issue5919ViewModel();
			Content = BuildLayout();
		}

		private View BuildLayout()
		{
			var stackLayout = new StackLayout();
			stackLayout.Padding = new Thickness(50);

			var label1 = new Label();
			label1.SetBinding(Label.TextProperty, "Board[0]");

			var label2 = new Label();
			label2.SetBinding(Label.TextProperty, "CurrentPlayer");

			stackLayout.Children.Add(label1);
			stackLayout.Children.Add(label2);

			return stackLayout;
		}

#if UITEST
		[Test]
		public void Issue5919Test()
		{
			RunningApp.WaitForElement("Nobody");
			RunningApp.WaitForElement("X");
		}
#endif
	}

	public class Issue5919ViewModel
	{
		public Player[] Board { get; }
		public Player CurrentPlayer { get; }

		public Issue5919ViewModel()
		{
			Board = new Player[] { Player.Nobody, Player.X, Player.O };
			CurrentPlayer = Player.X;
		}
	}

	public enum Player
	{
		Nobody,
		X,
		O
	}
}

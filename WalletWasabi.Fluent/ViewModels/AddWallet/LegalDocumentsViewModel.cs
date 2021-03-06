using WalletWasabi.Fluent.ViewModels.Navigation;

namespace WalletWasabi.Fluent.ViewModels.AddWallet
{
	[NavigationMetaData(
		Title = "Legal Docs",
		Caption = "Displays terms and conditions",
		Order = 3,
		Category = "General",
		Keywords = new[] { "View", "Legal", "Docs", "Documentation", "Terms", "Conditions", "Help" },
		IconName = "info_regular",
		NavigationTarget = NavigationTarget.DialogScreen)]
	public partial class LegalDocumentsViewModel : RoutableViewModel
	{
		public LegalDocumentsViewModel(string content)
		{
			Title = "Terms and Conditions";
			Content = content;
			NextCommand = BackCommand;
		}

		public string Content { get; }
	}
}